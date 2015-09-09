var readyStateCheckInterval = window.setInterval(function () {
    if (document.readyState !== 'complete') {
        return;
    }
    window.clearInterval(readyStateCheckInterval);

    chrome.runtime.sendMessage({ say: 'complete' });

    /**
     * https://developer.mozilla.org/en/docs/Web/API/MutationObserver
     * @type {MutationObserver}
     */
    var observer = new window.MutationObserver(function (mutations, observer) {
        for (var i = 0, I = mutations.length; i < I; i++) {
            // try?
            chrome.runtime.sendMessage({
                type: 'MutationObserver',
                data: {
                    type: mutations[i].type
                }
            });
        }
    });

    observer.observe(document, {
        subtree: true,
        attributes: true,
        childList: true,
        characterData: true
    });

}, 80);
