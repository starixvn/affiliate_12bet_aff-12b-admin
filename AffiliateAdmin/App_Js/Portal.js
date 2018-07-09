var Portal = (function () {
    var rcleanScript = /^\s*<!(?:\[CDATA\[|--)|(?:\]\]|--)>\s*$/g;

    var preservedScriptAttributes = {
        type: true,
        src: true,
        noModule: true
    };

    function DOMEval(code, doc, node) {
        doc = doc || document;

        var i,
            script = doc.createElement("script");

        script.text = code;
        if (node) {
            for (i in preservedScriptAttributes) {
                if (node[i]) {
                    script[i] = node[i];
                }
            }
        }
        doc.head.appendChild(script).parentNode.removeChild(script);
    }

    function requestScript(src) {
        var xhrScript = new XMLHttpRequest();
        xhrScript.onreadystatechange = function () {
            if (xhrScript.readyState == XMLHttpRequest.DONE) {
                DOMEval(xhrScript.responseText.replace(rcleanScript, ""));
            }
        }
        xhrScript.open('GET', src, false);
        xhrScript.setRequestHeader("Accept", "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01");
        xhrScript.setRequestHeader("X-Requested-With", "XMLHttpRequest");
        xhrScript.send();
    }

    var _me = {
        resolveClientUrl: function (url) {
            var rootUrl = window.rootUrl || "";
            return rootUrl + url;
        },

        handleClickMenu: function (url) {
            var requestUrl = _me.resolveClientUrl(url);

            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (xhr.readyState == XMLHttpRequest.DONE) {
                    document.getElementById("portalContainer").innerHTML = xhr.responseText;
                    var div = document.createElement("div");
                    div.innerHTML = xhr.responseText;

                    var scripts = div.getElementsByTagName("script");
                    if (scripts.length) {
                        for (var i = 0; i < scripts.length; i++) {
                            var node = scripts[i];

                            if (node.src && (node.type || "").toLowerCase() !== "module") {
                                requestScript(node.src);
                            } else {
                                var doc = scripts[scripts.length - 1].ownerDocument;
                                DOMEval(node.textContent.replace(rcleanScript, ""), doc, node);
                            }
                        }
                    }
                }
            }

            xhr.open('GET', requestUrl, true);
            xhr.send();
        }
    };

    return _me;
})();