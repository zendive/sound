var readyStateCheckInterval = window.setInterval(function () {
    if (document.readyState === 'complete') {
        window.clearInterval(readyStateCheckInterval);
        chrome.runtime.sendMessage({ say: 'complete' });
    }
}, 80);

/**
 * https://developer.mozilla.org/en/docs/Web/API/MutationObserver
 * @type {MutationObserver}
 */
var observer = new window.MutationObserver(function (mutations, observer) {
    for (var i = 0, I = mutations.length; i < I; i++) {
        chrome.runtime.sendMessage({ type: 'MutationObserver', data: mutations[i] });
    }
});

observer.observe(document, {
    subtree: true,
    attributes: true,
    childList: true,
    characterData: true
});
