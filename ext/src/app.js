// http://code.tutsplus.com/tutorials/the-web-audio-api-make-your-own-web-synthesizer--cms-23887
// https://developer.mozilla.org/en-US/docs/Web/API/OscillatorNode/type
chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    if ('inject' === request.context) {
        chrome.tabs.executeScript(sender.tab.id, {
            file: 'src/module/voyager.js',
            allFrames: true,
            matchAboutBlank: false,
            runAt: 'document_start' // document_start document_end document_idle
        });
    }
    else if ('complete' === request.context) {
        chrome.pageAction.show(sender.tab.id);
        chrome.tts.speak(sender.tab.index +' complete', {
            voiceName: 'Google US English', enqueue: false, rate: 1.5
        });
        chrome.tabs.sendMessage(sender.tab.id, {context: 'load'});
    }
});

// https://developer.chrome.com/extensions/tabs
// Programmatic injection: https://developer.chrome.com/extensions/content_scripts#pi
chrome.tabs.onCreated.addListener(function (tab) {
    console.log('chrome.tabs.onCreated', tab.id);
});

chrome.tabs.onRemoved.addListener(function (tabId, removeInfo) {
    console.log('chrome.tabs.onRemoved', tabId);
    chrome.tabs.sendMessage(tabId, {context: 'unload'});
});
