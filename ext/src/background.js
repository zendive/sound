
chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    if (request.say) {
        chrome.tts.speak(request.say, { voiceName: 'Google US English', enqueue: !true, rate: 1.5 });
    }
});

chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
    try {
        console.log(sender.tab.id);
        chrome.pageAction.show(sender.tab.id);
        sendResponse();
    }
    catch (bug) {
        console.error(bug);
    }
});
