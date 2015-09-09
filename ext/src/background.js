chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    try {
        chrome.pageAction.show(sender.tab.id);
        sendResponse();
    }
    catch (bug) {
        console.error(bug);
    }
});

chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    if (request.say) {
        //console.debug('sender.tab', sender.tab);
        chrome.tts.speak([sender.tab.index, request.say].join(' '), {
            voiceName: 'Google US English', enqueue: !true, rate: 1.5
        });
    }
});

var context = new AudioContext(),
    masterVolume = context.createGain(),
    stopTime = null,
    mutationWaveMap = {
        attributes: {wave: 'sine', frequency: 1000, duration: 0.02},
        childList: {wave: 'sine', frequency: 540, duration: 0.06},
        characterData: {wave: 'square', frequency: 5000, duration: 0.1},
        _default: {wave: 'triangle', frequency: 8000, duration: 0.5}
    };
masterVolume.gain.value = 0.1;
masterVolume.connect(context.destination);

function mutationObserverHandler (request, sender, sendResponse) {
    var ct = context.currentTime;
    if (ct < stopTime) {
        return;
    }

    var effect = (mutationWaveMap[request.data.type] || mutationWaveMap._default),
        osc = context.createOscillator();
    //*DBG*/console.debug('effect', effect);

    osc.connect(masterVolume);
    osc.type = effect.wave;
    var frequencyShiftMagnitude = 25 * (effect.frequency/100);
    var frequencyShift = window.parseInt(frequencyShiftMagnitude * Math.random());
    osc.frequency.value = effect.frequency + frequencyShift;
    stopTime = ct + effect.duration;

    osc.start(ct);
    osc.stop(stopTime);
}

/**
 * http://code.tutsplus.com/tutorials/the-web-audio-api-make-your-own-web-synthesizer--cms-23887
 * https://developer.mozilla.org/en-US/docs/Web/API/OscillatorNode/type
 */
chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    if ('MutationObserver' === request.type) {
        mutationObserverHandler(request, sender, sendResponse);
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
