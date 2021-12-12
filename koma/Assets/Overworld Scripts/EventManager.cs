using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class EventManager : MonoBehaviour
{
    //public static int useIC; //controlled by config.txt in game files. controls some elements of events. 0: off, 1: on.
    //public static string pName; //player char name. set in introduction

    public static EventManager _instance;
    //like a library.
    //the overworld will call to it, and it will return events that can be loaded today.

    private int useIC;
    private string pName;

    [SerializeField] private GameObject shakeObject;

    [SerializeField] private Font ancientsFont;
    [SerializeField] private Font defaultFont;

    //knows whether an event has been called before.
    [SerializeField] private SoundManager SM;
    [SerializeField] private FadeManager fader;
    [SerializeField] private Notifier notifier;
    [SerializeField] private TooltipManager ttm;
    [SerializeField] private PortraitLibrary pLibrary;
    
    //dialogue stuff members:
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private CanvasGroup choiceParent;
    [SerializeField] private Image bg;
    [SerializeField] private GameObject nameBox;
    [SerializeField] private Text nameText;
    [SerializeField] private Text sentenceText;
    [SerializeField] private Button buttonPrefab = null;

    [SerializeField] private Button[] textControlButtons; //in order: auto, skip, history

    //dialogue box hider
    private bool hideAcceptsInput = true; //when true, you can toggle hide. false when transforming.
    private bool hideOn = false; //when true, hide the dialogue box. also, stop the text from advancing.
    [SerializeField] private CanvasGroup diaBoxGroup; //the thing we hide/show.

    //image swap speed
    private float imgFadeSpeed = 1.5f; //higher is faster. controls the speed at which char imgs are replaced/shown/hidden during events.

    //typing speed controllers
    private float textWait = 0.035f; //how many seconds to wait after a non-period character in typesentence
    private float autoWait = 1.75f; //when auto is on, time waited when a sentence is fully written out before playing the next one
    private bool skipOn = false; //when true, don't wait at all between textWaits, just display one after another.
    private bool autoOn = false; //when true, the player can't continue the text, but it will continue automatically.
    private bool historyOn = false; //when true, viewing history and cannot continue the story.
    private bool usingDefaultFont = true;
    private bool isTalking; //determines the mode of speech which is used when nametext is not empty. can either be true:talk [""] or false:think [()] 

    private string currentSpeakerName; //used for pushing entries in history.
    [SerializeField] private GameObject HistoryPort; //master gameobject for the history interface.
    [SerializeField] private HistoryScroller histScroll; //used to fill/clear the content of the history interface.
    private List<HistoryEntry> historyList; 
    private int historyLimit = 10; //the max number of displays the historyList stores at a time.
    
    [SerializeField] private GameObject canProceedArrow; //visible when canProceed, invisible when cannot.
    private bool canProceed;
    private Event heldEv;
    private Story script;
    [SerializeField] private GameObject[] portraitSlots; //3 total. dimensions are 540 x 1080 | 1 : 2 ratio

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;
    }
    void Update()
    {             
        //toggle text control states:
        //a: auto
        //left ctrl: skip
        //h: history

        if (Input.GetKeyDown(KeyCode.A) && historyOn == false && hideOn == false)
        {
            toggle_auto();
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && historyOn == false && hideOn == false)
        {
            toggle_skip();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            toggle_history();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            toggle_hide();
        }
        if (hideOn == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            toggle_hide();
        }

        //press 'spacebar' to continue, only if canProceed if true, we aren't showing history, and we aren't hiding dialogue box
        if (canProceed == true && historyOn == false)
        {
            if (skipOn == false && autoOn == false && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
            {
                //start textDisplaying sound
                SM.play_typingSound();
                DisplayNextSentence();
            }
            else if (skipOn == true)
            {
                DisplayNextSentence();
            }
            else if (autoOn == true)
            {
                SM.play_typingSound();
                DisplayNextSentence();
            }                    
        }     
    }

    //MANAGE EVENT PREVIEW
    public static void dungeon_hovered(Dungeon dun, float xCoord, float yCoord)
    {
        _instance.ttm.show_dungeon_preview(dun, xCoord, yCoord);
    }
    public static void event_hovered(Event ev, float xCoord, float yCoord)
    {
        _instance.preview_event(ev, xCoord, yCoord);
    }
    public static void event_unhovered()
    {
        _instance.ttm.dismiss();
    }
    void preview_event(Event ev, float x, float y)
    {
        ttm.show_event_preview(ev, pLibrary, x, y);
    }

    //MANAGE EVENT SKIP/AUTO BUTTONS
    IEnumerator modify_diaBox_alpha(bool toTransparent, float speed = 1f)
    {
        float objectAlpha = diaBoxGroup.alpha;
        float currentAlpha;
        if (toTransparent) //set alpha to 0
        {
            while (diaBoxGroup.alpha > 0)
            {
                currentAlpha = objectAlpha - (speed * Time.deltaTime);
                objectAlpha = currentAlpha;
                diaBoxGroup.alpha = objectAlpha;
                yield return null;
            }
        }
        else //set alpha to 1
        {
            while (diaBoxGroup.alpha < 1)
            {
                currentAlpha = objectAlpha + (speed * Time.deltaTime);
                objectAlpha = currentAlpha;
                diaBoxGroup.alpha = objectAlpha;
                yield return null;
            }
        }
        hideAcceptsInput = true;
    }
    public void toggle_hide()
    {
        Debug.Log("toggle hide called");
        if (hideAcceptsInput == false) return;
        hideAcceptsInput = false;
        hideOn = !hideOn;
        //we transition it to the state we want using alpha, though.
        if (hideOn == true) 
        {
            StartCoroutine(modify_diaBox_alpha(true));
        }
        else
        {
            StartCoroutine(modify_diaBox_alpha(false));
        }
    }
    public void toggle_auto()
    {
        autoOn = !autoOn;
        if (autoOn == true)
        {
            textControlButtons[0].image.color = Color.black;
        }
        else
        {
            textControlButtons[0].image.color = Color.grey;
        }
    }
    public void toggle_skip()
    {
        skipOn = !skipOn;
        if (skipOn == true)
        {
            textControlButtons[1].image.color = Color.black;
        }
        else
        {
            textControlButtons[1].image.color = Color.grey;
        }
    }   
    public void toggle_history()
    {
        //use a viewport to view the last ?? sentences.
        //they're all safely stored in historyQueue, a queue of strings.
        historyOn = !historyOn;

        if (historyOn == true)
        {
            textControlButtons[2].image.color = Color.black;
            show_history();
        }
        else
        {
            textControlButtons[2].image.color = Color.grey;
            hide_history();
        }        
    }
    void show_history()
    {
        //fill history port.
        histScroll.show(historyList);
        HistoryPort.SetActive(true);
    }
    void hide_history()
    {
        HistoryPort.SetActive(false);
    }

    //MANAGE EVENT RUNNING
    public static void event_triggered(Event ev, bool doPause = true)
    {
        //called from eventHolder when an event is mouse clicked.       
        _instance.begin_event(ev, doPause);
    }

    IEnumerator healthy_pause(float duration)
    {
        //pause for a minute before starting the event. (so we can)
        yield return new WaitForSeconds(duration);
        setup_event();
    }
    IEnumerator TypeSentence(string sentence)
    {
        if (skipOn == false)
        {
            //hide canProceed arrow
            canProceedArrow.SetActive(false);

            sentenceText.text = "";
            string displayString = ""; 

            yield return new WaitForSeconds(0.05f);
            
            foreach (char letter in sentence.ToCharArray())
            {
                if (skipOn == true)
                {
                    //i.e., if skip is turned on while the sentence is playing.
                    sentenceText.text = sentence;
                    break;
                }

                displayString += letter;

                //control quotes, parentheses, or nothing.
                if (nameText.text == "")
                {
                    sentenceText.text = displayString;                    
                }
                else
                {
                    if (isTalking == true) //use quotes
                    {
                        sentenceText.text = "\"" + displayString + "\"";
                    }
                    else //use parantheses
                    {
                        sentenceText.text = "(" + displayString + ")";
                    }
                }

                yield return new WaitForSeconds(textWait); 
            }
            //show canProceed arrow.
            canProceedArrow.SetActive(true);
        }
        else
        {
            sentenceText.text = sentence;
            yield return new WaitForSeconds(0.1f);
        }
        
        if (autoOn == true && skipOn == false)
        {
            yield return new WaitForSeconds(autoWait);
        }
        canProceed = true;
    }

    public void begin_event(Event ev, bool doPause = true)
    {
        fader.fade_to_black();
        heldEv = ev;
        if (doPause)
        {
            StartCoroutine(healthy_pause(2.0f));
        }
        else
        {
            setup_event();
        }
    }   
    void setup_event()
    {
        //setup initial view
        if (ttm != null) ttm.dismiss();
        nameText.text = "";
        sentenceText.text = "";

        //reset auto/skip/and history       
        autoOn = false;
        skipOn = false;
        historyOn = false;
        currentSpeakerName = "NO SPEAKER";
        if (historyList == null) historyList = new List<HistoryEntry>();
        else historyList.Clear();
        for (int i = 0; i < textControlButtons.Length; i++)
        {
            textControlButtons[i].interactable = true;
            textControlButtons[i].image.color = Color.grey;
        }

        //setup starting speaker portraits - hide them all.
        for (int i = 0; i < 3; i++)
        {
            portraitSlots[i].gameObject.SetActive(false);
        }
        dialogueCanvas.SetActive(true);

        //setup story
        script = new Story(heldEv.get_story().text);

        //fill in roles text.
        fill_roles();

        //link
        link_external_functions();

        //enable input; we can start the event now.
        DisplayNextSentence();
    }
    void DisplayNextSentence()
    {
        //delete any remaining choice thingies, if they are there.
        int childCount = choiceParent.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(choiceParent.transform.GetChild(i).gameObject);
        }

        canProceed = false;
        if ( script.canContinue == true)
        {
            string sentence = script.Continue().Trim();

            //add name, sentence pair to history
            HistoryEntry entry = new HistoryEntry(currentSpeakerName, sentence);
            if (historyList.Count == historyLimit) //history limit is here.
            {
                historyList.RemoveAt(0);
            }
            historyList.Add(entry);

            StartCoroutine(TypeSentence(sentence));
        }
        else if (script.currentChoices.Count > 0)
        {
            //choices setup            
            for (int i = 0; i < script.currentChoices.Count; i++)
            {
                Choice choice = script.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                button.onClick.AddListener(delegate { OnClickChoiceButton(choice); });
            }
        }
        else
        {
            //end of story.
            end_event(heldEv);            
        }
    }
    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(choiceParent.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        return choice;
    }
    void OnClickChoiceButton(Choice choice)
    {
        SM.play_buttonSound();
        script.ChooseChoiceIndex(choice.index);
        DisplayNextSentence();
    }    
    void end_event(Event ev)
    {
        //make all the text control buttons uninteractable.
        for(int i = 0; i < textControlButtons.Length; i++)
        {
            textControlButtons[i].interactable = false;
        }

        //finish off the event.
        ev.post_event();
        ev.check_notifier(notifier);
        canProceed = false;
        fader.fade_from_black_cheat();
    }

    void recalibrate_portrait_positions()
    {
        //called when a portrait is hidden or called. we recalibrate the remaining portraits
        //so they're in the center in a nice way.

        //first, count how many portraits are still showing.
        int showing = 0;
        foreach(GameObject go in portraitSlots)
        {
            if (go.activeSelf == true) showing++;
        }

        List<GameObject> showingList = new List<GameObject>();
        for (int i = 0; i < portraitSlots.Length; i++)
        {
            if (portraitSlots[i].activeSelf == true) showingList.Add(portraitSlots[i]);
        }

        //position portrait slots based on number of portraits.
        switch (showing)
        {
            case 1:
                showingList[0].transform.localPosition = new Vector2(0f, 0f);
                break;
            case 2:
                showingList[0].transform.localPosition = new Vector2(-410f, 0f);
                showingList[1].transform.localPosition = new Vector2(410f, 0f);

                break;
            case 3:
                showingList[0].transform.localPosition = new Vector2(-615f, 0f);
                showingList[1].transform.localPosition = new Vector2(0f, 0f);
                showingList[2].transform.localPosition = new Vector2(615f, 0f);
                break;
        }
    }
    void fill_roles()
    {
        //called at the start of each story event to fill in character roles.       
        sentenceText.font = defaultFont;

        //dummy values -- will actually read from text file in save folder on main menu launch. hold the values in a static class i guess.
        useIC = 1; 
        script.variablesState["ic"] = useIC;
        pName = "SampleName";
        script.variablesState["player"] = pName;    
    }

    //LINKING EXTERNAL FUNCTIONS
    void link_external_functions()
    {
        //all the storys share the same external functions. we are externalizing complexity from 
        //ink and putting it here instead.

        //music
        script.BindExternalFunction("stop_music", () =>
        {
            this.stop_music();
        });
        script.BindExternalFunction("play_music", (int whichTrack) =>
        {
            this.play_music(whichTrack);
        });
        script.BindExternalFunction("play_sound", (int whichTrack) =>
        {
            this.play_sound(whichTrack);
        });

        //visuals
        script.BindExternalFunction("bg", (int id) => 
        {
            this.set_bg(id);
        });
        script.BindExternalFunction("n", (string name) =>
        {
            this.set_name(name);
        });
        script.BindExternalFunction("talk", (bool mode) =>
        {
            this.set_speech(mode);
        });
        script.BindExternalFunction("toggle_font", () =>
        {
            this.toggle_font();
        });
        script.BindExternalFunction("show", (int which, int index) =>
        {
            this.set_portrait_slot(which, index);
        });
        script.BindExternalFunction("hide", (int which) =>
        {
            this.hide_portrait_slot(which);
        });
        script.BindExternalFunction("shake", (int intensity, float duration) =>
        {
            this.camera_shake(intensity, duration);
        });

        //game (e.g. rel increased, set flag, etc.)

    }

    //text effects
    void set_name(string s)
    {
        if ( s == "" )
        {
            nameText.text = "";
            nameBox.SetActive(false);
        }
        else
        {
            nameBox.SetActive(true);
            nameText.text = s;
        }
        currentSpeakerName = s;
    }
    void set_speech(bool state)
    {
        isTalking = state;
    }
    void toggle_font()
    {
        if (usingDefaultFont == true)
        {
            sentenceText.font = ancientsFont;
        }
        else
        {
            sentenceText.font = defaultFont;
        }
        usingDefaultFont = !usingDefaultFont;
    }

    //visual effects
    void camera_shake(int intensity, float duration)
    {
        // -currently not working.
        //intensity determines how much the camera shakes
        //duration determines how long the shake lasts.
        //at the end of the time elapsed, returns back to normal.
        StartCoroutine(trigger_camera_shake(intensity, duration));
    }
    IEnumerator trigger_camera_shake(int intensity, float duration)
    {
        //save initial position.
        Vector3 initial_position = shakeObject.transform.localPosition;

        //zoom in so we don't see the edges fraying
        shakeObject.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);

        //shake
        while (duration > 0f)
        {
            shakeObject.transform.localPosition = initial_position + (Random.insideUnitSphere * intensity);
            duration -= Time.deltaTime;
            yield return null;
        }
        //reset initial position
        shakeObject.transform.localPosition = initial_position;
        shakeObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    void set_portrait_slot(int whichSlot, int index)
    {
        //if skip is on, then show directly.
        //two methods: 
        //1. if already showing an image: fade to half alpha, switch image, fade back to full alpha.
        //2. if not showing an image: set half alpha, switch image, show image, fade to full alpha.

        if (skipOn == false)
        {
            StartCoroutine(handle_image_switch_fade(imgFadeSpeed, portraitSlots[whichSlot].activeSelf, whichSlot, pLibrary.retrieve_fullp(index)));
        }
        else
        {
            portraitSlots[whichSlot].GetComponent<Image>().sprite = pLibrary.retrieve_fullp(index);
            portraitSlots[whichSlot].SetActive(true);
            recalibrate_portrait_positions();
        }
    }
    void hide_portrait_slot(int whichSlot)
    {
        StartCoroutine(handle_image_hide_fade(imgFadeSpeed, whichSlot));
        recalibrate_portrait_positions();
    }
    void set_bg(int id)
    {
        if (skipOn == false) fader.fade_to_black(); //automatic fading behaviour
        bg.sprite = pLibrary.retrieve_eventBg(id);
    }
    IEnumerator handle_image_switch_fade(float speed, bool fadeOutFirst, int whichSlot, Sprite switchSprite)
    {
        Color objectColor = portraitSlots[whichSlot].GetComponent<Image>().color;
        float fadeAmount;

        //if fadeOutFirst is true, then first fade the image to half alpha, then assign switchSprite to portraitSlots[whichSlot].
        if (fadeOutFirst == true)
        {
            speed *= 2; //makes each img switch faster, so it matches the same total duration as the other method.
            while ( objectColor.a > 0.25f )
            {
                fadeAmount = objectColor.a - (speed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                portraitSlots[whichSlot].GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0.25f);
            portraitSlots[whichSlot].SetActive(true);
        }
        portraitSlots[whichSlot].GetComponent<Image>().sprite = switchSprite;

        //fade the image back to full alpha.
        while ( objectColor.a < 1f )
        {
            fadeAmount = objectColor.a + (speed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            portraitSlots[whichSlot].GetComponent<Image>().color = objectColor;
            yield return null;
        }

    }
    IEnumerator handle_image_hide_fade(float speed, int whichSlot)
    {
        Color objectColor = portraitSlots[whichSlot].GetComponent<Image>().color;
        float fadeAmount;
        while (objectColor.a > 0.25f)
        {
            fadeAmount = objectColor.a - (speed * 2 * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            portraitSlots[whichSlot].GetComponent<Image>().color = objectColor;
            yield return null;
        }
        portraitSlots[whichSlot].gameObject.SetActive(false);
    }

    //sound effects
    void stop_music()
    {
        //stops a song from SM.
        SM.stop_playing();
    }
    void play_music(int whichTrack)
    {
        SM.play_loop(whichTrack);
    }
    void play_sound(int whichTrack)
    {
        //plays a song/sound from SM.
        SM.play_once(whichTrack);
    }
    
    
}
