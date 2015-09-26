var module = new (function () {
    "use strict";

    var Module = function () {
        this.q = {
            loaded: false,
            mutationWaveMap: {
                attributes: {wave: 'sine', frequency: 1000, duration: 0.02},
                childList: {wave: 'sine', frequency: 540, duration: 0.06},
                characterData: {wave: 'square', frequency: 5000, duration: 0.1},
                _default: {wave: 'triangle', frequency: 8000, duration: 0.5}
            }
        };
    };

    Module.prototype.name = 'Voyager';

    Module.prototype.init = function () {
        chrome.runtime.onMessage.addListener(this.onMessage.bind(this));

        var readyStateCheckInterval = window.setInterval(function () {
            if (document.readyState !== 'complete') {
                return;
            }
            window.clearInterval(readyStateCheckInterval);
            chrome.runtime.sendMessage({context: 'complete'});
        }.bind(this), 80);
    };

    Module.prototype.load = function () {
        this.q.loaded = true;
        this._start();
    };

    Module.prototype.unload = function () {
        if (this.q.loaded) {
            this.q.observer.disconnect();
        }
    };

    Module.prototype.onMessage = function (request, sender, sendResponse) {
        if ('load' === request.context) {
            this.load();
        }
        else if ('unload' === request.context) {
            this.unload();
        }
        // sendResponse.call();
    };

    Module.prototype._start = function () {
        // https://developer.mozilla.org/en/docs/Web/API/MutationObserver
        this.q.observer = new window.MutationObserver(function (mutations, observer) {
            for (var i = 0, I = mutations.length; i < I; i++) {
                this._mutationObserverHandler(mutations[i].type);
            }
        }.bind(this));
        this.q.observer.observe(document, {
            subtree: true,
            attributes: true,
            childList: true,
            characterData: true
        });

        this.q.audioContext = new AudioContext();
        this.q.masterVolume = this.q.audioContext.createGain();
        this.q.stopTime = null;
        this.q.masterVolume.gain.value = 0.1;
        this.q.masterVolume.connect(this.q.audioContext.destination);
    };

    Module.prototype._mutationObserverHandler = function (mutationType) {
        this.q.currentTime = this.q.audioContext.currentTime;
        if (this.q.currentTime < this.q.stopTime) {
            return;
        }
        this.q.effect = (this.q.mutationWaveMap[mutationType] || this.q.mutationWaveMap._default);
        this.q.osc = this.q.audioContext.createOscillator();
        this.q.osc.connect(this.q.masterVolume);
        this.q.osc.type = this.q.effect.wave;

        this.q.effect.shift = {
            magnitude: 25*(this.q.effect.frequency/100),
            frequency: 0
        };
        this.q.effect.shift.frequency = window.parseInt(this.q.effect.shift.magnitude * Math.random());
        this.q.osc.frequency.value = this.q.effect.frequency + this.q.effect.shift.frequency;

        this.q.stopTime = this.q.currentTime + this.q.effect.duration;
        this.q.osc.start(this.q.currentTime);
        this.q.osc.stop(this.q.stopTime);
        //*DBG*/console.debug('effect', state.effect);
    };

    return new Module();
})();

module.init();
