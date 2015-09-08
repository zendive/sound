console.log('inject');

var readyStateCheckInterval = window.setInterval(function () {
    if (document.readyState === 'loading') {
        chrome.runtime.sendMessage({ say: 'loading' });
    }
    else if (document.readyState === 'interactive') {
        chrome.runtime.sendMessage({ say: 'interactive' });
    }
    else if (document.readyState === 'complete') {
        window.clearInterval(readyStateCheckInterval);
        chrome.runtime.sendMessage({ say: 'complete' });
    }
}, 80);
