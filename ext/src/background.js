chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    try {
        console.log(sender.tab.id);
        //chrome.pageAction.show(sender.tab.id);
        sendResponse();
    }
    catch (bug) {
        console.error(bug);
    }
});

chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    if (request.say) {
        //console.debug('sender.tab', sender.tab);
        chrome.tts.speak('Number '+ sender.tab.index +' '+ request.say, {
            voiceName: 'Google US English', enqueue: !true, rate: 1.5
        });
    }
});

var context = new AudioContext(),
    masterVolume = context.createGain();
masterVolume.gain.value = 0.1;
masterVolume.connect(context.destination);

/**
 * http://code.tutsplus.com/tutorials/the-web-audio-api-make-your-own-web-synthesizer--cms-23887
 * https://developer.mozilla.org/en-US/docs/Web/API/OscillatorNode/type
 */
chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    if (request.type === 'MutationObserver') {
        var osc = context.createOscillator();
        osc.connect(masterVolume);

        switch (request.data.type) {
            case 'attributes':
                osc.type = 'square';
                break;
            case 'characterData':
                osc.type = 'sawtooth';
                break;
            case 'childList':
                osc.type = 'triangle';
                break;
            default:
                osc.type = 'sine';
                break;
        }

        //osc.detune.value = 10;
        osc.start(context.currentTime);
        osc.stop(context.currentTime + 0.05);
    }
});

/**
 * https://developer.chrome.com/extensions/tabs
 * Programmatic injection: https://developer.chrome.com/extensions/content_scripts#pi
 */
chrome.tabs.onCreated.addListener(function (tab) {
});
chrome.tabs.onRemoved.addListener(function (tabId, removeInfo) {
    //observer.disconnect();
});
