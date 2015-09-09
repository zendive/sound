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
        chrome.tts.speak(['Number', sender.tab.index, request.say].join(' '), {
            voiceName: 'Google US English', enqueue: !true, rate: 1.5
        });
    }
});

var context = new AudioContext(),
    masterVolume = context.createGain(),
    mutationWaveMap = {
        attributes: {wave: 'sine', frequency: 1000, duration: 0.02},
        childList: {wave: 'sine', frequency: 100, duration: 0.05},
        characterData: {wave: 'square', frequency: 8000, duration: 0.3},
        default: {wave: 'triangle', frequency: 8000, duration: 0.5}
    };
masterVolume.gain.value = 0.1;
masterVolume.connect(context.destination);

/**
 * http://code.tutsplus.com/tutorials/the-web-audio-api-make-your-own-web-synthesizer--cms-23887
 * https://developer.mozilla.org/en-US/docs/Web/API/OscillatorNode/type
 */
chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    if ('MutationObserver' === request.type) {
        var osc = context.createOscillator();
        osc.connect(masterVolume);

        var effect = (mutationWaveMap[request.data.type] || mutationWaveMap.default);
        osc.type = effect.wave;
        osc.frequency.value = effect.frequency;
        osc.detune.value = window.parseInt(10*Math.random());

        console.debug('effect', effect);

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
