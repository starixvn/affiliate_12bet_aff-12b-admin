/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "/dist/";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./source/js/pages/commission-group/index.jsx");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./node_modules/axios/index.js":
/*!*************************************!*\
  !*** ./node_modules/axios/index.js ***!
  \*************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! ./lib/axios */ "./node_modules/axios/lib/axios.js");

/***/ }),

/***/ "./node_modules/axios/lib/adapters/xhr.js":
/*!************************************************!*\
  !*** ./node_modules/axios/lib/adapters/xhr.js ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");
var settle = __webpack_require__(/*! ./../core/settle */ "./node_modules/axios/lib/core/settle.js");
var buildURL = __webpack_require__(/*! ./../helpers/buildURL */ "./node_modules/axios/lib/helpers/buildURL.js");
var parseHeaders = __webpack_require__(/*! ./../helpers/parseHeaders */ "./node_modules/axios/lib/helpers/parseHeaders.js");
var isURLSameOrigin = __webpack_require__(/*! ./../helpers/isURLSameOrigin */ "./node_modules/axios/lib/helpers/isURLSameOrigin.js");
var createError = __webpack_require__(/*! ../core/createError */ "./node_modules/axios/lib/core/createError.js");
var btoa = (typeof window !== 'undefined' && window.btoa && window.btoa.bind(window)) || __webpack_require__(/*! ./../helpers/btoa */ "./node_modules/axios/lib/helpers/btoa.js");

module.exports = function xhrAdapter(config) {
  return new Promise(function dispatchXhrRequest(resolve, reject) {
    var requestData = config.data;
    var requestHeaders = config.headers;

    if (utils.isFormData(requestData)) {
      delete requestHeaders['Content-Type']; // Let the browser set it
    }

    var request = new XMLHttpRequest();
    var loadEvent = 'onreadystatechange';
    var xDomain = false;

    // For IE 8/9 CORS support
    // Only supports POST and GET calls and doesn't returns the response headers.
    // DON'T do this for testing b/c XMLHttpRequest is mocked, not XDomainRequest.
    if ("development" !== 'test' &&
        typeof window !== 'undefined' &&
        window.XDomainRequest && !('withCredentials' in request) &&
        !isURLSameOrigin(config.url)) {
      request = new window.XDomainRequest();
      loadEvent = 'onload';
      xDomain = true;
      request.onprogress = function handleProgress() {};
      request.ontimeout = function handleTimeout() {};
    }

    // HTTP basic authentication
    if (config.auth) {
      var username = config.auth.username || '';
      var password = config.auth.password || '';
      requestHeaders.Authorization = 'Basic ' + btoa(username + ':' + password);
    }

    request.open(config.method.toUpperCase(), buildURL(config.url, config.params, config.paramsSerializer), true);

    // Set the request timeout in MS
    request.timeout = config.timeout;

    // Listen for ready state
    request[loadEvent] = function handleLoad() {
      if (!request || (request.readyState !== 4 && !xDomain)) {
        return;
      }

      // The request errored out and we didn't get a response, this will be
      // handled by onerror instead
      // With one exception: request that using file: protocol, most browsers
      // will return status as 0 even though it's a successful request
      if (request.status === 0 && !(request.responseURL && request.responseURL.indexOf('file:') === 0)) {
        return;
      }

      // Prepare the response
      var responseHeaders = 'getAllResponseHeaders' in request ? parseHeaders(request.getAllResponseHeaders()) : null;
      var responseData = !config.responseType || config.responseType === 'text' ? request.responseText : request.response;
      var response = {
        data: responseData,
        // IE sends 1223 instead of 204 (https://github.com/axios/axios/issues/201)
        status: request.status === 1223 ? 204 : request.status,
        statusText: request.status === 1223 ? 'No Content' : request.statusText,
        headers: responseHeaders,
        config: config,
        request: request
      };

      settle(resolve, reject, response);

      // Clean up request
      request = null;
    };

    // Handle low level network errors
    request.onerror = function handleError() {
      // Real errors are hidden from us by the browser
      // onerror should only fire if it's a network error
      reject(createError('Network Error', config, null, request));

      // Clean up request
      request = null;
    };

    // Handle timeout
    request.ontimeout = function handleTimeout() {
      reject(createError('timeout of ' + config.timeout + 'ms exceeded', config, 'ECONNABORTED',
        request));

      // Clean up request
      request = null;
    };

    // Add xsrf header
    // This is only done if running in a standard browser environment.
    // Specifically not if we're in a web worker, or react-native.
    if (utils.isStandardBrowserEnv()) {
      var cookies = __webpack_require__(/*! ./../helpers/cookies */ "./node_modules/axios/lib/helpers/cookies.js");

      // Add xsrf header
      var xsrfValue = (config.withCredentials || isURLSameOrigin(config.url)) && config.xsrfCookieName ?
          cookies.read(config.xsrfCookieName) :
          undefined;

      if (xsrfValue) {
        requestHeaders[config.xsrfHeaderName] = xsrfValue;
      }
    }

    // Add headers to the request
    if ('setRequestHeader' in request) {
      utils.forEach(requestHeaders, function setRequestHeader(val, key) {
        if (typeof requestData === 'undefined' && key.toLowerCase() === 'content-type') {
          // Remove Content-Type if data is undefined
          delete requestHeaders[key];
        } else {
          // Otherwise add header to the request
          request.setRequestHeader(key, val);
        }
      });
    }

    // Add withCredentials to request if needed
    if (config.withCredentials) {
      request.withCredentials = true;
    }

    // Add responseType to request if needed
    if (config.responseType) {
      try {
        request.responseType = config.responseType;
      } catch (e) {
        // Expected DOMException thrown by browsers not compatible XMLHttpRequest Level 2.
        // But, this can be suppressed for 'json' type as it can be parsed by default 'transformResponse' function.
        if (config.responseType !== 'json') {
          throw e;
        }
      }
    }

    // Handle progress if needed
    if (typeof config.onDownloadProgress === 'function') {
      request.addEventListener('progress', config.onDownloadProgress);
    }

    // Not all browsers support upload events
    if (typeof config.onUploadProgress === 'function' && request.upload) {
      request.upload.addEventListener('progress', config.onUploadProgress);
    }

    if (config.cancelToken) {
      // Handle cancellation
      config.cancelToken.promise.then(function onCanceled(cancel) {
        if (!request) {
          return;
        }

        request.abort();
        reject(cancel);
        // Clean up request
        request = null;
      });
    }

    if (requestData === undefined) {
      requestData = null;
    }

    // Send the request
    request.send(requestData);
  });
};


/***/ }),

/***/ "./node_modules/axios/lib/axios.js":
/*!*****************************************!*\
  !*** ./node_modules/axios/lib/axios.js ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./utils */ "./node_modules/axios/lib/utils.js");
var bind = __webpack_require__(/*! ./helpers/bind */ "./node_modules/axios/lib/helpers/bind.js");
var Axios = __webpack_require__(/*! ./core/Axios */ "./node_modules/axios/lib/core/Axios.js");
var defaults = __webpack_require__(/*! ./defaults */ "./node_modules/axios/lib/defaults.js");

/**
 * Create an instance of Axios
 *
 * @param {Object} defaultConfig The default config for the instance
 * @return {Axios} A new instance of Axios
 */
function createInstance(defaultConfig) {
  var context = new Axios(defaultConfig);
  var instance = bind(Axios.prototype.request, context);

  // Copy axios.prototype to instance
  utils.extend(instance, Axios.prototype, context);

  // Copy context to instance
  utils.extend(instance, context);

  return instance;
}

// Create the default instance to be exported
var axios = createInstance(defaults);

// Expose Axios class to allow class inheritance
axios.Axios = Axios;

// Factory for creating new instances
axios.create = function create(instanceConfig) {
  return createInstance(utils.merge(defaults, instanceConfig));
};

// Expose Cancel & CancelToken
axios.Cancel = __webpack_require__(/*! ./cancel/Cancel */ "./node_modules/axios/lib/cancel/Cancel.js");
axios.CancelToken = __webpack_require__(/*! ./cancel/CancelToken */ "./node_modules/axios/lib/cancel/CancelToken.js");
axios.isCancel = __webpack_require__(/*! ./cancel/isCancel */ "./node_modules/axios/lib/cancel/isCancel.js");

// Expose all/spread
axios.all = function all(promises) {
  return Promise.all(promises);
};
axios.spread = __webpack_require__(/*! ./helpers/spread */ "./node_modules/axios/lib/helpers/spread.js");

module.exports = axios;

// Allow use of default import syntax in TypeScript
module.exports.default = axios;


/***/ }),

/***/ "./node_modules/axios/lib/cancel/Cancel.js":
/*!*************************************************!*\
  !*** ./node_modules/axios/lib/cancel/Cancel.js ***!
  \*************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * A `Cancel` is an object that is thrown when an operation is canceled.
 *
 * @class
 * @param {string=} message The message.
 */
function Cancel(message) {
  this.message = message;
}

Cancel.prototype.toString = function toString() {
  return 'Cancel' + (this.message ? ': ' + this.message : '');
};

Cancel.prototype.__CANCEL__ = true;

module.exports = Cancel;


/***/ }),

/***/ "./node_modules/axios/lib/cancel/CancelToken.js":
/*!******************************************************!*\
  !*** ./node_modules/axios/lib/cancel/CancelToken.js ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var Cancel = __webpack_require__(/*! ./Cancel */ "./node_modules/axios/lib/cancel/Cancel.js");

/**
 * A `CancelToken` is an object that can be used to request cancellation of an operation.
 *
 * @class
 * @param {Function} executor The executor function.
 */
function CancelToken(executor) {
  if (typeof executor !== 'function') {
    throw new TypeError('executor must be a function.');
  }

  var resolvePromise;
  this.promise = new Promise(function promiseExecutor(resolve) {
    resolvePromise = resolve;
  });

  var token = this;
  executor(function cancel(message) {
    if (token.reason) {
      // Cancellation has already been requested
      return;
    }

    token.reason = new Cancel(message);
    resolvePromise(token.reason);
  });
}

/**
 * Throws a `Cancel` if cancellation has been requested.
 */
CancelToken.prototype.throwIfRequested = function throwIfRequested() {
  if (this.reason) {
    throw this.reason;
  }
};

/**
 * Returns an object that contains a new `CancelToken` and a function that, when called,
 * cancels the `CancelToken`.
 */
CancelToken.source = function source() {
  var cancel;
  var token = new CancelToken(function executor(c) {
    cancel = c;
  });
  return {
    token: token,
    cancel: cancel
  };
};

module.exports = CancelToken;


/***/ }),

/***/ "./node_modules/axios/lib/cancel/isCancel.js":
/*!***************************************************!*\
  !*** ./node_modules/axios/lib/cancel/isCancel.js ***!
  \***************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


module.exports = function isCancel(value) {
  return !!(value && value.__CANCEL__);
};


/***/ }),

/***/ "./node_modules/axios/lib/core/Axios.js":
/*!**********************************************!*\
  !*** ./node_modules/axios/lib/core/Axios.js ***!
  \**********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var defaults = __webpack_require__(/*! ./../defaults */ "./node_modules/axios/lib/defaults.js");
var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");
var InterceptorManager = __webpack_require__(/*! ./InterceptorManager */ "./node_modules/axios/lib/core/InterceptorManager.js");
var dispatchRequest = __webpack_require__(/*! ./dispatchRequest */ "./node_modules/axios/lib/core/dispatchRequest.js");

/**
 * Create a new instance of Axios
 *
 * @param {Object} instanceConfig The default config for the instance
 */
function Axios(instanceConfig) {
  this.defaults = instanceConfig;
  this.interceptors = {
    request: new InterceptorManager(),
    response: new InterceptorManager()
  };
}

/**
 * Dispatch a request
 *
 * @param {Object} config The config specific for this request (merged with this.defaults)
 */
Axios.prototype.request = function request(config) {
  /*eslint no-param-reassign:0*/
  // Allow for axios('example/url'[, config]) a la fetch API
  if (typeof config === 'string') {
    config = utils.merge({
      url: arguments[0]
    }, arguments[1]);
  }

  config = utils.merge(defaults, {method: 'get'}, this.defaults, config);
  config.method = config.method.toLowerCase();

  // Hook up interceptors middleware
  var chain = [dispatchRequest, undefined];
  var promise = Promise.resolve(config);

  this.interceptors.request.forEach(function unshiftRequestInterceptors(interceptor) {
    chain.unshift(interceptor.fulfilled, interceptor.rejected);
  });

  this.interceptors.response.forEach(function pushResponseInterceptors(interceptor) {
    chain.push(interceptor.fulfilled, interceptor.rejected);
  });

  while (chain.length) {
    promise = promise.then(chain.shift(), chain.shift());
  }

  return promise;
};

// Provide aliases for supported request methods
utils.forEach(['delete', 'get', 'head', 'options'], function forEachMethodNoData(method) {
  /*eslint func-names:0*/
  Axios.prototype[method] = function(url, config) {
    return this.request(utils.merge(config || {}, {
      method: method,
      url: url
    }));
  };
});

utils.forEach(['post', 'put', 'patch'], function forEachMethodWithData(method) {
  /*eslint func-names:0*/
  Axios.prototype[method] = function(url, data, config) {
    return this.request(utils.merge(config || {}, {
      method: method,
      url: url,
      data: data
    }));
  };
});

module.exports = Axios;


/***/ }),

/***/ "./node_modules/axios/lib/core/InterceptorManager.js":
/*!***********************************************************!*\
  !*** ./node_modules/axios/lib/core/InterceptorManager.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

function InterceptorManager() {
  this.handlers = [];
}

/**
 * Add a new interceptor to the stack
 *
 * @param {Function} fulfilled The function to handle `then` for a `Promise`
 * @param {Function} rejected The function to handle `reject` for a `Promise`
 *
 * @return {Number} An ID used to remove interceptor later
 */
InterceptorManager.prototype.use = function use(fulfilled, rejected) {
  this.handlers.push({
    fulfilled: fulfilled,
    rejected: rejected
  });
  return this.handlers.length - 1;
};

/**
 * Remove an interceptor from the stack
 *
 * @param {Number} id The ID that was returned by `use`
 */
InterceptorManager.prototype.eject = function eject(id) {
  if (this.handlers[id]) {
    this.handlers[id] = null;
  }
};

/**
 * Iterate over all the registered interceptors
 *
 * This method is particularly useful for skipping over any
 * interceptors that may have become `null` calling `eject`.
 *
 * @param {Function} fn The function to call for each interceptor
 */
InterceptorManager.prototype.forEach = function forEach(fn) {
  utils.forEach(this.handlers, function forEachHandler(h) {
    if (h !== null) {
      fn(h);
    }
  });
};

module.exports = InterceptorManager;


/***/ }),

/***/ "./node_modules/axios/lib/core/createError.js":
/*!****************************************************!*\
  !*** ./node_modules/axios/lib/core/createError.js ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var enhanceError = __webpack_require__(/*! ./enhanceError */ "./node_modules/axios/lib/core/enhanceError.js");

/**
 * Create an Error with the specified message, config, error code, request and response.
 *
 * @param {string} message The error message.
 * @param {Object} config The config.
 * @param {string} [code] The error code (for example, 'ECONNABORTED').
 * @param {Object} [request] The request.
 * @param {Object} [response] The response.
 * @returns {Error} The created error.
 */
module.exports = function createError(message, config, code, request, response) {
  var error = new Error(message);
  return enhanceError(error, config, code, request, response);
};


/***/ }),

/***/ "./node_modules/axios/lib/core/dispatchRequest.js":
/*!********************************************************!*\
  !*** ./node_modules/axios/lib/core/dispatchRequest.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");
var transformData = __webpack_require__(/*! ./transformData */ "./node_modules/axios/lib/core/transformData.js");
var isCancel = __webpack_require__(/*! ../cancel/isCancel */ "./node_modules/axios/lib/cancel/isCancel.js");
var defaults = __webpack_require__(/*! ../defaults */ "./node_modules/axios/lib/defaults.js");
var isAbsoluteURL = __webpack_require__(/*! ./../helpers/isAbsoluteURL */ "./node_modules/axios/lib/helpers/isAbsoluteURL.js");
var combineURLs = __webpack_require__(/*! ./../helpers/combineURLs */ "./node_modules/axios/lib/helpers/combineURLs.js");

/**
 * Throws a `Cancel` if cancellation has been requested.
 */
function throwIfCancellationRequested(config) {
  if (config.cancelToken) {
    config.cancelToken.throwIfRequested();
  }
}

/**
 * Dispatch a request to the server using the configured adapter.
 *
 * @param {object} config The config that is to be used for the request
 * @returns {Promise} The Promise to be fulfilled
 */
module.exports = function dispatchRequest(config) {
  throwIfCancellationRequested(config);

  // Support baseURL config
  if (config.baseURL && !isAbsoluteURL(config.url)) {
    config.url = combineURLs(config.baseURL, config.url);
  }

  // Ensure headers exist
  config.headers = config.headers || {};

  // Transform request data
  config.data = transformData(
    config.data,
    config.headers,
    config.transformRequest
  );

  // Flatten headers
  config.headers = utils.merge(
    config.headers.common || {},
    config.headers[config.method] || {},
    config.headers || {}
  );

  utils.forEach(
    ['delete', 'get', 'head', 'post', 'put', 'patch', 'common'],
    function cleanHeaderConfig(method) {
      delete config.headers[method];
    }
  );

  var adapter = config.adapter || defaults.adapter;

  return adapter(config).then(function onAdapterResolution(response) {
    throwIfCancellationRequested(config);

    // Transform response data
    response.data = transformData(
      response.data,
      response.headers,
      config.transformResponse
    );

    return response;
  }, function onAdapterRejection(reason) {
    if (!isCancel(reason)) {
      throwIfCancellationRequested(config);

      // Transform response data
      if (reason && reason.response) {
        reason.response.data = transformData(
          reason.response.data,
          reason.response.headers,
          config.transformResponse
        );
      }
    }

    return Promise.reject(reason);
  });
};


/***/ }),

/***/ "./node_modules/axios/lib/core/enhanceError.js":
/*!*****************************************************!*\
  !*** ./node_modules/axios/lib/core/enhanceError.js ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * Update an Error with the specified config, error code, and response.
 *
 * @param {Error} error The error to update.
 * @param {Object} config The config.
 * @param {string} [code] The error code (for example, 'ECONNABORTED').
 * @param {Object} [request] The request.
 * @param {Object} [response] The response.
 * @returns {Error} The error.
 */
module.exports = function enhanceError(error, config, code, request, response) {
  error.config = config;
  if (code) {
    error.code = code;
  }
  error.request = request;
  error.response = response;
  return error;
};


/***/ }),

/***/ "./node_modules/axios/lib/core/settle.js":
/*!***********************************************!*\
  !*** ./node_modules/axios/lib/core/settle.js ***!
  \***********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var createError = __webpack_require__(/*! ./createError */ "./node_modules/axios/lib/core/createError.js");

/**
 * Resolve or reject a Promise based on response status.
 *
 * @param {Function} resolve A function that resolves the promise.
 * @param {Function} reject A function that rejects the promise.
 * @param {object} response The response.
 */
module.exports = function settle(resolve, reject, response) {
  var validateStatus = response.config.validateStatus;
  // Note: status is not exposed by XDomainRequest
  if (!response.status || !validateStatus || validateStatus(response.status)) {
    resolve(response);
  } else {
    reject(createError(
      'Request failed with status code ' + response.status,
      response.config,
      null,
      response.request,
      response
    ));
  }
};


/***/ }),

/***/ "./node_modules/axios/lib/core/transformData.js":
/*!******************************************************!*\
  !*** ./node_modules/axios/lib/core/transformData.js ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

/**
 * Transform the data for a request or a response
 *
 * @param {Object|String} data The data to be transformed
 * @param {Array} headers The headers for the request or response
 * @param {Array|Function} fns A single function or Array of functions
 * @returns {*} The resulting transformed data
 */
module.exports = function transformData(data, headers, fns) {
  /*eslint no-param-reassign:0*/
  utils.forEach(fns, function transform(fn) {
    data = fn(data, headers);
  });

  return data;
};


/***/ }),

/***/ "./node_modules/axios/lib/defaults.js":
/*!********************************************!*\
  !*** ./node_modules/axios/lib/defaults.js ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(process) {

var utils = __webpack_require__(/*! ./utils */ "./node_modules/axios/lib/utils.js");
var normalizeHeaderName = __webpack_require__(/*! ./helpers/normalizeHeaderName */ "./node_modules/axios/lib/helpers/normalizeHeaderName.js");

var DEFAULT_CONTENT_TYPE = {
  'Content-Type': 'application/x-www-form-urlencoded'
};

function setContentTypeIfUnset(headers, value) {
  if (!utils.isUndefined(headers) && utils.isUndefined(headers['Content-Type'])) {
    headers['Content-Type'] = value;
  }
}

function getDefaultAdapter() {
  var adapter;
  if (typeof XMLHttpRequest !== 'undefined') {
    // For browsers use XHR adapter
    adapter = __webpack_require__(/*! ./adapters/xhr */ "./node_modules/axios/lib/adapters/xhr.js");
  } else if (typeof process !== 'undefined') {
    // For node use HTTP adapter
    adapter = __webpack_require__(/*! ./adapters/http */ "./node_modules/axios/lib/adapters/xhr.js");
  }
  return adapter;
}

var defaults = {
  adapter: getDefaultAdapter(),

  transformRequest: [function transformRequest(data, headers) {
    normalizeHeaderName(headers, 'Content-Type');
    if (utils.isFormData(data) ||
      utils.isArrayBuffer(data) ||
      utils.isBuffer(data) ||
      utils.isStream(data) ||
      utils.isFile(data) ||
      utils.isBlob(data)
    ) {
      return data;
    }
    if (utils.isArrayBufferView(data)) {
      return data.buffer;
    }
    if (utils.isURLSearchParams(data)) {
      setContentTypeIfUnset(headers, 'application/x-www-form-urlencoded;charset=utf-8');
      return data.toString();
    }
    if (utils.isObject(data)) {
      setContentTypeIfUnset(headers, 'application/json;charset=utf-8');
      return JSON.stringify(data);
    }
    return data;
  }],

  transformResponse: [function transformResponse(data) {
    /*eslint no-param-reassign:0*/
    if (typeof data === 'string') {
      try {
        data = JSON.parse(data);
      } catch (e) { /* Ignore */ }
    }
    return data;
  }],

  /**
   * A timeout in milliseconds to abort a request. If set to 0 (default) a
   * timeout is not created.
   */
  timeout: 0,

  xsrfCookieName: 'XSRF-TOKEN',
  xsrfHeaderName: 'X-XSRF-TOKEN',

  maxContentLength: -1,

  validateStatus: function validateStatus(status) {
    return status >= 200 && status < 300;
  }
};

defaults.headers = {
  common: {
    'Accept': 'application/json, text/plain, */*'
  }
};

utils.forEach(['delete', 'get', 'head'], function forEachMethodNoData(method) {
  defaults.headers[method] = {};
});

utils.forEach(['post', 'put', 'patch'], function forEachMethodWithData(method) {
  defaults.headers[method] = utils.merge(DEFAULT_CONTENT_TYPE);
});

module.exports = defaults;

/* WEBPACK VAR INJECTION */}.call(this, __webpack_require__(/*! ./../../process/browser.js */ "./node_modules/process/browser.js")))

/***/ }),

/***/ "./node_modules/axios/lib/helpers/bind.js":
/*!************************************************!*\
  !*** ./node_modules/axios/lib/helpers/bind.js ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


module.exports = function bind(fn, thisArg) {
  return function wrap() {
    var args = new Array(arguments.length);
    for (var i = 0; i < args.length; i++) {
      args[i] = arguments[i];
    }
    return fn.apply(thisArg, args);
  };
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/btoa.js":
/*!************************************************!*\
  !*** ./node_modules/axios/lib/helpers/btoa.js ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


// btoa polyfill for IE<10 courtesy https://github.com/davidchambers/Base64.js

var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';

function E() {
  this.message = 'String contains an invalid character';
}
E.prototype = new Error;
E.prototype.code = 5;
E.prototype.name = 'InvalidCharacterError';

function btoa(input) {
  var str = String(input);
  var output = '';
  for (
    // initialize result and counter
    var block, charCode, idx = 0, map = chars;
    // if the next str index does not exist:
    //   change the mapping table to "="
    //   check if d has no fractional digits
    str.charAt(idx | 0) || (map = '=', idx % 1);
    // "8 - idx % 1 * 8" generates the sequence 2, 4, 6, 8
    output += map.charAt(63 & block >> 8 - idx % 1 * 8)
  ) {
    charCode = str.charCodeAt(idx += 3 / 4);
    if (charCode > 0xFF) {
      throw new E();
    }
    block = block << 8 | charCode;
  }
  return output;
}

module.exports = btoa;


/***/ }),

/***/ "./node_modules/axios/lib/helpers/buildURL.js":
/*!****************************************************!*\
  !*** ./node_modules/axios/lib/helpers/buildURL.js ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

function encode(val) {
  return encodeURIComponent(val).
    replace(/%40/gi, '@').
    replace(/%3A/gi, ':').
    replace(/%24/g, '$').
    replace(/%2C/gi, ',').
    replace(/%20/g, '+').
    replace(/%5B/gi, '[').
    replace(/%5D/gi, ']');
}

/**
 * Build a URL by appending params to the end
 *
 * @param {string} url The base of the url (e.g., http://www.google.com)
 * @param {object} [params] The params to be appended
 * @returns {string} The formatted url
 */
module.exports = function buildURL(url, params, paramsSerializer) {
  /*eslint no-param-reassign:0*/
  if (!params) {
    return url;
  }

  var serializedParams;
  if (paramsSerializer) {
    serializedParams = paramsSerializer(params);
  } else if (utils.isURLSearchParams(params)) {
    serializedParams = params.toString();
  } else {
    var parts = [];

    utils.forEach(params, function serialize(val, key) {
      if (val === null || typeof val === 'undefined') {
        return;
      }

      if (utils.isArray(val)) {
        key = key + '[]';
      } else {
        val = [val];
      }

      utils.forEach(val, function parseValue(v) {
        if (utils.isDate(v)) {
          v = v.toISOString();
        } else if (utils.isObject(v)) {
          v = JSON.stringify(v);
        }
        parts.push(encode(key) + '=' + encode(v));
      });
    });

    serializedParams = parts.join('&');
  }

  if (serializedParams) {
    url += (url.indexOf('?') === -1 ? '?' : '&') + serializedParams;
  }

  return url;
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/combineURLs.js":
/*!*******************************************************!*\
  !*** ./node_modules/axios/lib/helpers/combineURLs.js ***!
  \*******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * Creates a new URL by combining the specified URLs
 *
 * @param {string} baseURL The base URL
 * @param {string} relativeURL The relative URL
 * @returns {string} The combined URL
 */
module.exports = function combineURLs(baseURL, relativeURL) {
  return relativeURL
    ? baseURL.replace(/\/+$/, '') + '/' + relativeURL.replace(/^\/+/, '')
    : baseURL;
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/cookies.js":
/*!***************************************************!*\
  !*** ./node_modules/axios/lib/helpers/cookies.js ***!
  \***************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

module.exports = (
  utils.isStandardBrowserEnv() ?

  // Standard browser envs support document.cookie
  (function standardBrowserEnv() {
    return {
      write: function write(name, value, expires, path, domain, secure) {
        var cookie = [];
        cookie.push(name + '=' + encodeURIComponent(value));

        if (utils.isNumber(expires)) {
          cookie.push('expires=' + new Date(expires).toGMTString());
        }

        if (utils.isString(path)) {
          cookie.push('path=' + path);
        }

        if (utils.isString(domain)) {
          cookie.push('domain=' + domain);
        }

        if (secure === true) {
          cookie.push('secure');
        }

        document.cookie = cookie.join('; ');
      },

      read: function read(name) {
        var match = document.cookie.match(new RegExp('(^|;\\s*)(' + name + ')=([^;]*)'));
        return (match ? decodeURIComponent(match[3]) : null);
      },

      remove: function remove(name) {
        this.write(name, '', Date.now() - 86400000);
      }
    };
  })() :

  // Non standard browser env (web workers, react-native) lack needed support.
  (function nonStandardBrowserEnv() {
    return {
      write: function write() {},
      read: function read() { return null; },
      remove: function remove() {}
    };
  })()
);


/***/ }),

/***/ "./node_modules/axios/lib/helpers/isAbsoluteURL.js":
/*!*********************************************************!*\
  !*** ./node_modules/axios/lib/helpers/isAbsoluteURL.js ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * Determines whether the specified URL is absolute
 *
 * @param {string} url The URL to test
 * @returns {boolean} True if the specified URL is absolute, otherwise false
 */
module.exports = function isAbsoluteURL(url) {
  // A URL is considered absolute if it begins with "<scheme>://" or "//" (protocol-relative URL).
  // RFC 3986 defines scheme name as a sequence of characters beginning with a letter and followed
  // by any combination of letters, digits, plus, period, or hyphen.
  return /^([a-z][a-z\d\+\-\.]*:)?\/\//i.test(url);
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/isURLSameOrigin.js":
/*!***********************************************************!*\
  !*** ./node_modules/axios/lib/helpers/isURLSameOrigin.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

module.exports = (
  utils.isStandardBrowserEnv() ?

  // Standard browser envs have full support of the APIs needed to test
  // whether the request URL is of the same origin as current location.
  (function standardBrowserEnv() {
    var msie = /(msie|trident)/i.test(navigator.userAgent);
    var urlParsingNode = document.createElement('a');
    var originURL;

    /**
    * Parse a URL to discover it's components
    *
    * @param {String} url The URL to be parsed
    * @returns {Object}
    */
    function resolveURL(url) {
      var href = url;

      if (msie) {
        // IE needs attribute set twice to normalize properties
        urlParsingNode.setAttribute('href', href);
        href = urlParsingNode.href;
      }

      urlParsingNode.setAttribute('href', href);

      // urlParsingNode provides the UrlUtils interface - http://url.spec.whatwg.org/#urlutils
      return {
        href: urlParsingNode.href,
        protocol: urlParsingNode.protocol ? urlParsingNode.protocol.replace(/:$/, '') : '',
        host: urlParsingNode.host,
        search: urlParsingNode.search ? urlParsingNode.search.replace(/^\?/, '') : '',
        hash: urlParsingNode.hash ? urlParsingNode.hash.replace(/^#/, '') : '',
        hostname: urlParsingNode.hostname,
        port: urlParsingNode.port,
        pathname: (urlParsingNode.pathname.charAt(0) === '/') ?
                  urlParsingNode.pathname :
                  '/' + urlParsingNode.pathname
      };
    }

    originURL = resolveURL(window.location.href);

    /**
    * Determine if a URL shares the same origin as the current location
    *
    * @param {String} requestURL The URL to test
    * @returns {boolean} True if URL shares the same origin, otherwise false
    */
    return function isURLSameOrigin(requestURL) {
      var parsed = (utils.isString(requestURL)) ? resolveURL(requestURL) : requestURL;
      return (parsed.protocol === originURL.protocol &&
            parsed.host === originURL.host);
    };
  })() :

  // Non standard browser envs (web workers, react-native) lack needed support.
  (function nonStandardBrowserEnv() {
    return function isURLSameOrigin() {
      return true;
    };
  })()
);


/***/ }),

/***/ "./node_modules/axios/lib/helpers/normalizeHeaderName.js":
/*!***************************************************************!*\
  !*** ./node_modules/axios/lib/helpers/normalizeHeaderName.js ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ../utils */ "./node_modules/axios/lib/utils.js");

module.exports = function normalizeHeaderName(headers, normalizedName) {
  utils.forEach(headers, function processHeader(value, name) {
    if (name !== normalizedName && name.toUpperCase() === normalizedName.toUpperCase()) {
      headers[normalizedName] = value;
      delete headers[name];
    }
  });
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/parseHeaders.js":
/*!********************************************************!*\
  !*** ./node_modules/axios/lib/helpers/parseHeaders.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

// Headers whose duplicates are ignored by node
// c.f. https://nodejs.org/api/http.html#http_message_headers
var ignoreDuplicateOf = [
  'age', 'authorization', 'content-length', 'content-type', 'etag',
  'expires', 'from', 'host', 'if-modified-since', 'if-unmodified-since',
  'last-modified', 'location', 'max-forwards', 'proxy-authorization',
  'referer', 'retry-after', 'user-agent'
];

/**
 * Parse headers into an object
 *
 * ```
 * Date: Wed, 27 Aug 2014 08:58:49 GMT
 * Content-Type: application/json
 * Connection: keep-alive
 * Transfer-Encoding: chunked
 * ```
 *
 * @param {String} headers Headers needing to be parsed
 * @returns {Object} Headers parsed into an object
 */
module.exports = function parseHeaders(headers) {
  var parsed = {};
  var key;
  var val;
  var i;

  if (!headers) { return parsed; }

  utils.forEach(headers.split('\n'), function parser(line) {
    i = line.indexOf(':');
    key = utils.trim(line.substr(0, i)).toLowerCase();
    val = utils.trim(line.substr(i + 1));

    if (key) {
      if (parsed[key] && ignoreDuplicateOf.indexOf(key) >= 0) {
        return;
      }
      if (key === 'set-cookie') {
        parsed[key] = (parsed[key] ? parsed[key] : []).concat([val]);
      } else {
        parsed[key] = parsed[key] ? parsed[key] + ', ' + val : val;
      }
    }
  });

  return parsed;
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/spread.js":
/*!**************************************************!*\
  !*** ./node_modules/axios/lib/helpers/spread.js ***!
  \**************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * Syntactic sugar for invoking a function and expanding an array for arguments.
 *
 * Common use case would be to use `Function.prototype.apply`.
 *
 *  ```js
 *  function f(x, y, z) {}
 *  var args = [1, 2, 3];
 *  f.apply(null, args);
 *  ```
 *
 * With `spread` this example can be re-written.
 *
 *  ```js
 *  spread(function(x, y, z) {})([1, 2, 3]);
 *  ```
 *
 * @param {Function} callback
 * @returns {Function}
 */
module.exports = function spread(callback) {
  return function wrap(arr) {
    return callback.apply(null, arr);
  };
};


/***/ }),

/***/ "./node_modules/axios/lib/utils.js":
/*!*****************************************!*\
  !*** ./node_modules/axios/lib/utils.js ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var bind = __webpack_require__(/*! ./helpers/bind */ "./node_modules/axios/lib/helpers/bind.js");
var isBuffer = __webpack_require__(/*! is-buffer */ "./node_modules/is-buffer/index.js");

/*global toString:true*/

// utils is a library of generic helper functions non-specific to axios

var toString = Object.prototype.toString;

/**
 * Determine if a value is an Array
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is an Array, otherwise false
 */
function isArray(val) {
  return toString.call(val) === '[object Array]';
}

/**
 * Determine if a value is an ArrayBuffer
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is an ArrayBuffer, otherwise false
 */
function isArrayBuffer(val) {
  return toString.call(val) === '[object ArrayBuffer]';
}

/**
 * Determine if a value is a FormData
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is an FormData, otherwise false
 */
function isFormData(val) {
  return (typeof FormData !== 'undefined') && (val instanceof FormData);
}

/**
 * Determine if a value is a view on an ArrayBuffer
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a view on an ArrayBuffer, otherwise false
 */
function isArrayBufferView(val) {
  var result;
  if ((typeof ArrayBuffer !== 'undefined') && (ArrayBuffer.isView)) {
    result = ArrayBuffer.isView(val);
  } else {
    result = (val) && (val.buffer) && (val.buffer instanceof ArrayBuffer);
  }
  return result;
}

/**
 * Determine if a value is a String
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a String, otherwise false
 */
function isString(val) {
  return typeof val === 'string';
}

/**
 * Determine if a value is a Number
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a Number, otherwise false
 */
function isNumber(val) {
  return typeof val === 'number';
}

/**
 * Determine if a value is undefined
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if the value is undefined, otherwise false
 */
function isUndefined(val) {
  return typeof val === 'undefined';
}

/**
 * Determine if a value is an Object
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is an Object, otherwise false
 */
function isObject(val) {
  return val !== null && typeof val === 'object';
}

/**
 * Determine if a value is a Date
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a Date, otherwise false
 */
function isDate(val) {
  return toString.call(val) === '[object Date]';
}

/**
 * Determine if a value is a File
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a File, otherwise false
 */
function isFile(val) {
  return toString.call(val) === '[object File]';
}

/**
 * Determine if a value is a Blob
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a Blob, otherwise false
 */
function isBlob(val) {
  return toString.call(val) === '[object Blob]';
}

/**
 * Determine if a value is a Function
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a Function, otherwise false
 */
function isFunction(val) {
  return toString.call(val) === '[object Function]';
}

/**
 * Determine if a value is a Stream
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a Stream, otherwise false
 */
function isStream(val) {
  return isObject(val) && isFunction(val.pipe);
}

/**
 * Determine if a value is a URLSearchParams object
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a URLSearchParams object, otherwise false
 */
function isURLSearchParams(val) {
  return typeof URLSearchParams !== 'undefined' && val instanceof URLSearchParams;
}

/**
 * Trim excess whitespace off the beginning and end of a string
 *
 * @param {String} str The String to trim
 * @returns {String} The String freed of excess whitespace
 */
function trim(str) {
  return str.replace(/^\s*/, '').replace(/\s*$/, '');
}

/**
 * Determine if we're running in a standard browser environment
 *
 * This allows axios to run in a web worker, and react-native.
 * Both environments support XMLHttpRequest, but not fully standard globals.
 *
 * web workers:
 *  typeof window -> undefined
 *  typeof document -> undefined
 *
 * react-native:
 *  navigator.product -> 'ReactNative'
 */
function isStandardBrowserEnv() {
  if (typeof navigator !== 'undefined' && navigator.product === 'ReactNative') {
    return false;
  }
  return (
    typeof window !== 'undefined' &&
    typeof document !== 'undefined'
  );
}

/**
 * Iterate over an Array or an Object invoking a function for each item.
 *
 * If `obj` is an Array callback will be called passing
 * the value, index, and complete array for each item.
 *
 * If 'obj' is an Object callback will be called passing
 * the value, key, and complete object for each property.
 *
 * @param {Object|Array} obj The object to iterate
 * @param {Function} fn The callback to invoke for each item
 */
function forEach(obj, fn) {
  // Don't bother if no value provided
  if (obj === null || typeof obj === 'undefined') {
    return;
  }

  // Force an array if not already something iterable
  if (typeof obj !== 'object') {
    /*eslint no-param-reassign:0*/
    obj = [obj];
  }

  if (isArray(obj)) {
    // Iterate over array values
    for (var i = 0, l = obj.length; i < l; i++) {
      fn.call(null, obj[i], i, obj);
    }
  } else {
    // Iterate over object keys
    for (var key in obj) {
      if (Object.prototype.hasOwnProperty.call(obj, key)) {
        fn.call(null, obj[key], key, obj);
      }
    }
  }
}

/**
 * Accepts varargs expecting each argument to be an object, then
 * immutably merges the properties of each object and returns result.
 *
 * When multiple objects contain the same key the later object in
 * the arguments list will take precedence.
 *
 * Example:
 *
 * ```js
 * var result = merge({foo: 123}, {foo: 456});
 * console.log(result.foo); // outputs 456
 * ```
 *
 * @param {Object} obj1 Object to merge
 * @returns {Object} Result of all merge properties
 */
function merge(/* obj1, obj2, obj3, ... */) {
  var result = {};
  function assignValue(val, key) {
    if (typeof result[key] === 'object' && typeof val === 'object') {
      result[key] = merge(result[key], val);
    } else {
      result[key] = val;
    }
  }

  for (var i = 0, l = arguments.length; i < l; i++) {
    forEach(arguments[i], assignValue);
  }
  return result;
}

/**
 * Extends object a by mutably adding to it the properties of object b.
 *
 * @param {Object} a The object to be extended
 * @param {Object} b The object to copy properties from
 * @param {Object} thisArg The object to bind function to
 * @return {Object} The resulting value of object a
 */
function extend(a, b, thisArg) {
  forEach(b, function assignValue(val, key) {
    if (thisArg && typeof val === 'function') {
      a[key] = bind(val, thisArg);
    } else {
      a[key] = val;
    }
  });
  return a;
}

module.exports = {
  isArray: isArray,
  isArrayBuffer: isArrayBuffer,
  isBuffer: isBuffer,
  isFormData: isFormData,
  isArrayBufferView: isArrayBufferView,
  isString: isString,
  isNumber: isNumber,
  isObject: isObject,
  isUndefined: isUndefined,
  isDate: isDate,
  isFile: isFile,
  isBlob: isBlob,
  isFunction: isFunction,
  isStream: isStream,
  isURLSearchParams: isURLSearchParams,
  isStandardBrowserEnv: isStandardBrowserEnv,
  forEach: forEach,
  merge: merge,
  extend: extend,
  trim: trim
};


/***/ }),

/***/ "./node_modules/babel-runtime/core-js/array/from.js":
/*!**********************************************************!*\
  !*** ./node_modules/babel-runtime/core-js/array/from.js ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/array/from */ "./node_modules/babel-runtime/node_modules/core-js/library/fn/array/from.js"), __esModule: true };

/***/ }),

/***/ "./node_modules/babel-runtime/core-js/json/stringify.js":
/*!**************************************************************!*\
  !*** ./node_modules/babel-runtime/core-js/json/stringify.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/json/stringify */ "./node_modules/babel-runtime/node_modules/core-js/library/fn/json/stringify.js"), __esModule: true };

/***/ }),

/***/ "./node_modules/babel-runtime/core-js/object/assign.js":
/*!*************************************************************!*\
  !*** ./node_modules/babel-runtime/core-js/object/assign.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/assign */ "./node_modules/babel-runtime/node_modules/core-js/library/fn/object/assign.js"), __esModule: true };

/***/ }),

/***/ "./node_modules/babel-runtime/core-js/object/create.js":
/*!*************************************************************!*\
  !*** ./node_modules/babel-runtime/core-js/object/create.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/create */ "./node_modules/babel-runtime/node_modules/core-js/library/fn/object/create.js"), __esModule: true };

/***/ }),

/***/ "./node_modules/babel-runtime/core-js/object/define-property.js":
/*!**********************************************************************!*\
  !*** ./node_modules/babel-runtime/core-js/object/define-property.js ***!
  \**********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/define-property */ "./node_modules/babel-runtime/node_modules/core-js/library/fn/object/define-property.js"), __esModule: true };

/***/ }),

/***/ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js":
/*!***********************************************************************!*\
  !*** ./node_modules/babel-runtime/core-js/object/get-prototype-of.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/get-prototype-of */ "./node_modules/babel-runtime/node_modules/core-js/library/fn/object/get-prototype-of.js"), __esModule: true };

/***/ }),

/***/ "./node_modules/babel-runtime/core-js/object/set-prototype-of.js":
/*!***********************************************************************!*\
  !*** ./node_modules/babel-runtime/core-js/object/set-prototype-of.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/set-prototype-of */ "./node_modules/babel-runtime/node_modules/core-js/library/fn/object/set-prototype-of.js"), __esModule: true };

/***/ }),

/***/ "./node_modules/babel-runtime/core-js/promise.js":
/*!*******************************************************!*\
  !*** ./node_modules/babel-runtime/core-js/promise.js ***!
  \*******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/promise */ "./node_modules/babel-runtime/node_modules/core-js/library/fn/promise.js"), __esModule: true };

/***/ }),

/***/ "./node_modules/babel-runtime/core-js/symbol.js":
/*!******************************************************!*\
  !*** ./node_modules/babel-runtime/core-js/symbol.js ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/symbol */ "./node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/index.js"), __esModule: true };

/***/ }),

/***/ "./node_modules/babel-runtime/core-js/symbol/iterator.js":
/*!***************************************************************!*\
  !*** ./node_modules/babel-runtime/core-js/symbol/iterator.js ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/symbol/iterator */ "./node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/iterator.js"), __esModule: true };

/***/ }),

/***/ "./node_modules/babel-runtime/helpers/asyncToGenerator.js":
/*!****************************************************************!*\
  !*** ./node_modules/babel-runtime/helpers/asyncToGenerator.js ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _promise = __webpack_require__(/*! ../core-js/promise */ "./node_modules/babel-runtime/core-js/promise.js");

var _promise2 = _interopRequireDefault(_promise);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = function (fn) {
  return function () {
    var gen = fn.apply(this, arguments);
    return new _promise2.default(function (resolve, reject) {
      function step(key, arg) {
        try {
          var info = gen[key](arg);
          var value = info.value;
        } catch (error) {
          reject(error);
          return;
        }

        if (info.done) {
          resolve(value);
        } else {
          return _promise2.default.resolve(value).then(function (value) {
            step("next", value);
          }, function (err) {
            step("throw", err);
          });
        }
      }

      return step("next");
    });
  };
};

/***/ }),

/***/ "./node_modules/babel-runtime/helpers/classCallCheck.js":
/*!**************************************************************!*\
  !*** ./node_modules/babel-runtime/helpers/classCallCheck.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

exports.default = function (instance, Constructor) {
  if (!(instance instanceof Constructor)) {
    throw new TypeError("Cannot call a class as a function");
  }
};

/***/ }),

/***/ "./node_modules/babel-runtime/helpers/createClass.js":
/*!***********************************************************!*\
  !*** ./node_modules/babel-runtime/helpers/createClass.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _defineProperty = __webpack_require__(/*! ../core-js/object/define-property */ "./node_modules/babel-runtime/core-js/object/define-property.js");

var _defineProperty2 = _interopRequireDefault(_defineProperty);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = function () {
  function defineProperties(target, props) {
    for (var i = 0; i < props.length; i++) {
      var descriptor = props[i];
      descriptor.enumerable = descriptor.enumerable || false;
      descriptor.configurable = true;
      if ("value" in descriptor) descriptor.writable = true;
      (0, _defineProperty2.default)(target, descriptor.key, descriptor);
    }
  }

  return function (Constructor, protoProps, staticProps) {
    if (protoProps) defineProperties(Constructor.prototype, protoProps);
    if (staticProps) defineProperties(Constructor, staticProps);
    return Constructor;
  };
}();

/***/ }),

/***/ "./node_modules/babel-runtime/helpers/defineProperty.js":
/*!**************************************************************!*\
  !*** ./node_modules/babel-runtime/helpers/defineProperty.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _defineProperty = __webpack_require__(/*! ../core-js/object/define-property */ "./node_modules/babel-runtime/core-js/object/define-property.js");

var _defineProperty2 = _interopRequireDefault(_defineProperty);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = function (obj, key, value) {
  if (key in obj) {
    (0, _defineProperty2.default)(obj, key, {
      value: value,
      enumerable: true,
      configurable: true,
      writable: true
    });
  } else {
    obj[key] = value;
  }

  return obj;
};

/***/ }),

/***/ "./node_modules/babel-runtime/helpers/extends.js":
/*!*******************************************************!*\
  !*** ./node_modules/babel-runtime/helpers/extends.js ***!
  \*******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _assign = __webpack_require__(/*! ../core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = _assign2.default || function (target) {
  for (var i = 1; i < arguments.length; i++) {
    var source = arguments[i];

    for (var key in source) {
      if (Object.prototype.hasOwnProperty.call(source, key)) {
        target[key] = source[key];
      }
    }
  }

  return target;
};

/***/ }),

/***/ "./node_modules/babel-runtime/helpers/inherits.js":
/*!********************************************************!*\
  !*** ./node_modules/babel-runtime/helpers/inherits.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _setPrototypeOf = __webpack_require__(/*! ../core-js/object/set-prototype-of */ "./node_modules/babel-runtime/core-js/object/set-prototype-of.js");

var _setPrototypeOf2 = _interopRequireDefault(_setPrototypeOf);

var _create = __webpack_require__(/*! ../core-js/object/create */ "./node_modules/babel-runtime/core-js/object/create.js");

var _create2 = _interopRequireDefault(_create);

var _typeof2 = __webpack_require__(/*! ../helpers/typeof */ "./node_modules/babel-runtime/helpers/typeof.js");

var _typeof3 = _interopRequireDefault(_typeof2);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = function (subClass, superClass) {
  if (typeof superClass !== "function" && superClass !== null) {
    throw new TypeError("Super expression must either be null or a function, not " + (typeof superClass === "undefined" ? "undefined" : (0, _typeof3.default)(superClass)));
  }

  subClass.prototype = (0, _create2.default)(superClass && superClass.prototype, {
    constructor: {
      value: subClass,
      enumerable: false,
      writable: true,
      configurable: true
    }
  });
  if (superClass) _setPrototypeOf2.default ? (0, _setPrototypeOf2.default)(subClass, superClass) : subClass.__proto__ = superClass;
};

/***/ }),

/***/ "./node_modules/babel-runtime/helpers/objectWithoutProperties.js":
/*!***********************************************************************!*\
  !*** ./node_modules/babel-runtime/helpers/objectWithoutProperties.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

exports.default = function (obj, keys) {
  var target = {};

  for (var i in obj) {
    if (keys.indexOf(i) >= 0) continue;
    if (!Object.prototype.hasOwnProperty.call(obj, i)) continue;
    target[i] = obj[i];
  }

  return target;
};

/***/ }),

/***/ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js":
/*!*************************************************************************!*\
  !*** ./node_modules/babel-runtime/helpers/possibleConstructorReturn.js ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _typeof2 = __webpack_require__(/*! ../helpers/typeof */ "./node_modules/babel-runtime/helpers/typeof.js");

var _typeof3 = _interopRequireDefault(_typeof2);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = function (self, call) {
  if (!self) {
    throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
  }

  return call && ((typeof call === "undefined" ? "undefined" : (0, _typeof3.default)(call)) === "object" || typeof call === "function") ? call : self;
};

/***/ }),

/***/ "./node_modules/babel-runtime/helpers/toConsumableArray.js":
/*!*****************************************************************!*\
  !*** ./node_modules/babel-runtime/helpers/toConsumableArray.js ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _from = __webpack_require__(/*! ../core-js/array/from */ "./node_modules/babel-runtime/core-js/array/from.js");

var _from2 = _interopRequireDefault(_from);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = function (arr) {
  if (Array.isArray(arr)) {
    for (var i = 0, arr2 = Array(arr.length); i < arr.length; i++) {
      arr2[i] = arr[i];
    }

    return arr2;
  } else {
    return (0, _from2.default)(arr);
  }
};

/***/ }),

/***/ "./node_modules/babel-runtime/helpers/typeof.js":
/*!******************************************************!*\
  !*** ./node_modules/babel-runtime/helpers/typeof.js ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _iterator = __webpack_require__(/*! ../core-js/symbol/iterator */ "./node_modules/babel-runtime/core-js/symbol/iterator.js");

var _iterator2 = _interopRequireDefault(_iterator);

var _symbol = __webpack_require__(/*! ../core-js/symbol */ "./node_modules/babel-runtime/core-js/symbol.js");

var _symbol2 = _interopRequireDefault(_symbol);

var _typeof = typeof _symbol2.default === "function" && typeof _iterator2.default === "symbol" ? function (obj) { return typeof obj; } : function (obj) { return obj && typeof _symbol2.default === "function" && obj.constructor === _symbol2.default && obj !== _symbol2.default.prototype ? "symbol" : typeof obj; };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = typeof _symbol2.default === "function" && _typeof(_iterator2.default) === "symbol" ? function (obj) {
  return typeof obj === "undefined" ? "undefined" : _typeof(obj);
} : function (obj) {
  return obj && typeof _symbol2.default === "function" && obj.constructor === _symbol2.default && obj !== _symbol2.default.prototype ? "symbol" : typeof obj === "undefined" ? "undefined" : _typeof(obj);
};

/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/fn/array/from.js":
/*!**********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/fn/array/from.js ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.string.iterator */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.string.iterator.js");
__webpack_require__(/*! ../../modules/es6.array.from */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.from.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Array.from;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/fn/json/stringify.js":
/*!**************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/fn/json/stringify.js ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var core = __webpack_require__(/*! ../../modules/_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var $JSON = core.JSON || (core.JSON = { stringify: JSON.stringify });
module.exports = function stringify(it) { // eslint-disable-line no-unused-vars
  return $JSON.stringify.apply($JSON, arguments);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/fn/object/assign.js":
/*!*************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/fn/object/assign.js ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.assign */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.assign.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object.assign;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/fn/object/create.js":
/*!*************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/fn/object/create.js ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.create */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.create.js");
var $Object = __webpack_require__(/*! ../../modules/_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object;
module.exports = function create(P, D) {
  return $Object.create(P, D);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/fn/object/define-property.js":
/*!**********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/fn/object/define-property.js ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.define-property */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.define-property.js");
var $Object = __webpack_require__(/*! ../../modules/_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object;
module.exports = function defineProperty(it, key, desc) {
  return $Object.defineProperty(it, key, desc);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/fn/object/get-prototype-of.js":
/*!***********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/fn/object/get-prototype-of.js ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.get-prototype-of */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.get-prototype-of.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object.getPrototypeOf;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/fn/object/set-prototype-of.js":
/*!***********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/fn/object/set-prototype-of.js ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.set-prototype-of */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.set-prototype-of.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object.setPrototypeOf;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/fn/promise.js":
/*!*******************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/fn/promise.js ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../modules/es6.object.to-string */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.to-string.js");
__webpack_require__(/*! ../modules/es6.string.iterator */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.string.iterator.js");
__webpack_require__(/*! ../modules/web.dom.iterable */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/web.dom.iterable.js");
__webpack_require__(/*! ../modules/es6.promise */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.promise.js");
__webpack_require__(/*! ../modules/es7.promise.finally */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.finally.js");
__webpack_require__(/*! ../modules/es7.promise.try */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.try.js");
module.exports = __webpack_require__(/*! ../modules/_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Promise;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/index.js":
/*!************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/index.js ***!
  \************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.symbol */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.symbol.js");
__webpack_require__(/*! ../../modules/es6.object.to-string */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.to-string.js");
__webpack_require__(/*! ../../modules/es7.symbol.async-iterator */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.async-iterator.js");
__webpack_require__(/*! ../../modules/es7.symbol.observable */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.observable.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Symbol;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/iterator.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/iterator.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.string.iterator */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.string.iterator.js");
__webpack_require__(/*! ../../modules/web.dom.iterable */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/web.dom.iterable.js");
module.exports = __webpack_require__(/*! ../../modules/_wks-ext */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-ext.js").f('iterator');


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (it) {
  if (typeof it != 'function') throw TypeError(it + ' is not a function!');
  return it;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_add-to-unscopables.js":
/*!************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_add-to-unscopables.js ***!
  \************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function () { /* empty */ };


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-instance.js":
/*!*****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-instance.js ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (it, Constructor, name, forbiddenField) {
  if (!(it instanceof Constructor) || (forbiddenField !== undefined && forbiddenField in it)) {
    throw TypeError(name + ': incorrect invocation!');
  } return it;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var isObject = __webpack_require__(/*! ./_is-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
module.exports = function (it) {
  if (!isObject(it)) throw TypeError(it + ' is not an object!');
  return it;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_array-includes.js":
/*!********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_array-includes.js ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// false -> Array#indexOf
// true  -> Array#includes
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");
var toLength = __webpack_require__(/*! ./_to-length */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-length.js");
var toAbsoluteIndex = __webpack_require__(/*! ./_to-absolute-index */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-absolute-index.js");
module.exports = function (IS_INCLUDES) {
  return function ($this, el, fromIndex) {
    var O = toIObject($this);
    var length = toLength(O.length);
    var index = toAbsoluteIndex(fromIndex, length);
    var value;
    // Array#includes uses SameValueZero equality algorithm
    // eslint-disable-next-line no-self-compare
    if (IS_INCLUDES && el != el) while (length > index) {
      value = O[index++];
      // eslint-disable-next-line no-self-compare
      if (value != value) return true;
    // Array#indexOf ignores holes, Array#includes - not
    } else for (;length > index; index++) if (IS_INCLUDES || index in O) {
      if (O[index] === el) return IS_INCLUDES || index || 0;
    } return !IS_INCLUDES && -1;
  };
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_classof.js":
/*!*************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_classof.js ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// getting tag from 19.1.3.6 Object.prototype.toString()
var cof = __webpack_require__(/*! ./_cof */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js");
var TAG = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('toStringTag');
// ES3 wrong here
var ARG = cof(function () { return arguments; }()) == 'Arguments';

// fallback for IE11 Script Access Denied error
var tryGet = function (it, key) {
  try {
    return it[key];
  } catch (e) { /* empty */ }
};

module.exports = function (it) {
  var O, T, B;
  return it === undefined ? 'Undefined' : it === null ? 'Null'
    // @@toStringTag case
    : typeof (T = tryGet(O = Object(it), TAG)) == 'string' ? T
    // builtinTag case
    : ARG ? cof(O)
    // ES3 arguments fallback
    : (B = cof(O)) == 'Object' && typeof O.callee == 'function' ? 'Arguments' : B;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js":
/*!*********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

var toString = {}.toString;

module.exports = function (it) {
  return toString.call(it).slice(8, -1);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js":
/*!**********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

var core = module.exports = { version: '2.5.7' };
if (typeof __e == 'number') __e = core; // eslint-disable-line no-undef


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_create-property.js":
/*!*********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_create-property.js ***!
  \*********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $defineProperty = __webpack_require__(/*! ./_object-dp */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js");
var createDesc = __webpack_require__(/*! ./_property-desc */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js");

module.exports = function (object, index, value) {
  if (index in object) $defineProperty.f(object, index, createDesc(0, value));
  else object[index] = value;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js":
/*!*********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// optional / simple context binding
var aFunction = __webpack_require__(/*! ./_a-function */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js");
module.exports = function (fn, that, length) {
  aFunction(fn);
  if (that === undefined) return fn;
  switch (length) {
    case 1: return function (a) {
      return fn.call(that, a);
    };
    case 2: return function (a, b) {
      return fn.call(that, a, b);
    };
    case 3: return function (a, b, c) {
      return fn.call(that, a, b, c);
    };
  }
  return function (/* ...args */) {
    return fn.apply(that, arguments);
  };
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_defined.js":
/*!*************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_defined.js ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// 7.2.1 RequireObjectCoercible(argument)
module.exports = function (it) {
  if (it == undefined) throw TypeError("Can't call method on  " + it);
  return it;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js":
/*!*****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// Thank's IE8 for his funny defineProperty
module.exports = !__webpack_require__(/*! ./_fails */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js")(function () {
  return Object.defineProperty({}, 'a', { get: function () { return 7; } }).a != 7;
});


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_dom-create.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_dom-create.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var isObject = __webpack_require__(/*! ./_is-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var document = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js").document;
// typeof document.createElement is 'object' in old IE
var is = isObject(document) && isObject(document.createElement);
module.exports = function (it) {
  return is ? document.createElement(it) : {};
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-bug-keys.js":
/*!*******************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-bug-keys.js ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// IE 8- don't enum bug keys
module.exports = (
  'constructor,hasOwnProperty,isPrototypeOf,propertyIsEnumerable,toLocaleString,toString,valueOf'
).split(',');


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-keys.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-keys.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// all enumerable object keys, includes symbols
var getKeys = __webpack_require__(/*! ./_object-keys */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js");
var gOPS = __webpack_require__(/*! ./_object-gops */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gops.js");
var pIE = __webpack_require__(/*! ./_object-pie */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js");
module.exports = function (it) {
  var result = getKeys(it);
  var getSymbols = gOPS.f;
  if (getSymbols) {
    var symbols = getSymbols(it);
    var isEnum = pIE.f;
    var i = 0;
    var key;
    while (symbols.length > i) if (isEnum.call(it, key = symbols[i++])) result.push(key);
  } return result;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js":
/*!************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js ***!
  \************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var core = __webpack_require__(/*! ./_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var ctx = __webpack_require__(/*! ./_ctx */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js");
var hide = __webpack_require__(/*! ./_hide */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js");
var has = __webpack_require__(/*! ./_has */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var PROTOTYPE = 'prototype';

var $export = function (type, name, source) {
  var IS_FORCED = type & $export.F;
  var IS_GLOBAL = type & $export.G;
  var IS_STATIC = type & $export.S;
  var IS_PROTO = type & $export.P;
  var IS_BIND = type & $export.B;
  var IS_WRAP = type & $export.W;
  var exports = IS_GLOBAL ? core : core[name] || (core[name] = {});
  var expProto = exports[PROTOTYPE];
  var target = IS_GLOBAL ? global : IS_STATIC ? global[name] : (global[name] || {})[PROTOTYPE];
  var key, own, out;
  if (IS_GLOBAL) source = name;
  for (key in source) {
    // contains in native
    own = !IS_FORCED && target && target[key] !== undefined;
    if (own && has(exports, key)) continue;
    // export native or passed
    out = own ? target[key] : source[key];
    // prevent global pollution for namespaces
    exports[key] = IS_GLOBAL && typeof target[key] != 'function' ? source[key]
    // bind timers to global for call from export context
    : IS_BIND && own ? ctx(out, global)
    // wrap global constructors for prevent change them in library
    : IS_WRAP && target[key] == out ? (function (C) {
      var F = function (a, b, c) {
        if (this instanceof C) {
          switch (arguments.length) {
            case 0: return new C();
            case 1: return new C(a);
            case 2: return new C(a, b);
          } return new C(a, b, c);
        } return C.apply(this, arguments);
      };
      F[PROTOTYPE] = C[PROTOTYPE];
      return F;
    // make static versions for prototype methods
    })(out) : IS_PROTO && typeof out == 'function' ? ctx(Function.call, out) : out;
    // export proto methods to core.%CONSTRUCTOR%.methods.%NAME%
    if (IS_PROTO) {
      (exports.virtual || (exports.virtual = {}))[key] = out;
      // export proto methods to core.%CONSTRUCTOR%.prototype.%NAME%
      if (type & $export.R && expProto && !expProto[key]) hide(expProto, key, out);
    }
  }
};
// type bitmap
$export.F = 1;   // forced
$export.G = 2;   // global
$export.S = 4;   // static
$export.P = 8;   // proto
$export.B = 16;  // bind
$export.W = 32;  // wrap
$export.U = 64;  // safe
$export.R = 128; // real proto method for `library`
module.exports = $export;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js":
/*!***********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (exec) {
  try {
    return !!exec();
  } catch (e) {
    return true;
  }
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_for-of.js":
/*!************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_for-of.js ***!
  \************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var ctx = __webpack_require__(/*! ./_ctx */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js");
var call = __webpack_require__(/*! ./_iter-call */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-call.js");
var isArrayIter = __webpack_require__(/*! ./_is-array-iter */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array-iter.js");
var anObject = __webpack_require__(/*! ./_an-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var toLength = __webpack_require__(/*! ./_to-length */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-length.js");
var getIterFn = __webpack_require__(/*! ./core.get-iterator-method */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/core.get-iterator-method.js");
var BREAK = {};
var RETURN = {};
var exports = module.exports = function (iterable, entries, fn, that, ITERATOR) {
  var iterFn = ITERATOR ? function () { return iterable; } : getIterFn(iterable);
  var f = ctx(fn, that, entries ? 2 : 1);
  var index = 0;
  var length, step, iterator, result;
  if (typeof iterFn != 'function') throw TypeError(iterable + ' is not iterable!');
  // fast case for arrays with default iterator
  if (isArrayIter(iterFn)) for (length = toLength(iterable.length); length > index; index++) {
    result = entries ? f(anObject(step = iterable[index])[0], step[1]) : f(iterable[index]);
    if (result === BREAK || result === RETURN) return result;
  } else for (iterator = iterFn.call(iterable); !(step = iterator.next()).done;) {
    result = call(iterator, f, step.value, entries);
    if (result === BREAK || result === RETURN) return result;
  }
};
exports.BREAK = BREAK;
exports.RETURN = RETURN;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js":
/*!************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js ***!
  \************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// https://github.com/zloirock/core-js/issues/86#issuecomment-115759028
var global = module.exports = typeof window != 'undefined' && window.Math == Math
  ? window : typeof self != 'undefined' && self.Math == Math ? self
  // eslint-disable-next-line no-new-func
  : Function('return this')();
if (typeof __g == 'number') __g = global; // eslint-disable-line no-undef


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js":
/*!*********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

var hasOwnProperty = {}.hasOwnProperty;
module.exports = function (it, key) {
  return hasOwnProperty.call(it, key);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js":
/*!**********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var dP = __webpack_require__(/*! ./_object-dp */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js");
var createDesc = __webpack_require__(/*! ./_property-desc */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js");
module.exports = __webpack_require__(/*! ./_descriptors */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js") ? function (object, key, value) {
  return dP.f(object, key, createDesc(1, value));
} : function (object, key, value) {
  object[key] = value;
  return object;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_html.js":
/*!**********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_html.js ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var document = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js").document;
module.exports = document && document.documentElement;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_ie8-dom-define.js":
/*!********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_ie8-dom-define.js ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = !__webpack_require__(/*! ./_descriptors */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js") && !__webpack_require__(/*! ./_fails */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js")(function () {
  return Object.defineProperty(__webpack_require__(/*! ./_dom-create */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_dom-create.js")('div'), 'a', { get: function () { return 7; } }).a != 7;
});


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_invoke.js":
/*!************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_invoke.js ***!
  \************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// fast apply, http://jsperf.lnkit.com/fast-apply/5
module.exports = function (fn, args, that) {
  var un = that === undefined;
  switch (args.length) {
    case 0: return un ? fn()
                      : fn.call(that);
    case 1: return un ? fn(args[0])
                      : fn.call(that, args[0]);
    case 2: return un ? fn(args[0], args[1])
                      : fn.call(that, args[0], args[1]);
    case 3: return un ? fn(args[0], args[1], args[2])
                      : fn.call(that, args[0], args[1], args[2]);
    case 4: return un ? fn(args[0], args[1], args[2], args[3])
                      : fn.call(that, args[0], args[1], args[2], args[3]);
  } return fn.apply(that, args);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iobject.js":
/*!*************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_iobject.js ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// fallback for non-array-like ES3 and non-enumerable old V8 strings
var cof = __webpack_require__(/*! ./_cof */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js");
// eslint-disable-next-line no-prototype-builtins
module.exports = Object('z').propertyIsEnumerable(0) ? Object : function (it) {
  return cof(it) == 'String' ? it.split('') : Object(it);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array-iter.js":
/*!*******************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array-iter.js ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// check on default Array iterator
var Iterators = __webpack_require__(/*! ./_iterators */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js");
var ITERATOR = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('iterator');
var ArrayProto = Array.prototype;

module.exports = function (it) {
  return it !== undefined && (Iterators.Array === it || ArrayProto[ITERATOR] === it);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array.js":
/*!**************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array.js ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 7.2.2 IsArray(argument)
var cof = __webpack_require__(/*! ./_cof */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js");
module.exports = Array.isArray || function isArray(arg) {
  return cof(arg) == 'Array';
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (it) {
  return typeof it === 'object' ? it !== null : typeof it === 'function';
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-call.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-call.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// call something on iterator step with safe closing on error
var anObject = __webpack_require__(/*! ./_an-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
module.exports = function (iterator, fn, value, entries) {
  try {
    return entries ? fn(anObject(value)[0], value[1]) : fn(value);
  // 7.4.6 IteratorClose(iterator, completion)
  } catch (e) {
    var ret = iterator['return'];
    if (ret !== undefined) anObject(ret.call(iterator));
    throw e;
  }
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-create.js":
/*!*****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-create.js ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var create = __webpack_require__(/*! ./_object-create */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-create.js");
var descriptor = __webpack_require__(/*! ./_property-desc */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js");
var setToStringTag = __webpack_require__(/*! ./_set-to-string-tag */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js");
var IteratorPrototype = {};

// 25.1.2.1.1 %IteratorPrototype%[@@iterator]()
__webpack_require__(/*! ./_hide */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js")(IteratorPrototype, __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('iterator'), function () { return this; });

module.exports = function (Constructor, NAME, next) {
  Constructor.prototype = create(IteratorPrototype, { next: descriptor(1, next) });
  setToStringTag(Constructor, NAME + ' Iterator');
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-define.js":
/*!*****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-define.js ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var LIBRARY = __webpack_require__(/*! ./_library */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js");
var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var redefine = __webpack_require__(/*! ./_redefine */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine.js");
var hide = __webpack_require__(/*! ./_hide */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js");
var Iterators = __webpack_require__(/*! ./_iterators */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js");
var $iterCreate = __webpack_require__(/*! ./_iter-create */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-create.js");
var setToStringTag = __webpack_require__(/*! ./_set-to-string-tag */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js");
var getPrototypeOf = __webpack_require__(/*! ./_object-gpo */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gpo.js");
var ITERATOR = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('iterator');
var BUGGY = !([].keys && 'next' in [].keys()); // Safari has buggy iterators w/o `next`
var FF_ITERATOR = '@@iterator';
var KEYS = 'keys';
var VALUES = 'values';

var returnThis = function () { return this; };

module.exports = function (Base, NAME, Constructor, next, DEFAULT, IS_SET, FORCED) {
  $iterCreate(Constructor, NAME, next);
  var getMethod = function (kind) {
    if (!BUGGY && kind in proto) return proto[kind];
    switch (kind) {
      case KEYS: return function keys() { return new Constructor(this, kind); };
      case VALUES: return function values() { return new Constructor(this, kind); };
    } return function entries() { return new Constructor(this, kind); };
  };
  var TAG = NAME + ' Iterator';
  var DEF_VALUES = DEFAULT == VALUES;
  var VALUES_BUG = false;
  var proto = Base.prototype;
  var $native = proto[ITERATOR] || proto[FF_ITERATOR] || DEFAULT && proto[DEFAULT];
  var $default = $native || getMethod(DEFAULT);
  var $entries = DEFAULT ? !DEF_VALUES ? $default : getMethod('entries') : undefined;
  var $anyNative = NAME == 'Array' ? proto.entries || $native : $native;
  var methods, key, IteratorPrototype;
  // Fix native
  if ($anyNative) {
    IteratorPrototype = getPrototypeOf($anyNative.call(new Base()));
    if (IteratorPrototype !== Object.prototype && IteratorPrototype.next) {
      // Set @@toStringTag to native iterators
      setToStringTag(IteratorPrototype, TAG, true);
      // fix for some old engines
      if (!LIBRARY && typeof IteratorPrototype[ITERATOR] != 'function') hide(IteratorPrototype, ITERATOR, returnThis);
    }
  }
  // fix Array#{values, @@iterator}.name in V8 / FF
  if (DEF_VALUES && $native && $native.name !== VALUES) {
    VALUES_BUG = true;
    $default = function values() { return $native.call(this); };
  }
  // Define iterator
  if ((!LIBRARY || FORCED) && (BUGGY || VALUES_BUG || !proto[ITERATOR])) {
    hide(proto, ITERATOR, $default);
  }
  // Plug for library
  Iterators[NAME] = $default;
  Iterators[TAG] = returnThis;
  if (DEFAULT) {
    methods = {
      values: DEF_VALUES ? $default : getMethod(VALUES),
      keys: IS_SET ? $default : getMethod(KEYS),
      entries: $entries
    };
    if (FORCED) for (key in methods) {
      if (!(key in proto)) redefine(proto, key, methods[key]);
    } else $export($export.P + $export.F * (BUGGY || VALUES_BUG), NAME, methods);
  }
  return methods;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-detect.js":
/*!*****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-detect.js ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var ITERATOR = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('iterator');
var SAFE_CLOSING = false;

try {
  var riter = [7][ITERATOR]();
  riter['return'] = function () { SAFE_CLOSING = true; };
  // eslint-disable-next-line no-throw-literal
  Array.from(riter, function () { throw 2; });
} catch (e) { /* empty */ }

module.exports = function (exec, skipClosing) {
  if (!skipClosing && !SAFE_CLOSING) return false;
  var safe = false;
  try {
    var arr = [7];
    var iter = arr[ITERATOR]();
    iter.next = function () { return { done: safe = true }; };
    arr[ITERATOR] = function () { return iter; };
    exec(arr);
  } catch (e) { /* empty */ }
  return safe;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-step.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-step.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (done, value) {
  return { value: value, done: !!done };
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = {};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js":
/*!*************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = true;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_meta.js":
/*!**********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_meta.js ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var META = __webpack_require__(/*! ./_uid */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js")('meta');
var isObject = __webpack_require__(/*! ./_is-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var has = __webpack_require__(/*! ./_has */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var setDesc = __webpack_require__(/*! ./_object-dp */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js").f;
var id = 0;
var isExtensible = Object.isExtensible || function () {
  return true;
};
var FREEZE = !__webpack_require__(/*! ./_fails */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js")(function () {
  return isExtensible(Object.preventExtensions({}));
});
var setMeta = function (it) {
  setDesc(it, META, { value: {
    i: 'O' + ++id, // object ID
    w: {}          // weak collections IDs
  } });
};
var fastKey = function (it, create) {
  // return primitive with prefix
  if (!isObject(it)) return typeof it == 'symbol' ? it : (typeof it == 'string' ? 'S' : 'P') + it;
  if (!has(it, META)) {
    // can't set metadata to uncaught frozen object
    if (!isExtensible(it)) return 'F';
    // not necessary to add metadata
    if (!create) return 'E';
    // add missing metadata
    setMeta(it);
  // return object ID
  } return it[META].i;
};
var getWeak = function (it, create) {
  if (!has(it, META)) {
    // can't set metadata to uncaught frozen object
    if (!isExtensible(it)) return true;
    // not necessary to add metadata
    if (!create) return false;
    // add missing metadata
    setMeta(it);
  // return hash weak collections IDs
  } return it[META].w;
};
// add metadata on freeze-family methods calling
var onFreeze = function (it) {
  if (FREEZE && meta.NEED && isExtensible(it) && !has(it, META)) setMeta(it);
  return it;
};
var meta = module.exports = {
  KEY: META,
  NEED: false,
  fastKey: fastKey,
  getWeak: getWeak,
  onFreeze: onFreeze
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_microtask.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_microtask.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var macrotask = __webpack_require__(/*! ./_task */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_task.js").set;
var Observer = global.MutationObserver || global.WebKitMutationObserver;
var process = global.process;
var Promise = global.Promise;
var isNode = __webpack_require__(/*! ./_cof */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js")(process) == 'process';

module.exports = function () {
  var head, last, notify;

  var flush = function () {
    var parent, fn;
    if (isNode && (parent = process.domain)) parent.exit();
    while (head) {
      fn = head.fn;
      head = head.next;
      try {
        fn();
      } catch (e) {
        if (head) notify();
        else last = undefined;
        throw e;
      }
    } last = undefined;
    if (parent) parent.enter();
  };

  // Node.js
  if (isNode) {
    notify = function () {
      process.nextTick(flush);
    };
  // browsers with MutationObserver, except iOS Safari - https://github.com/zloirock/core-js/issues/339
  } else if (Observer && !(global.navigator && global.navigator.standalone)) {
    var toggle = true;
    var node = document.createTextNode('');
    new Observer(flush).observe(node, { characterData: true }); // eslint-disable-line no-new
    notify = function () {
      node.data = toggle = !toggle;
    };
  // environments with maybe non-completely correct, but existent Promise
  } else if (Promise && Promise.resolve) {
    // Promise.resolve without an argument throws an error in LG WebOS 2
    var promise = Promise.resolve(undefined);
    notify = function () {
      promise.then(flush);
    };
  // for other environments - macrotask based on:
  // - setImmediate
  // - MessageChannel
  // - window.postMessag
  // - onreadystatechange
  // - setTimeout
  } else {
    notify = function () {
      // strange IE + webpack dev server bug - use .call(global)
      macrotask.call(global, flush);
    };
  }

  return function (fn) {
    var task = { fn: fn, next: undefined };
    if (last) last.next = task;
    if (!head) {
      head = task;
      notify();
    } last = task;
  };
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_new-promise-capability.js":
/*!****************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_new-promise-capability.js ***!
  \****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// 25.4.1.5 NewPromiseCapability(C)
var aFunction = __webpack_require__(/*! ./_a-function */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js");

function PromiseCapability(C) {
  var resolve, reject;
  this.promise = new C(function ($$resolve, $$reject) {
    if (resolve !== undefined || reject !== undefined) throw TypeError('Bad Promise constructor');
    resolve = $$resolve;
    reject = $$reject;
  });
  this.resolve = aFunction(resolve);
  this.reject = aFunction(reject);
}

module.exports.f = function (C) {
  return new PromiseCapability(C);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-assign.js":
/*!*******************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-assign.js ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// 19.1.2.1 Object.assign(target, source, ...)
var getKeys = __webpack_require__(/*! ./_object-keys */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js");
var gOPS = __webpack_require__(/*! ./_object-gops */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gops.js");
var pIE = __webpack_require__(/*! ./_object-pie */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js");
var toObject = __webpack_require__(/*! ./_to-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js");
var IObject = __webpack_require__(/*! ./_iobject */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iobject.js");
var $assign = Object.assign;

// should work with symbols and should have deterministic property order (V8 bug)
module.exports = !$assign || __webpack_require__(/*! ./_fails */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js")(function () {
  var A = {};
  var B = {};
  // eslint-disable-next-line no-undef
  var S = Symbol();
  var K = 'abcdefghijklmnopqrst';
  A[S] = 7;
  K.split('').forEach(function (k) { B[k] = k; });
  return $assign({}, A)[S] != 7 || Object.keys($assign({}, B)).join('') != K;
}) ? function assign(target, source) { // eslint-disable-line no-unused-vars
  var T = toObject(target);
  var aLen = arguments.length;
  var index = 1;
  var getSymbols = gOPS.f;
  var isEnum = pIE.f;
  while (aLen > index) {
    var S = IObject(arguments[index++]);
    var keys = getSymbols ? getKeys(S).concat(getSymbols(S)) : getKeys(S);
    var length = keys.length;
    var j = 0;
    var key;
    while (length > j) if (isEnum.call(S, key = keys[j++])) T[key] = S[key];
  } return T;
} : $assign;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-create.js":
/*!*******************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-create.js ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.2 / 15.2.3.5 Object.create(O [, Properties])
var anObject = __webpack_require__(/*! ./_an-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var dPs = __webpack_require__(/*! ./_object-dps */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dps.js");
var enumBugKeys = __webpack_require__(/*! ./_enum-bug-keys */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-bug-keys.js");
var IE_PROTO = __webpack_require__(/*! ./_shared-key */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_shared-key.js")('IE_PROTO');
var Empty = function () { /* empty */ };
var PROTOTYPE = 'prototype';

// Create object with fake `null` prototype: use iframe Object with cleared prototype
var createDict = function () {
  // Thrash, waste and sodomy: IE GC bug
  var iframe = __webpack_require__(/*! ./_dom-create */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_dom-create.js")('iframe');
  var i = enumBugKeys.length;
  var lt = '<';
  var gt = '>';
  var iframeDocument;
  iframe.style.display = 'none';
  __webpack_require__(/*! ./_html */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_html.js").appendChild(iframe);
  iframe.src = 'javascript:'; // eslint-disable-line no-script-url
  // createDict = iframe.contentWindow.Object;
  // html.removeChild(iframe);
  iframeDocument = iframe.contentWindow.document;
  iframeDocument.open();
  iframeDocument.write(lt + 'script' + gt + 'document.F=Object' + lt + '/script' + gt);
  iframeDocument.close();
  createDict = iframeDocument.F;
  while (i--) delete createDict[PROTOTYPE][enumBugKeys[i]];
  return createDict();
};

module.exports = Object.create || function create(O, Properties) {
  var result;
  if (O !== null) {
    Empty[PROTOTYPE] = anObject(O);
    result = new Empty();
    Empty[PROTOTYPE] = null;
    // add "__proto__" for Object.getPrototypeOf polyfill
    result[IE_PROTO] = O;
  } else result = createDict();
  return Properties === undefined ? result : dPs(result, Properties);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var anObject = __webpack_require__(/*! ./_an-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var IE8_DOM_DEFINE = __webpack_require__(/*! ./_ie8-dom-define */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_ie8-dom-define.js");
var toPrimitive = __webpack_require__(/*! ./_to-primitive */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-primitive.js");
var dP = Object.defineProperty;

exports.f = __webpack_require__(/*! ./_descriptors */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js") ? Object.defineProperty : function defineProperty(O, P, Attributes) {
  anObject(O);
  P = toPrimitive(P, true);
  anObject(Attributes);
  if (IE8_DOM_DEFINE) try {
    return dP(O, P, Attributes);
  } catch (e) { /* empty */ }
  if ('get' in Attributes || 'set' in Attributes) throw TypeError('Accessors not supported!');
  if ('value' in Attributes) O[P] = Attributes.value;
  return O;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dps.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dps.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var dP = __webpack_require__(/*! ./_object-dp */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js");
var anObject = __webpack_require__(/*! ./_an-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var getKeys = __webpack_require__(/*! ./_object-keys */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js");

module.exports = __webpack_require__(/*! ./_descriptors */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js") ? Object.defineProperties : function defineProperties(O, Properties) {
  anObject(O);
  var keys = getKeys(Properties);
  var length = keys.length;
  var i = 0;
  var P;
  while (length > i) dP.f(O, P = keys[i++], Properties[P]);
  return O;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopd.js":
/*!*****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopd.js ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var pIE = __webpack_require__(/*! ./_object-pie */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js");
var createDesc = __webpack_require__(/*! ./_property-desc */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js");
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");
var toPrimitive = __webpack_require__(/*! ./_to-primitive */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-primitive.js");
var has = __webpack_require__(/*! ./_has */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var IE8_DOM_DEFINE = __webpack_require__(/*! ./_ie8-dom-define */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_ie8-dom-define.js");
var gOPD = Object.getOwnPropertyDescriptor;

exports.f = __webpack_require__(/*! ./_descriptors */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js") ? gOPD : function getOwnPropertyDescriptor(O, P) {
  O = toIObject(O);
  P = toPrimitive(P, true);
  if (IE8_DOM_DEFINE) try {
    return gOPD(O, P);
  } catch (e) { /* empty */ }
  if (has(O, P)) return createDesc(!pIE.f.call(O, P), O[P]);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn-ext.js":
/*!*********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn-ext.js ***!
  \*********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// fallback for IE11 buggy Object.getOwnPropertyNames with iframe and window
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");
var gOPN = __webpack_require__(/*! ./_object-gopn */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn.js").f;
var toString = {}.toString;

var windowNames = typeof window == 'object' && window && Object.getOwnPropertyNames
  ? Object.getOwnPropertyNames(window) : [];

var getWindowNames = function (it) {
  try {
    return gOPN(it);
  } catch (e) {
    return windowNames.slice();
  }
};

module.exports.f = function getOwnPropertyNames(it) {
  return windowNames && toString.call(it) == '[object Window]' ? getWindowNames(it) : gOPN(toIObject(it));
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn.js":
/*!*****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn.js ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.7 / 15.2.3.4 Object.getOwnPropertyNames(O)
var $keys = __webpack_require__(/*! ./_object-keys-internal */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys-internal.js");
var hiddenKeys = __webpack_require__(/*! ./_enum-bug-keys */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-bug-keys.js").concat('length', 'prototype');

exports.f = Object.getOwnPropertyNames || function getOwnPropertyNames(O) {
  return $keys(O, hiddenKeys);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gops.js":
/*!*****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gops.js ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

exports.f = Object.getOwnPropertySymbols;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gpo.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gpo.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.9 / 15.2.3.2 Object.getPrototypeOf(O)
var has = __webpack_require__(/*! ./_has */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var toObject = __webpack_require__(/*! ./_to-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js");
var IE_PROTO = __webpack_require__(/*! ./_shared-key */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_shared-key.js")('IE_PROTO');
var ObjectProto = Object.prototype;

module.exports = Object.getPrototypeOf || function (O) {
  O = toObject(O);
  if (has(O, IE_PROTO)) return O[IE_PROTO];
  if (typeof O.constructor == 'function' && O instanceof O.constructor) {
    return O.constructor.prototype;
  } return O instanceof Object ? ObjectProto : null;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys-internal.js":
/*!**************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys-internal.js ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var has = __webpack_require__(/*! ./_has */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");
var arrayIndexOf = __webpack_require__(/*! ./_array-includes */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_array-includes.js")(false);
var IE_PROTO = __webpack_require__(/*! ./_shared-key */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_shared-key.js")('IE_PROTO');

module.exports = function (object, names) {
  var O = toIObject(object);
  var i = 0;
  var result = [];
  var key;
  for (key in O) if (key != IE_PROTO) has(O, key) && result.push(key);
  // Don't enum bug & hidden keys
  while (names.length > i) if (has(O, key = names[i++])) {
    ~arrayIndexOf(result, key) || result.push(key);
  }
  return result;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js":
/*!*****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.14 / 15.2.3.14 Object.keys(O)
var $keys = __webpack_require__(/*! ./_object-keys-internal */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys-internal.js");
var enumBugKeys = __webpack_require__(/*! ./_enum-bug-keys */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-bug-keys.js");

module.exports = Object.keys || function keys(O) {
  return $keys(O, enumBugKeys);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

exports.f = {}.propertyIsEnumerable;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-sap.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-sap.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// most Object methods by ES6 should accept primitives
var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var core = __webpack_require__(/*! ./_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var fails = __webpack_require__(/*! ./_fails */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js");
module.exports = function (KEY, exec) {
  var fn = (core.Object || {})[KEY] || Object[KEY];
  var exp = {};
  exp[KEY] = exec(fn);
  $export($export.S + $export.F * fails(function () { fn(1); }), 'Object', exp);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_perform.js":
/*!*************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_perform.js ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (exec) {
  try {
    return { e: false, v: exec() };
  } catch (e) {
    return { e: true, v: e };
  }
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_promise-resolve.js":
/*!*********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_promise-resolve.js ***!
  \*********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var anObject = __webpack_require__(/*! ./_an-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var isObject = __webpack_require__(/*! ./_is-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var newPromiseCapability = __webpack_require__(/*! ./_new-promise-capability */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_new-promise-capability.js");

module.exports = function (C, x) {
  anObject(C);
  if (isObject(x) && x.constructor === C) return x;
  var promiseCapability = newPromiseCapability.f(C);
  var resolve = promiseCapability.resolve;
  resolve(x);
  return promiseCapability.promise;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js":
/*!*******************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (bitmap, value) {
  return {
    enumerable: !(bitmap & 1),
    configurable: !(bitmap & 2),
    writable: !(bitmap & 4),
    value: value
  };
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine-all.js":
/*!******************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine-all.js ***!
  \******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var hide = __webpack_require__(/*! ./_hide */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js");
module.exports = function (target, src, safe) {
  for (var key in src) {
    if (safe && target[key]) target[key] = src[key];
    else hide(target, key, src[key]);
  } return target;
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine.js":
/*!**************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine.js ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! ./_hide */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js");


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-proto.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-proto.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// Works with __proto__ only. Old v8 can't work with null proto objects.
/* eslint-disable no-proto */
var isObject = __webpack_require__(/*! ./_is-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var anObject = __webpack_require__(/*! ./_an-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var check = function (O, proto) {
  anObject(O);
  if (!isObject(proto) && proto !== null) throw TypeError(proto + ": can't set as prototype!");
};
module.exports = {
  set: Object.setPrototypeOf || ('__proto__' in {} ? // eslint-disable-line
    function (test, buggy, set) {
      try {
        set = __webpack_require__(/*! ./_ctx */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js")(Function.call, __webpack_require__(/*! ./_object-gopd */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopd.js").f(Object.prototype, '__proto__').set, 2);
        set(test, []);
        buggy = !(test instanceof Array);
      } catch (e) { buggy = true; }
      return function setPrototypeOf(O, proto) {
        check(O, proto);
        if (buggy) O.__proto__ = proto;
        else set(O, proto);
        return O;
      };
    }({}, false) : undefined),
  check: check
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-species.js":
/*!*****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-species.js ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var core = __webpack_require__(/*! ./_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var dP = __webpack_require__(/*! ./_object-dp */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js");
var DESCRIPTORS = __webpack_require__(/*! ./_descriptors */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js");
var SPECIES = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('species');

module.exports = function (KEY) {
  var C = typeof core[KEY] == 'function' ? core[KEY] : global[KEY];
  if (DESCRIPTORS && C && !C[SPECIES]) dP.f(C, SPECIES, {
    configurable: true,
    get: function () { return this; }
  });
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js":
/*!***********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var def = __webpack_require__(/*! ./_object-dp */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js").f;
var has = __webpack_require__(/*! ./_has */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var TAG = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('toStringTag');

module.exports = function (it, tag, stat) {
  if (it && !has(it = stat ? it : it.prototype, TAG)) def(it, TAG, { configurable: true, value: tag });
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_shared-key.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_shared-key.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var shared = __webpack_require__(/*! ./_shared */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_shared.js")('keys');
var uid = __webpack_require__(/*! ./_uid */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js");
module.exports = function (key) {
  return shared[key] || (shared[key] = uid(key));
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_shared.js":
/*!************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_shared.js ***!
  \************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var core = __webpack_require__(/*! ./_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var SHARED = '__core-js_shared__';
var store = global[SHARED] || (global[SHARED] = {});

(module.exports = function (key, value) {
  return store[key] || (store[key] = value !== undefined ? value : {});
})('versions', []).push({
  version: core.version,
  mode: __webpack_require__(/*! ./_library */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js") ? 'pure' : 'global',
  copyright: '© 2018 Denis Pushkarev (zloirock.ru)'
});


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_species-constructor.js":
/*!*************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_species-constructor.js ***!
  \*************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 7.3.20 SpeciesConstructor(O, defaultConstructor)
var anObject = __webpack_require__(/*! ./_an-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var aFunction = __webpack_require__(/*! ./_a-function */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js");
var SPECIES = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('species');
module.exports = function (O, D) {
  var C = anObject(O).constructor;
  var S;
  return C === undefined || (S = anObject(C)[SPECIES]) == undefined ? D : aFunction(S);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_string-at.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_string-at.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var toInteger = __webpack_require__(/*! ./_to-integer */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-integer.js");
var defined = __webpack_require__(/*! ./_defined */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_defined.js");
// true  -> String#at
// false -> String#codePointAt
module.exports = function (TO_STRING) {
  return function (that, pos) {
    var s = String(defined(that));
    var i = toInteger(pos);
    var l = s.length;
    var a, b;
    if (i < 0 || i >= l) return TO_STRING ? '' : undefined;
    a = s.charCodeAt(i);
    return a < 0xd800 || a > 0xdbff || i + 1 === l || (b = s.charCodeAt(i + 1)) < 0xdc00 || b > 0xdfff
      ? TO_STRING ? s.charAt(i) : a
      : TO_STRING ? s.slice(i, i + 2) : (a - 0xd800 << 10) + (b - 0xdc00) + 0x10000;
  };
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_task.js":
/*!**********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_task.js ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var ctx = __webpack_require__(/*! ./_ctx */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js");
var invoke = __webpack_require__(/*! ./_invoke */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_invoke.js");
var html = __webpack_require__(/*! ./_html */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_html.js");
var cel = __webpack_require__(/*! ./_dom-create */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_dom-create.js");
var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var process = global.process;
var setTask = global.setImmediate;
var clearTask = global.clearImmediate;
var MessageChannel = global.MessageChannel;
var Dispatch = global.Dispatch;
var counter = 0;
var queue = {};
var ONREADYSTATECHANGE = 'onreadystatechange';
var defer, channel, port;
var run = function () {
  var id = +this;
  // eslint-disable-next-line no-prototype-builtins
  if (queue.hasOwnProperty(id)) {
    var fn = queue[id];
    delete queue[id];
    fn();
  }
};
var listener = function (event) {
  run.call(event.data);
};
// Node.js 0.9+ & IE10+ has setImmediate, otherwise:
if (!setTask || !clearTask) {
  setTask = function setImmediate(fn) {
    var args = [];
    var i = 1;
    while (arguments.length > i) args.push(arguments[i++]);
    queue[++counter] = function () {
      // eslint-disable-next-line no-new-func
      invoke(typeof fn == 'function' ? fn : Function(fn), args);
    };
    defer(counter);
    return counter;
  };
  clearTask = function clearImmediate(id) {
    delete queue[id];
  };
  // Node.js 0.8-
  if (__webpack_require__(/*! ./_cof */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js")(process) == 'process') {
    defer = function (id) {
      process.nextTick(ctx(run, id, 1));
    };
  // Sphere (JS game engine) Dispatch API
  } else if (Dispatch && Dispatch.now) {
    defer = function (id) {
      Dispatch.now(ctx(run, id, 1));
    };
  // Browsers with MessageChannel, includes WebWorkers
  } else if (MessageChannel) {
    channel = new MessageChannel();
    port = channel.port2;
    channel.port1.onmessage = listener;
    defer = ctx(port.postMessage, port, 1);
  // Browsers with postMessage, skip WebWorkers
  // IE8 has postMessage, but it's sync & typeof its postMessage is 'object'
  } else if (global.addEventListener && typeof postMessage == 'function' && !global.importScripts) {
    defer = function (id) {
      global.postMessage(id + '', '*');
    };
    global.addEventListener('message', listener, false);
  // IE8-
  } else if (ONREADYSTATECHANGE in cel('script')) {
    defer = function (id) {
      html.appendChild(cel('script'))[ONREADYSTATECHANGE] = function () {
        html.removeChild(this);
        run.call(id);
      };
    };
  // Rest old browsers
  } else {
    defer = function (id) {
      setTimeout(ctx(run, id, 1), 0);
    };
  }
}
module.exports = {
  set: setTask,
  clear: clearTask
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-absolute-index.js":
/*!***********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-absolute-index.js ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var toInteger = __webpack_require__(/*! ./_to-integer */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-integer.js");
var max = Math.max;
var min = Math.min;
module.exports = function (index, length) {
  index = toInteger(index);
  return index < 0 ? max(index + length, 0) : min(index, length);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-integer.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-integer.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// 7.1.4 ToInteger
var ceil = Math.ceil;
var floor = Math.floor;
module.exports = function (it) {
  return isNaN(it = +it) ? 0 : (it > 0 ? floor : ceil)(it);
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// to indexed object, toObject with fallback for non-array-like ES3 strings
var IObject = __webpack_require__(/*! ./_iobject */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iobject.js");
var defined = __webpack_require__(/*! ./_defined */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_defined.js");
module.exports = function (it) {
  return IObject(defined(it));
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-length.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-length.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 7.1.15 ToLength
var toInteger = __webpack_require__(/*! ./_to-integer */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-integer.js");
var min = Math.min;
module.exports = function (it) {
  return it > 0 ? min(toInteger(it), 0x1fffffffffffff) : 0; // pow(2, 53) - 1 == 9007199254740991
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 7.1.13 ToObject(argument)
var defined = __webpack_require__(/*! ./_defined */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_defined.js");
module.exports = function (it) {
  return Object(defined(it));
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-primitive.js":
/*!******************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-primitive.js ***!
  \******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 7.1.1 ToPrimitive(input [, PreferredType])
var isObject = __webpack_require__(/*! ./_is-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
// instead of the ES6 spec version, we didn't implement @@toPrimitive case
// and the second argument - flag - preferred type is a string
module.exports = function (it, S) {
  if (!isObject(it)) return it;
  var fn, val;
  if (S && typeof (fn = it.toString) == 'function' && !isObject(val = fn.call(it))) return val;
  if (typeof (fn = it.valueOf) == 'function' && !isObject(val = fn.call(it))) return val;
  if (!S && typeof (fn = it.toString) == 'function' && !isObject(val = fn.call(it))) return val;
  throw TypeError("Can't convert object to primitive value");
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js":
/*!*********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

var id = 0;
var px = Math.random();
module.exports = function (key) {
  return 'Symbol('.concat(key === undefined ? '' : key, ')_', (++id + px).toString(36));
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_user-agent.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_user-agent.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var navigator = global.navigator;

module.exports = navigator && navigator.userAgent || '';


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-define.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-define.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var core = __webpack_require__(/*! ./_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var LIBRARY = __webpack_require__(/*! ./_library */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js");
var wksExt = __webpack_require__(/*! ./_wks-ext */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-ext.js");
var defineProperty = __webpack_require__(/*! ./_object-dp */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js").f;
module.exports = function (name) {
  var $Symbol = core.Symbol || (core.Symbol = LIBRARY ? {} : global.Symbol || {});
  if (name.charAt(0) != '_' && !(name in $Symbol)) defineProperty($Symbol, name, { value: wksExt.f(name) });
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-ext.js":
/*!*************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-ext.js ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

exports.f = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js");


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js":
/*!*********************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var store = __webpack_require__(/*! ./_shared */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_shared.js")('wks');
var uid = __webpack_require__(/*! ./_uid */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js");
var Symbol = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js").Symbol;
var USE_SYMBOL = typeof Symbol == 'function';

var $exports = module.exports = function (name) {
  return store[name] || (store[name] =
    USE_SYMBOL && Symbol[name] || (USE_SYMBOL ? Symbol : uid)('Symbol.' + name));
};

$exports.store = store;


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/core.get-iterator-method.js":
/*!*****************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/core.get-iterator-method.js ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var classof = __webpack_require__(/*! ./_classof */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_classof.js");
var ITERATOR = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('iterator');
var Iterators = __webpack_require__(/*! ./_iterators */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js");
module.exports = __webpack_require__(/*! ./_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").getIteratorMethod = function (it) {
  if (it != undefined) return it[ITERATOR]
    || it['@@iterator']
    || Iterators[classof(it)];
};


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.from.js":
/*!*******************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.from.js ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var ctx = __webpack_require__(/*! ./_ctx */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js");
var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var toObject = __webpack_require__(/*! ./_to-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js");
var call = __webpack_require__(/*! ./_iter-call */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-call.js");
var isArrayIter = __webpack_require__(/*! ./_is-array-iter */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array-iter.js");
var toLength = __webpack_require__(/*! ./_to-length */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-length.js");
var createProperty = __webpack_require__(/*! ./_create-property */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_create-property.js");
var getIterFn = __webpack_require__(/*! ./core.get-iterator-method */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/core.get-iterator-method.js");

$export($export.S + $export.F * !__webpack_require__(/*! ./_iter-detect */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-detect.js")(function (iter) { Array.from(iter); }), 'Array', {
  // 22.1.2.1 Array.from(arrayLike, mapfn = undefined, thisArg = undefined)
  from: function from(arrayLike /* , mapfn = undefined, thisArg = undefined */) {
    var O = toObject(arrayLike);
    var C = typeof this == 'function' ? this : Array;
    var aLen = arguments.length;
    var mapfn = aLen > 1 ? arguments[1] : undefined;
    var mapping = mapfn !== undefined;
    var index = 0;
    var iterFn = getIterFn(O);
    var length, result, step, iterator;
    if (mapping) mapfn = ctx(mapfn, aLen > 2 ? arguments[2] : undefined, 2);
    // if object isn't iterable or it's array with default iterator - use simple case
    if (iterFn != undefined && !(C == Array && isArrayIter(iterFn))) {
      for (iterator = iterFn.call(O), result = new C(); !(step = iterator.next()).done; index++) {
        createProperty(result, index, mapping ? call(iterator, mapfn, [step.value, index], true) : step.value);
      }
    } else {
      length = toLength(O.length);
      for (result = new C(length); length > index; index++) {
        createProperty(result, index, mapping ? mapfn(O[index], index) : O[index]);
      }
    }
    result.length = index;
    return result;
  }
});


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.iterator.js":
/*!***********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.iterator.js ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var addToUnscopables = __webpack_require__(/*! ./_add-to-unscopables */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_add-to-unscopables.js");
var step = __webpack_require__(/*! ./_iter-step */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-step.js");
var Iterators = __webpack_require__(/*! ./_iterators */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js");
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");

// 22.1.3.4 Array.prototype.entries()
// 22.1.3.13 Array.prototype.keys()
// 22.1.3.29 Array.prototype.values()
// 22.1.3.30 Array.prototype[@@iterator]()
module.exports = __webpack_require__(/*! ./_iter-define */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-define.js")(Array, 'Array', function (iterated, kind) {
  this._t = toIObject(iterated); // target
  this._i = 0;                   // next index
  this._k = kind;                // kind
// 22.1.5.2.1 %ArrayIteratorPrototype%.next()
}, function () {
  var O = this._t;
  var kind = this._k;
  var index = this._i++;
  if (!O || index >= O.length) {
    this._t = undefined;
    return step(1);
  }
  if (kind == 'keys') return step(0, index);
  if (kind == 'values') return step(0, O[index]);
  return step(0, [index, O[index]]);
}, 'values');

// argumentsList[@@iterator] is %ArrayProto_values% (9.4.4.6, 9.4.4.7)
Iterators.Arguments = Iterators.Array;

addToUnscopables('keys');
addToUnscopables('values');
addToUnscopables('entries');


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.assign.js":
/*!**********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.assign.js ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.3.1 Object.assign(target, source)
var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");

$export($export.S + $export.F, 'Object', { assign: __webpack_require__(/*! ./_object-assign */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-assign.js") });


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.create.js":
/*!**********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.create.js ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
// 19.1.2.2 / 15.2.3.5 Object.create(O [, Properties])
$export($export.S, 'Object', { create: __webpack_require__(/*! ./_object-create */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-create.js") });


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.define-property.js":
/*!*******************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.define-property.js ***!
  \*******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
// 19.1.2.4 / 15.2.3.6 Object.defineProperty(O, P, Attributes)
$export($export.S + $export.F * !__webpack_require__(/*! ./_descriptors */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js"), 'Object', { defineProperty: __webpack_require__(/*! ./_object-dp */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js").f });


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.get-prototype-of.js":
/*!********************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.get-prototype-of.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.9 Object.getPrototypeOf(O)
var toObject = __webpack_require__(/*! ./_to-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js");
var $getPrototypeOf = __webpack_require__(/*! ./_object-gpo */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gpo.js");

__webpack_require__(/*! ./_object-sap */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-sap.js")('getPrototypeOf', function () {
  return function getPrototypeOf(it) {
    return $getPrototypeOf(toObject(it));
  };
});


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.set-prototype-of.js":
/*!********************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.set-prototype-of.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.3.19 Object.setPrototypeOf(O, proto)
var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
$export($export.S, 'Object', { setPrototypeOf: __webpack_require__(/*! ./_set-proto */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-proto.js").set });


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.to-string.js":
/*!*************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.to-string.js ***!
  \*************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {



/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.promise.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.promise.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var LIBRARY = __webpack_require__(/*! ./_library */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js");
var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var ctx = __webpack_require__(/*! ./_ctx */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js");
var classof = __webpack_require__(/*! ./_classof */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_classof.js");
var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var isObject = __webpack_require__(/*! ./_is-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var aFunction = __webpack_require__(/*! ./_a-function */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js");
var anInstance = __webpack_require__(/*! ./_an-instance */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-instance.js");
var forOf = __webpack_require__(/*! ./_for-of */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_for-of.js");
var speciesConstructor = __webpack_require__(/*! ./_species-constructor */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_species-constructor.js");
var task = __webpack_require__(/*! ./_task */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_task.js").set;
var microtask = __webpack_require__(/*! ./_microtask */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_microtask.js")();
var newPromiseCapabilityModule = __webpack_require__(/*! ./_new-promise-capability */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_new-promise-capability.js");
var perform = __webpack_require__(/*! ./_perform */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_perform.js");
var userAgent = __webpack_require__(/*! ./_user-agent */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_user-agent.js");
var promiseResolve = __webpack_require__(/*! ./_promise-resolve */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_promise-resolve.js");
var PROMISE = 'Promise';
var TypeError = global.TypeError;
var process = global.process;
var versions = process && process.versions;
var v8 = versions && versions.v8 || '';
var $Promise = global[PROMISE];
var isNode = classof(process) == 'process';
var empty = function () { /* empty */ };
var Internal, newGenericPromiseCapability, OwnPromiseCapability, Wrapper;
var newPromiseCapability = newGenericPromiseCapability = newPromiseCapabilityModule.f;

var USE_NATIVE = !!function () {
  try {
    // correct subclassing with @@species support
    var promise = $Promise.resolve(1);
    var FakePromise = (promise.constructor = {})[__webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('species')] = function (exec) {
      exec(empty, empty);
    };
    // unhandled rejections tracking support, NodeJS Promise without it fails @@species test
    return (isNode || typeof PromiseRejectionEvent == 'function')
      && promise.then(empty) instanceof FakePromise
      // v8 6.6 (Node 10 and Chrome 66) have a bug with resolving custom thenables
      // https://bugs.chromium.org/p/chromium/issues/detail?id=830565
      // we can't detect it synchronously, so just check versions
      && v8.indexOf('6.6') !== 0
      && userAgent.indexOf('Chrome/66') === -1;
  } catch (e) { /* empty */ }
}();

// helpers
var isThenable = function (it) {
  var then;
  return isObject(it) && typeof (then = it.then) == 'function' ? then : false;
};
var notify = function (promise, isReject) {
  if (promise._n) return;
  promise._n = true;
  var chain = promise._c;
  microtask(function () {
    var value = promise._v;
    var ok = promise._s == 1;
    var i = 0;
    var run = function (reaction) {
      var handler = ok ? reaction.ok : reaction.fail;
      var resolve = reaction.resolve;
      var reject = reaction.reject;
      var domain = reaction.domain;
      var result, then, exited;
      try {
        if (handler) {
          if (!ok) {
            if (promise._h == 2) onHandleUnhandled(promise);
            promise._h = 1;
          }
          if (handler === true) result = value;
          else {
            if (domain) domain.enter();
            result = handler(value); // may throw
            if (domain) {
              domain.exit();
              exited = true;
            }
          }
          if (result === reaction.promise) {
            reject(TypeError('Promise-chain cycle'));
          } else if (then = isThenable(result)) {
            then.call(result, resolve, reject);
          } else resolve(result);
        } else reject(value);
      } catch (e) {
        if (domain && !exited) domain.exit();
        reject(e);
      }
    };
    while (chain.length > i) run(chain[i++]); // variable length - can't use forEach
    promise._c = [];
    promise._n = false;
    if (isReject && !promise._h) onUnhandled(promise);
  });
};
var onUnhandled = function (promise) {
  task.call(global, function () {
    var value = promise._v;
    var unhandled = isUnhandled(promise);
    var result, handler, console;
    if (unhandled) {
      result = perform(function () {
        if (isNode) {
          process.emit('unhandledRejection', value, promise);
        } else if (handler = global.onunhandledrejection) {
          handler({ promise: promise, reason: value });
        } else if ((console = global.console) && console.error) {
          console.error('Unhandled promise rejection', value);
        }
      });
      // Browsers should not trigger `rejectionHandled` event if it was handled here, NodeJS - should
      promise._h = isNode || isUnhandled(promise) ? 2 : 1;
    } promise._a = undefined;
    if (unhandled && result.e) throw result.v;
  });
};
var isUnhandled = function (promise) {
  return promise._h !== 1 && (promise._a || promise._c).length === 0;
};
var onHandleUnhandled = function (promise) {
  task.call(global, function () {
    var handler;
    if (isNode) {
      process.emit('rejectionHandled', promise);
    } else if (handler = global.onrejectionhandled) {
      handler({ promise: promise, reason: promise._v });
    }
  });
};
var $reject = function (value) {
  var promise = this;
  if (promise._d) return;
  promise._d = true;
  promise = promise._w || promise; // unwrap
  promise._v = value;
  promise._s = 2;
  if (!promise._a) promise._a = promise._c.slice();
  notify(promise, true);
};
var $resolve = function (value) {
  var promise = this;
  var then;
  if (promise._d) return;
  promise._d = true;
  promise = promise._w || promise; // unwrap
  try {
    if (promise === value) throw TypeError("Promise can't be resolved itself");
    if (then = isThenable(value)) {
      microtask(function () {
        var wrapper = { _w: promise, _d: false }; // wrap
        try {
          then.call(value, ctx($resolve, wrapper, 1), ctx($reject, wrapper, 1));
        } catch (e) {
          $reject.call(wrapper, e);
        }
      });
    } else {
      promise._v = value;
      promise._s = 1;
      notify(promise, false);
    }
  } catch (e) {
    $reject.call({ _w: promise, _d: false }, e); // wrap
  }
};

// constructor polyfill
if (!USE_NATIVE) {
  // 25.4.3.1 Promise(executor)
  $Promise = function Promise(executor) {
    anInstance(this, $Promise, PROMISE, '_h');
    aFunction(executor);
    Internal.call(this);
    try {
      executor(ctx($resolve, this, 1), ctx($reject, this, 1));
    } catch (err) {
      $reject.call(this, err);
    }
  };
  // eslint-disable-next-line no-unused-vars
  Internal = function Promise(executor) {
    this._c = [];             // <- awaiting reactions
    this._a = undefined;      // <- checked in isUnhandled reactions
    this._s = 0;              // <- state
    this._d = false;          // <- done
    this._v = undefined;      // <- value
    this._h = 0;              // <- rejection state, 0 - default, 1 - handled, 2 - unhandled
    this._n = false;          // <- notify
  };
  Internal.prototype = __webpack_require__(/*! ./_redefine-all */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine-all.js")($Promise.prototype, {
    // 25.4.5.3 Promise.prototype.then(onFulfilled, onRejected)
    then: function then(onFulfilled, onRejected) {
      var reaction = newPromiseCapability(speciesConstructor(this, $Promise));
      reaction.ok = typeof onFulfilled == 'function' ? onFulfilled : true;
      reaction.fail = typeof onRejected == 'function' && onRejected;
      reaction.domain = isNode ? process.domain : undefined;
      this._c.push(reaction);
      if (this._a) this._a.push(reaction);
      if (this._s) notify(this, false);
      return reaction.promise;
    },
    // 25.4.5.1 Promise.prototype.catch(onRejected)
    'catch': function (onRejected) {
      return this.then(undefined, onRejected);
    }
  });
  OwnPromiseCapability = function () {
    var promise = new Internal();
    this.promise = promise;
    this.resolve = ctx($resolve, promise, 1);
    this.reject = ctx($reject, promise, 1);
  };
  newPromiseCapabilityModule.f = newPromiseCapability = function (C) {
    return C === $Promise || C === Wrapper
      ? new OwnPromiseCapability(C)
      : newGenericPromiseCapability(C);
  };
}

$export($export.G + $export.W + $export.F * !USE_NATIVE, { Promise: $Promise });
__webpack_require__(/*! ./_set-to-string-tag */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js")($Promise, PROMISE);
__webpack_require__(/*! ./_set-species */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-species.js")(PROMISE);
Wrapper = __webpack_require__(/*! ./_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js")[PROMISE];

// statics
$export($export.S + $export.F * !USE_NATIVE, PROMISE, {
  // 25.4.4.5 Promise.reject(r)
  reject: function reject(r) {
    var capability = newPromiseCapability(this);
    var $$reject = capability.reject;
    $$reject(r);
    return capability.promise;
  }
});
$export($export.S + $export.F * (LIBRARY || !USE_NATIVE), PROMISE, {
  // 25.4.4.6 Promise.resolve(x)
  resolve: function resolve(x) {
    return promiseResolve(LIBRARY && this === Wrapper ? $Promise : this, x);
  }
});
$export($export.S + $export.F * !(USE_NATIVE && __webpack_require__(/*! ./_iter-detect */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-detect.js")(function (iter) {
  $Promise.all(iter)['catch'](empty);
})), PROMISE, {
  // 25.4.4.1 Promise.all(iterable)
  all: function all(iterable) {
    var C = this;
    var capability = newPromiseCapability(C);
    var resolve = capability.resolve;
    var reject = capability.reject;
    var result = perform(function () {
      var values = [];
      var index = 0;
      var remaining = 1;
      forOf(iterable, false, function (promise) {
        var $index = index++;
        var alreadyCalled = false;
        values.push(undefined);
        remaining++;
        C.resolve(promise).then(function (value) {
          if (alreadyCalled) return;
          alreadyCalled = true;
          values[$index] = value;
          --remaining || resolve(values);
        }, reject);
      });
      --remaining || resolve(values);
    });
    if (result.e) reject(result.v);
    return capability.promise;
  },
  // 25.4.4.4 Promise.race(iterable)
  race: function race(iterable) {
    var C = this;
    var capability = newPromiseCapability(C);
    var reject = capability.reject;
    var result = perform(function () {
      forOf(iterable, false, function (promise) {
        C.resolve(promise).then(capability.resolve, reject);
      });
    });
    if (result.e) reject(result.v);
    return capability.promise;
  }
});


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.string.iterator.js":
/*!************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.string.iterator.js ***!
  \************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $at = __webpack_require__(/*! ./_string-at */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_string-at.js")(true);

// 21.1.3.27 String.prototype[@@iterator]()
__webpack_require__(/*! ./_iter-define */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-define.js")(String, 'String', function (iterated) {
  this._t = String(iterated); // target
  this._i = 0;                // next index
// 21.1.5.2.1 %StringIteratorPrototype%.next()
}, function () {
  var O = this._t;
  var index = this._i;
  var point;
  if (index >= O.length) return { value: undefined, done: true };
  point = $at(O, index);
  this._i += point.length;
  return { value: point, done: false };
});


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.symbol.js":
/*!***************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.symbol.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// ECMAScript 6 symbols shim
var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var has = __webpack_require__(/*! ./_has */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var DESCRIPTORS = __webpack_require__(/*! ./_descriptors */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js");
var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var redefine = __webpack_require__(/*! ./_redefine */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine.js");
var META = __webpack_require__(/*! ./_meta */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_meta.js").KEY;
var $fails = __webpack_require__(/*! ./_fails */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js");
var shared = __webpack_require__(/*! ./_shared */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_shared.js");
var setToStringTag = __webpack_require__(/*! ./_set-to-string-tag */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js");
var uid = __webpack_require__(/*! ./_uid */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js");
var wks = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js");
var wksExt = __webpack_require__(/*! ./_wks-ext */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-ext.js");
var wksDefine = __webpack_require__(/*! ./_wks-define */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-define.js");
var enumKeys = __webpack_require__(/*! ./_enum-keys */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-keys.js");
var isArray = __webpack_require__(/*! ./_is-array */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array.js");
var anObject = __webpack_require__(/*! ./_an-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var isObject = __webpack_require__(/*! ./_is-object */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");
var toPrimitive = __webpack_require__(/*! ./_to-primitive */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_to-primitive.js");
var createDesc = __webpack_require__(/*! ./_property-desc */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js");
var _create = __webpack_require__(/*! ./_object-create */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-create.js");
var gOPNExt = __webpack_require__(/*! ./_object-gopn-ext */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn-ext.js");
var $GOPD = __webpack_require__(/*! ./_object-gopd */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopd.js");
var $DP = __webpack_require__(/*! ./_object-dp */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js");
var $keys = __webpack_require__(/*! ./_object-keys */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js");
var gOPD = $GOPD.f;
var dP = $DP.f;
var gOPN = gOPNExt.f;
var $Symbol = global.Symbol;
var $JSON = global.JSON;
var _stringify = $JSON && $JSON.stringify;
var PROTOTYPE = 'prototype';
var HIDDEN = wks('_hidden');
var TO_PRIMITIVE = wks('toPrimitive');
var isEnum = {}.propertyIsEnumerable;
var SymbolRegistry = shared('symbol-registry');
var AllSymbols = shared('symbols');
var OPSymbols = shared('op-symbols');
var ObjectProto = Object[PROTOTYPE];
var USE_NATIVE = typeof $Symbol == 'function';
var QObject = global.QObject;
// Don't use setters in Qt Script, https://github.com/zloirock/core-js/issues/173
var setter = !QObject || !QObject[PROTOTYPE] || !QObject[PROTOTYPE].findChild;

// fallback for old Android, https://code.google.com/p/v8/issues/detail?id=687
var setSymbolDesc = DESCRIPTORS && $fails(function () {
  return _create(dP({}, 'a', {
    get: function () { return dP(this, 'a', { value: 7 }).a; }
  })).a != 7;
}) ? function (it, key, D) {
  var protoDesc = gOPD(ObjectProto, key);
  if (protoDesc) delete ObjectProto[key];
  dP(it, key, D);
  if (protoDesc && it !== ObjectProto) dP(ObjectProto, key, protoDesc);
} : dP;

var wrap = function (tag) {
  var sym = AllSymbols[tag] = _create($Symbol[PROTOTYPE]);
  sym._k = tag;
  return sym;
};

var isSymbol = USE_NATIVE && typeof $Symbol.iterator == 'symbol' ? function (it) {
  return typeof it == 'symbol';
} : function (it) {
  return it instanceof $Symbol;
};

var $defineProperty = function defineProperty(it, key, D) {
  if (it === ObjectProto) $defineProperty(OPSymbols, key, D);
  anObject(it);
  key = toPrimitive(key, true);
  anObject(D);
  if (has(AllSymbols, key)) {
    if (!D.enumerable) {
      if (!has(it, HIDDEN)) dP(it, HIDDEN, createDesc(1, {}));
      it[HIDDEN][key] = true;
    } else {
      if (has(it, HIDDEN) && it[HIDDEN][key]) it[HIDDEN][key] = false;
      D = _create(D, { enumerable: createDesc(0, false) });
    } return setSymbolDesc(it, key, D);
  } return dP(it, key, D);
};
var $defineProperties = function defineProperties(it, P) {
  anObject(it);
  var keys = enumKeys(P = toIObject(P));
  var i = 0;
  var l = keys.length;
  var key;
  while (l > i) $defineProperty(it, key = keys[i++], P[key]);
  return it;
};
var $create = function create(it, P) {
  return P === undefined ? _create(it) : $defineProperties(_create(it), P);
};
var $propertyIsEnumerable = function propertyIsEnumerable(key) {
  var E = isEnum.call(this, key = toPrimitive(key, true));
  if (this === ObjectProto && has(AllSymbols, key) && !has(OPSymbols, key)) return false;
  return E || !has(this, key) || !has(AllSymbols, key) || has(this, HIDDEN) && this[HIDDEN][key] ? E : true;
};
var $getOwnPropertyDescriptor = function getOwnPropertyDescriptor(it, key) {
  it = toIObject(it);
  key = toPrimitive(key, true);
  if (it === ObjectProto && has(AllSymbols, key) && !has(OPSymbols, key)) return;
  var D = gOPD(it, key);
  if (D && has(AllSymbols, key) && !(has(it, HIDDEN) && it[HIDDEN][key])) D.enumerable = true;
  return D;
};
var $getOwnPropertyNames = function getOwnPropertyNames(it) {
  var names = gOPN(toIObject(it));
  var result = [];
  var i = 0;
  var key;
  while (names.length > i) {
    if (!has(AllSymbols, key = names[i++]) && key != HIDDEN && key != META) result.push(key);
  } return result;
};
var $getOwnPropertySymbols = function getOwnPropertySymbols(it) {
  var IS_OP = it === ObjectProto;
  var names = gOPN(IS_OP ? OPSymbols : toIObject(it));
  var result = [];
  var i = 0;
  var key;
  while (names.length > i) {
    if (has(AllSymbols, key = names[i++]) && (IS_OP ? has(ObjectProto, key) : true)) result.push(AllSymbols[key]);
  } return result;
};

// 19.4.1.1 Symbol([description])
if (!USE_NATIVE) {
  $Symbol = function Symbol() {
    if (this instanceof $Symbol) throw TypeError('Symbol is not a constructor!');
    var tag = uid(arguments.length > 0 ? arguments[0] : undefined);
    var $set = function (value) {
      if (this === ObjectProto) $set.call(OPSymbols, value);
      if (has(this, HIDDEN) && has(this[HIDDEN], tag)) this[HIDDEN][tag] = false;
      setSymbolDesc(this, tag, createDesc(1, value));
    };
    if (DESCRIPTORS && setter) setSymbolDesc(ObjectProto, tag, { configurable: true, set: $set });
    return wrap(tag);
  };
  redefine($Symbol[PROTOTYPE], 'toString', function toString() {
    return this._k;
  });

  $GOPD.f = $getOwnPropertyDescriptor;
  $DP.f = $defineProperty;
  __webpack_require__(/*! ./_object-gopn */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn.js").f = gOPNExt.f = $getOwnPropertyNames;
  __webpack_require__(/*! ./_object-pie */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js").f = $propertyIsEnumerable;
  __webpack_require__(/*! ./_object-gops */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gops.js").f = $getOwnPropertySymbols;

  if (DESCRIPTORS && !__webpack_require__(/*! ./_library */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js")) {
    redefine(ObjectProto, 'propertyIsEnumerable', $propertyIsEnumerable, true);
  }

  wksExt.f = function (name) {
    return wrap(wks(name));
  };
}

$export($export.G + $export.W + $export.F * !USE_NATIVE, { Symbol: $Symbol });

for (var es6Symbols = (
  // 19.4.2.2, 19.4.2.3, 19.4.2.4, 19.4.2.6, 19.4.2.8, 19.4.2.9, 19.4.2.10, 19.4.2.11, 19.4.2.12, 19.4.2.13, 19.4.2.14
  'hasInstance,isConcatSpreadable,iterator,match,replace,search,species,split,toPrimitive,toStringTag,unscopables'
).split(','), j = 0; es6Symbols.length > j;)wks(es6Symbols[j++]);

for (var wellKnownSymbols = $keys(wks.store), k = 0; wellKnownSymbols.length > k;) wksDefine(wellKnownSymbols[k++]);

$export($export.S + $export.F * !USE_NATIVE, 'Symbol', {
  // 19.4.2.1 Symbol.for(key)
  'for': function (key) {
    return has(SymbolRegistry, key += '')
      ? SymbolRegistry[key]
      : SymbolRegistry[key] = $Symbol(key);
  },
  // 19.4.2.5 Symbol.keyFor(sym)
  keyFor: function keyFor(sym) {
    if (!isSymbol(sym)) throw TypeError(sym + ' is not a symbol!');
    for (var key in SymbolRegistry) if (SymbolRegistry[key] === sym) return key;
  },
  useSetter: function () { setter = true; },
  useSimple: function () { setter = false; }
});

$export($export.S + $export.F * !USE_NATIVE, 'Object', {
  // 19.1.2.2 Object.create(O [, Properties])
  create: $create,
  // 19.1.2.4 Object.defineProperty(O, P, Attributes)
  defineProperty: $defineProperty,
  // 19.1.2.3 Object.defineProperties(O, Properties)
  defineProperties: $defineProperties,
  // 19.1.2.6 Object.getOwnPropertyDescriptor(O, P)
  getOwnPropertyDescriptor: $getOwnPropertyDescriptor,
  // 19.1.2.7 Object.getOwnPropertyNames(O)
  getOwnPropertyNames: $getOwnPropertyNames,
  // 19.1.2.8 Object.getOwnPropertySymbols(O)
  getOwnPropertySymbols: $getOwnPropertySymbols
});

// 24.3.2 JSON.stringify(value [, replacer [, space]])
$JSON && $export($export.S + $export.F * (!USE_NATIVE || $fails(function () {
  var S = $Symbol();
  // MS Edge converts symbol values to JSON as {}
  // WebKit converts symbol values to JSON as null
  // V8 throws on boxed symbols
  return _stringify([S]) != '[null]' || _stringify({ a: S }) != '{}' || _stringify(Object(S)) != '{}';
})), 'JSON', {
  stringify: function stringify(it) {
    var args = [it];
    var i = 1;
    var replacer, $replacer;
    while (arguments.length > i) args.push(arguments[i++]);
    $replacer = replacer = args[1];
    if (!isObject(replacer) && it === undefined || isSymbol(it)) return; // IE8 returns string on undefined
    if (!isArray(replacer)) replacer = function (key, value) {
      if (typeof $replacer == 'function') value = $replacer.call(this, key, value);
      if (!isSymbol(value)) return value;
    };
    args[1] = replacer;
    return _stringify.apply($JSON, args);
  }
});

// 19.4.3.4 Symbol.prototype[@@toPrimitive](hint)
$Symbol[PROTOTYPE][TO_PRIMITIVE] || __webpack_require__(/*! ./_hide */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js")($Symbol[PROTOTYPE], TO_PRIMITIVE, $Symbol[PROTOTYPE].valueOf);
// 19.4.3.5 Symbol.prototype[@@toStringTag]
setToStringTag($Symbol, 'Symbol');
// 20.2.1.9 Math[@@toStringTag]
setToStringTag(Math, 'Math', true);
// 24.3.3 JSON[@@toStringTag]
setToStringTag(global.JSON, 'JSON', true);


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.finally.js":
/*!************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.finally.js ***!
  \************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
// https://github.com/tc39/proposal-promise-finally

var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var core = __webpack_require__(/*! ./_core */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var speciesConstructor = __webpack_require__(/*! ./_species-constructor */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_species-constructor.js");
var promiseResolve = __webpack_require__(/*! ./_promise-resolve */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_promise-resolve.js");

$export($export.P + $export.R, 'Promise', { 'finally': function (onFinally) {
  var C = speciesConstructor(this, core.Promise || global.Promise);
  var isFunction = typeof onFinally == 'function';
  return this.then(
    isFunction ? function (x) {
      return promiseResolve(C, onFinally()).then(function () { return x; });
    } : onFinally,
    isFunction ? function (e) {
      return promiseResolve(C, onFinally()).then(function () { throw e; });
    } : onFinally
  );
} });


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.try.js":
/*!********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.try.js ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// https://github.com/tc39/proposal-promise-try
var $export = __webpack_require__(/*! ./_export */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var newPromiseCapability = __webpack_require__(/*! ./_new-promise-capability */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_new-promise-capability.js");
var perform = __webpack_require__(/*! ./_perform */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_perform.js");

$export($export.S, 'Promise', { 'try': function (callbackfn) {
  var promiseCapability = newPromiseCapability.f(this);
  var result = perform(callbackfn);
  (result.e ? promiseCapability.reject : promiseCapability.resolve)(result.v);
  return promiseCapability.promise;
} });


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.async-iterator.js":
/*!******************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.async-iterator.js ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ./_wks-define */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-define.js")('asyncIterator');


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.observable.js":
/*!**************************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.observable.js ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ./_wks-define */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-define.js")('observable');


/***/ }),

/***/ "./node_modules/babel-runtime/node_modules/core-js/library/modules/web.dom.iterable.js":
/*!*********************************************************************************************!*\
  !*** ./node_modules/babel-runtime/node_modules/core-js/library/modules/web.dom.iterable.js ***!
  \*********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ./es6.array.iterator */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.iterator.js");
var global = __webpack_require__(/*! ./_global */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var hide = __webpack_require__(/*! ./_hide */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js");
var Iterators = __webpack_require__(/*! ./_iterators */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js");
var TO_STRING_TAG = __webpack_require__(/*! ./_wks */ "./node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('toStringTag');

var DOMIterables = ('CSSRuleList,CSSStyleDeclaration,CSSValueList,ClientRectList,DOMRectList,DOMStringList,' +
  'DOMTokenList,DataTransferItemList,FileList,HTMLAllCollection,HTMLCollection,HTMLFormElement,HTMLSelectElement,' +
  'MediaList,MimeTypeArray,NamedNodeMap,NodeList,PaintRequestList,Plugin,PluginArray,SVGLengthList,SVGNumberList,' +
  'SVGPathSegList,SVGPointList,SVGStringList,SVGTransformList,SourceBufferList,StyleSheetList,TextTrackCueList,' +
  'TextTrackList,TouchList').split(',');

for (var i = 0; i < DOMIterables.length; i++) {
  var NAME = DOMIterables[i];
  var Collection = global[NAME];
  var proto = Collection && Collection.prototype;
  if (proto && !proto[TO_STRING_TAG]) hide(proto, TO_STRING_TAG, NAME);
  Iterators[NAME] = Iterators.Array;
}


/***/ }),

/***/ "./node_modules/babel-runtime/regenerator/index.js":
/*!*********************************************************!*\
  !*** ./node_modules/babel-runtime/regenerator/index.js ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! regenerator-runtime */ "./node_modules/regenerator-runtime/runtime-module.js");


/***/ }),

/***/ "./node_modules/is-buffer/index.js":
/*!*****************************************!*\
  !*** ./node_modules/is-buffer/index.js ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports) {

/*!
 * Determine if an object is a Buffer
 *
 * @author   Feross Aboukhadijeh <https://feross.org>
 * @license  MIT
 */

// The _isBuffer check is for Safari 5-7 support, because it's missing
// Object.prototype.constructor. Remove this eventually
module.exports = function (obj) {
  return obj != null && (isBuffer(obj) || isSlowBuffer(obj) || !!obj._isBuffer)
}

function isBuffer (obj) {
  return !!obj.constructor && typeof obj.constructor.isBuffer === 'function' && obj.constructor.isBuffer(obj)
}

// For Node v0.10 support. Remove this eventually.
function isSlowBuffer (obj) {
  return typeof obj.readFloatLE === 'function' && typeof obj.slice === 'function' && isBuffer(obj.slice(0, 0))
}


/***/ }),

/***/ "./node_modules/process/browser.js":
/*!*****************************************!*\
  !*** ./node_modules/process/browser.js ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// shim for using process in browser
var process = module.exports = {};

// cached from whatever global is present so that test runners that stub it
// don't break things.  But we need to wrap it in a try catch in case it is
// wrapped in strict mode code which doesn't define any globals.  It's inside a
// function because try/catches deoptimize in certain engines.

var cachedSetTimeout;
var cachedClearTimeout;

function defaultSetTimout() {
    throw new Error('setTimeout has not been defined');
}
function defaultClearTimeout () {
    throw new Error('clearTimeout has not been defined');
}
(function () {
    try {
        if (typeof setTimeout === 'function') {
            cachedSetTimeout = setTimeout;
        } else {
            cachedSetTimeout = defaultSetTimout;
        }
    } catch (e) {
        cachedSetTimeout = defaultSetTimout;
    }
    try {
        if (typeof clearTimeout === 'function') {
            cachedClearTimeout = clearTimeout;
        } else {
            cachedClearTimeout = defaultClearTimeout;
        }
    } catch (e) {
        cachedClearTimeout = defaultClearTimeout;
    }
} ())
function runTimeout(fun) {
    if (cachedSetTimeout === setTimeout) {
        //normal enviroments in sane situations
        return setTimeout(fun, 0);
    }
    // if setTimeout wasn't available but was latter defined
    if ((cachedSetTimeout === defaultSetTimout || !cachedSetTimeout) && setTimeout) {
        cachedSetTimeout = setTimeout;
        return setTimeout(fun, 0);
    }
    try {
        // when when somebody has screwed with setTimeout but no I.E. maddness
        return cachedSetTimeout(fun, 0);
    } catch(e){
        try {
            // When we are in I.E. but the script has been evaled so I.E. doesn't trust the global object when called normally
            return cachedSetTimeout.call(null, fun, 0);
        } catch(e){
            // same as above but when it's a version of I.E. that must have the global object for 'this', hopfully our context correct otherwise it will throw a global error
            return cachedSetTimeout.call(this, fun, 0);
        }
    }


}
function runClearTimeout(marker) {
    if (cachedClearTimeout === clearTimeout) {
        //normal enviroments in sane situations
        return clearTimeout(marker);
    }
    // if clearTimeout wasn't available but was latter defined
    if ((cachedClearTimeout === defaultClearTimeout || !cachedClearTimeout) && clearTimeout) {
        cachedClearTimeout = clearTimeout;
        return clearTimeout(marker);
    }
    try {
        // when when somebody has screwed with setTimeout but no I.E. maddness
        return cachedClearTimeout(marker);
    } catch (e){
        try {
            // When we are in I.E. but the script has been evaled so I.E. doesn't  trust the global object when called normally
            return cachedClearTimeout.call(null, marker);
        } catch (e){
            // same as above but when it's a version of I.E. that must have the global object for 'this', hopfully our context correct otherwise it will throw a global error.
            // Some versions of I.E. have different rules for clearTimeout vs setTimeout
            return cachedClearTimeout.call(this, marker);
        }
    }



}
var queue = [];
var draining = false;
var currentQueue;
var queueIndex = -1;

function cleanUpNextTick() {
    if (!draining || !currentQueue) {
        return;
    }
    draining = false;
    if (currentQueue.length) {
        queue = currentQueue.concat(queue);
    } else {
        queueIndex = -1;
    }
    if (queue.length) {
        drainQueue();
    }
}

function drainQueue() {
    if (draining) {
        return;
    }
    var timeout = runTimeout(cleanUpNextTick);
    draining = true;

    var len = queue.length;
    while(len) {
        currentQueue = queue;
        queue = [];
        while (++queueIndex < len) {
            if (currentQueue) {
                currentQueue[queueIndex].run();
            }
        }
        queueIndex = -1;
        len = queue.length;
    }
    currentQueue = null;
    draining = false;
    runClearTimeout(timeout);
}

process.nextTick = function (fun) {
    var args = new Array(arguments.length - 1);
    if (arguments.length > 1) {
        for (var i = 1; i < arguments.length; i++) {
            args[i - 1] = arguments[i];
        }
    }
    queue.push(new Item(fun, args));
    if (queue.length === 1 && !draining) {
        runTimeout(drainQueue);
    }
};

// v8 likes predictible objects
function Item(fun, array) {
    this.fun = fun;
    this.array = array;
}
Item.prototype.run = function () {
    this.fun.apply(null, this.array);
};
process.title = 'browser';
process.browser = true;
process.env = {};
process.argv = [];
process.version = ''; // empty string to avoid regexp issues
process.versions = {};

function noop() {}

process.on = noop;
process.addListener = noop;
process.once = noop;
process.off = noop;
process.removeListener = noop;
process.removeAllListeners = noop;
process.emit = noop;
process.prependListener = noop;
process.prependOnceListener = noop;

process.listeners = function (name) { return [] }

process.binding = function (name) {
    throw new Error('process.binding is not supported');
};

process.cwd = function () { return '/' };
process.chdir = function (dir) {
    throw new Error('process.chdir is not supported');
};
process.umask = function() { return 0; };


/***/ }),

/***/ "./node_modules/prop-types/index.js":
/*!*******************************************************************************************************!*\
  !*** delegated ./node_modules/prop-types/index.js from dll-reference reactstrap_4b9f8d5b9e3dd1ee3943 ***!
  \*******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference reactstrap_4b9f8d5b9e3dd1ee3943 */ "dll-reference reactstrap_4b9f8d5b9e3dd1ee3943"))(0);

/***/ }),

/***/ "./node_modules/react-dom/index.js":
/*!******************************************************************************************************!*\
  !*** delegated ./node_modules/react-dom/index.js from dll-reference reactstrap_4b9f8d5b9e3dd1ee3943 ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference reactstrap_4b9f8d5b9e3dd1ee3943 */ "dll-reference reactstrap_4b9f8d5b9e3dd1ee3943"))(3);

/***/ }),

/***/ "./node_modules/react-redux/es/index.js":
/*!******************************************************************************************************!*\
  !*** delegated ./node_modules/react-redux/es/index.js from dll-reference react_4b9f8d5b9e3dd1ee3943 ***!
  \******************************************************************************************************/
/*! exports provided: Provider, createProvider, connectAdvanced, connect */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_4b9f8d5b9e3dd1ee3943 */ "dll-reference react_4b9f8d5b9e3dd1ee3943"))(65);

/***/ }),

/***/ "./node_modules/react/index.js":
/*!**************************************************************************************************!*\
  !*** delegated ./node_modules/react/index.js from dll-reference reactstrap_4b9f8d5b9e3dd1ee3943 ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference reactstrap_4b9f8d5b9e3dd1ee3943 */ "dll-reference reactstrap_4b9f8d5b9e3dd1ee3943"))(1);

/***/ }),

/***/ "./node_modules/reactstrap/dist/reactstrap.es.js":
/*!********************************************************************************************************************!*\
  !*** delegated ./node_modules/reactstrap/dist/reactstrap.es.js from dll-reference reactstrap_4b9f8d5b9e3dd1ee3943 ***!
  \********************************************************************************************************************/
/*! exports provided: Alert, Container, Row, Col, Navbar, NavbarBrand, NavbarToggler, Nav, NavItem, NavDropdown, NavLink, Breadcrumb, BreadcrumbItem, Button, ButtonDropdown, ButtonGroup, ButtonToolbar, Dropdown, DropdownItem, DropdownMenu, DropdownToggle, Fade, Badge, Card, CardLink, CardGroup, CardDeck, CardColumns, CardBody, CardBlock, CardFooter, CardHeader, CardImg, CardImgOverlay, Carousel, UncontrolledCarousel, CarouselControl, CarouselItem, CarouselIndicators, CarouselCaption, CardSubtitle, CardText, CardTitle, Popover, PopoverContent, PopoverBody, PopoverTitle, PopoverHeader, Progress, Modal, ModalHeader, ModalBody, ModalFooter, PopperContent, PopperTargetHelper, Tooltip, Table, ListGroup, Form, FormFeedback, FormGroup, FormText, Input, InputGroup, InputGroupAddon, InputGroupButton, InputGroupButtonDropdown, InputGroupText, Label, CustomInput, Media, Pagination, PaginationItem, PaginationLink, TabContent, TabPane, Jumbotron, Collapse, ListGroupItem, ListGroupItemText, ListGroupItemHeading, UncontrolledAlert, UncontrolledButtonDropdown, UncontrolledCollapse, UncontrolledDropdown, UncontrolledNavDropdown, UncontrolledTooltip, Util */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference reactstrap_4b9f8d5b9e3dd1ee3943 */ "dll-reference reactstrap_4b9f8d5b9e3dd1ee3943"))(57);

/***/ }),

/***/ "./node_modules/redux-thunk/es/index.js":
/*!******************************************************************************************************!*\
  !*** delegated ./node_modules/redux-thunk/es/index.js from dll-reference react_4b9f8d5b9e3dd1ee3943 ***!
  \******************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_4b9f8d5b9e3dd1ee3943 */ "dll-reference react_4b9f8d5b9e3dd1ee3943"))(62);

/***/ }),

/***/ "./node_modules/redux/es/redux.js":
/*!************************************************************************************************!*\
  !*** delegated ./node_modules/redux/es/redux.js from dll-reference react_4b9f8d5b9e3dd1ee3943 ***!
  \************************************************************************************************/
/*! exports provided: createStore, combineReducers, bindActionCreators, applyMiddleware, compose, __DO_NOT_USE__ActionTypes */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_4b9f8d5b9e3dd1ee3943 */ "dll-reference react_4b9f8d5b9e3dd1ee3943"))(29);

/***/ }),

/***/ "./node_modules/regenerator-runtime/runtime-module.js":
/*!************************************************************!*\
  !*** ./node_modules/regenerator-runtime/runtime-module.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

/**
 * Copyright (c) 2014-present, Facebook, Inc.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */

// This method of obtaining a reference to the global object needs to be
// kept identical to the way it is obtained in runtime.js
var g = (function() { return this })() || Function("return this")();

// Use `getOwnPropertyNames` because not all browsers support calling
// `hasOwnProperty` on the global `self` object in a worker. See #183.
var hadRuntime = g.regeneratorRuntime &&
  Object.getOwnPropertyNames(g).indexOf("regeneratorRuntime") >= 0;

// Save the old regeneratorRuntime in case it needs to be restored later.
var oldRuntime = hadRuntime && g.regeneratorRuntime;

// Force reevalutation of runtime.js.
g.regeneratorRuntime = undefined;

module.exports = __webpack_require__(/*! ./runtime */ "./node_modules/regenerator-runtime/runtime.js");

if (hadRuntime) {
  // Restore the original runtime.
  g.regeneratorRuntime = oldRuntime;
} else {
  // Remove the global property added by runtime.js.
  try {
    delete g.regeneratorRuntime;
  } catch(e) {
    g.regeneratorRuntime = undefined;
  }
}


/***/ }),

/***/ "./node_modules/regenerator-runtime/runtime.js":
/*!*****************************************************!*\
  !*** ./node_modules/regenerator-runtime/runtime.js ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

/**
 * Copyright (c) 2014-present, Facebook, Inc.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */

!(function(global) {
  "use strict";

  var Op = Object.prototype;
  var hasOwn = Op.hasOwnProperty;
  var undefined; // More compressible than void 0.
  var $Symbol = typeof Symbol === "function" ? Symbol : {};
  var iteratorSymbol = $Symbol.iterator || "@@iterator";
  var asyncIteratorSymbol = $Symbol.asyncIterator || "@@asyncIterator";
  var toStringTagSymbol = $Symbol.toStringTag || "@@toStringTag";

  var inModule = typeof module === "object";
  var runtime = global.regeneratorRuntime;
  if (runtime) {
    if (inModule) {
      // If regeneratorRuntime is defined globally and we're in a module,
      // make the exports object identical to regeneratorRuntime.
      module.exports = runtime;
    }
    // Don't bother evaluating the rest of this file if the runtime was
    // already defined globally.
    return;
  }

  // Define the runtime globally (as expected by generated code) as either
  // module.exports (if we're in a module) or a new, empty object.
  runtime = global.regeneratorRuntime = inModule ? module.exports : {};

  function wrap(innerFn, outerFn, self, tryLocsList) {
    // If outerFn provided and outerFn.prototype is a Generator, then outerFn.prototype instanceof Generator.
    var protoGenerator = outerFn && outerFn.prototype instanceof Generator ? outerFn : Generator;
    var generator = Object.create(protoGenerator.prototype);
    var context = new Context(tryLocsList || []);

    // The ._invoke method unifies the implementations of the .next,
    // .throw, and .return methods.
    generator._invoke = makeInvokeMethod(innerFn, self, context);

    return generator;
  }
  runtime.wrap = wrap;

  // Try/catch helper to minimize deoptimizations. Returns a completion
  // record like context.tryEntries[i].completion. This interface could
  // have been (and was previously) designed to take a closure to be
  // invoked without arguments, but in all the cases we care about we
  // already have an existing method we want to call, so there's no need
  // to create a new function object. We can even get away with assuming
  // the method takes exactly one argument, since that happens to be true
  // in every case, so we don't have to touch the arguments object. The
  // only additional allocation required is the completion record, which
  // has a stable shape and so hopefully should be cheap to allocate.
  function tryCatch(fn, obj, arg) {
    try {
      return { type: "normal", arg: fn.call(obj, arg) };
    } catch (err) {
      return { type: "throw", arg: err };
    }
  }

  var GenStateSuspendedStart = "suspendedStart";
  var GenStateSuspendedYield = "suspendedYield";
  var GenStateExecuting = "executing";
  var GenStateCompleted = "completed";

  // Returning this object from the innerFn has the same effect as
  // breaking out of the dispatch switch statement.
  var ContinueSentinel = {};

  // Dummy constructor functions that we use as the .constructor and
  // .constructor.prototype properties for functions that return Generator
  // objects. For full spec compliance, you may wish to configure your
  // minifier not to mangle the names of these two functions.
  function Generator() {}
  function GeneratorFunction() {}
  function GeneratorFunctionPrototype() {}

  // This is a polyfill for %IteratorPrototype% for environments that
  // don't natively support it.
  var IteratorPrototype = {};
  IteratorPrototype[iteratorSymbol] = function () {
    return this;
  };

  var getProto = Object.getPrototypeOf;
  var NativeIteratorPrototype = getProto && getProto(getProto(values([])));
  if (NativeIteratorPrototype &&
      NativeIteratorPrototype !== Op &&
      hasOwn.call(NativeIteratorPrototype, iteratorSymbol)) {
    // This environment has a native %IteratorPrototype%; use it instead
    // of the polyfill.
    IteratorPrototype = NativeIteratorPrototype;
  }

  var Gp = GeneratorFunctionPrototype.prototype =
    Generator.prototype = Object.create(IteratorPrototype);
  GeneratorFunction.prototype = Gp.constructor = GeneratorFunctionPrototype;
  GeneratorFunctionPrototype.constructor = GeneratorFunction;
  GeneratorFunctionPrototype[toStringTagSymbol] =
    GeneratorFunction.displayName = "GeneratorFunction";

  // Helper for defining the .next, .throw, and .return methods of the
  // Iterator interface in terms of a single ._invoke method.
  function defineIteratorMethods(prototype) {
    ["next", "throw", "return"].forEach(function(method) {
      prototype[method] = function(arg) {
        return this._invoke(method, arg);
      };
    });
  }

  runtime.isGeneratorFunction = function(genFun) {
    var ctor = typeof genFun === "function" && genFun.constructor;
    return ctor
      ? ctor === GeneratorFunction ||
        // For the native GeneratorFunction constructor, the best we can
        // do is to check its .name property.
        (ctor.displayName || ctor.name) === "GeneratorFunction"
      : false;
  };

  runtime.mark = function(genFun) {
    if (Object.setPrototypeOf) {
      Object.setPrototypeOf(genFun, GeneratorFunctionPrototype);
    } else {
      genFun.__proto__ = GeneratorFunctionPrototype;
      if (!(toStringTagSymbol in genFun)) {
        genFun[toStringTagSymbol] = "GeneratorFunction";
      }
    }
    genFun.prototype = Object.create(Gp);
    return genFun;
  };

  // Within the body of any async function, `await x` is transformed to
  // `yield regeneratorRuntime.awrap(x)`, so that the runtime can test
  // `hasOwn.call(value, "__await")` to determine if the yielded value is
  // meant to be awaited.
  runtime.awrap = function(arg) {
    return { __await: arg };
  };

  function AsyncIterator(generator) {
    function invoke(method, arg, resolve, reject) {
      var record = tryCatch(generator[method], generator, arg);
      if (record.type === "throw") {
        reject(record.arg);
      } else {
        var result = record.arg;
        var value = result.value;
        if (value &&
            typeof value === "object" &&
            hasOwn.call(value, "__await")) {
          return Promise.resolve(value.__await).then(function(value) {
            invoke("next", value, resolve, reject);
          }, function(err) {
            invoke("throw", err, resolve, reject);
          });
        }

        return Promise.resolve(value).then(function(unwrapped) {
          // When a yielded Promise is resolved, its final value becomes
          // the .value of the Promise<{value,done}> result for the
          // current iteration. If the Promise is rejected, however, the
          // result for this iteration will be rejected with the same
          // reason. Note that rejections of yielded Promises are not
          // thrown back into the generator function, as is the case
          // when an awaited Promise is rejected. This difference in
          // behavior between yield and await is important, because it
          // allows the consumer to decide what to do with the yielded
          // rejection (swallow it and continue, manually .throw it back
          // into the generator, abandon iteration, whatever). With
          // await, by contrast, there is no opportunity to examine the
          // rejection reason outside the generator function, so the
          // only option is to throw it from the await expression, and
          // let the generator function handle the exception.
          result.value = unwrapped;
          resolve(result);
        }, reject);
      }
    }

    var previousPromise;

    function enqueue(method, arg) {
      function callInvokeWithMethodAndArg() {
        return new Promise(function(resolve, reject) {
          invoke(method, arg, resolve, reject);
        });
      }

      return previousPromise =
        // If enqueue has been called before, then we want to wait until
        // all previous Promises have been resolved before calling invoke,
        // so that results are always delivered in the correct order. If
        // enqueue has not been called before, then it is important to
        // call invoke immediately, without waiting on a callback to fire,
        // so that the async generator function has the opportunity to do
        // any necessary setup in a predictable way. This predictability
        // is why the Promise constructor synchronously invokes its
        // executor callback, and why async functions synchronously
        // execute code before the first await. Since we implement simple
        // async functions in terms of async generators, it is especially
        // important to get this right, even though it requires care.
        previousPromise ? previousPromise.then(
          callInvokeWithMethodAndArg,
          // Avoid propagating failures to Promises returned by later
          // invocations of the iterator.
          callInvokeWithMethodAndArg
        ) : callInvokeWithMethodAndArg();
    }

    // Define the unified helper method that is used to implement .next,
    // .throw, and .return (see defineIteratorMethods).
    this._invoke = enqueue;
  }

  defineIteratorMethods(AsyncIterator.prototype);
  AsyncIterator.prototype[asyncIteratorSymbol] = function () {
    return this;
  };
  runtime.AsyncIterator = AsyncIterator;

  // Note that simple async functions are implemented on top of
  // AsyncIterator objects; they just return a Promise for the value of
  // the final result produced by the iterator.
  runtime.async = function(innerFn, outerFn, self, tryLocsList) {
    var iter = new AsyncIterator(
      wrap(innerFn, outerFn, self, tryLocsList)
    );

    return runtime.isGeneratorFunction(outerFn)
      ? iter // If outerFn is a generator, return the full iterator.
      : iter.next().then(function(result) {
          return result.done ? result.value : iter.next();
        });
  };

  function makeInvokeMethod(innerFn, self, context) {
    var state = GenStateSuspendedStart;

    return function invoke(method, arg) {
      if (state === GenStateExecuting) {
        throw new Error("Generator is already running");
      }

      if (state === GenStateCompleted) {
        if (method === "throw") {
          throw arg;
        }

        // Be forgiving, per 25.3.3.3.3 of the spec:
        // https://people.mozilla.org/~jorendorff/es6-draft.html#sec-generatorresume
        return doneResult();
      }

      context.method = method;
      context.arg = arg;

      while (true) {
        var delegate = context.delegate;
        if (delegate) {
          var delegateResult = maybeInvokeDelegate(delegate, context);
          if (delegateResult) {
            if (delegateResult === ContinueSentinel) continue;
            return delegateResult;
          }
        }

        if (context.method === "next") {
          // Setting context._sent for legacy support of Babel's
          // function.sent implementation.
          context.sent = context._sent = context.arg;

        } else if (context.method === "throw") {
          if (state === GenStateSuspendedStart) {
            state = GenStateCompleted;
            throw context.arg;
          }

          context.dispatchException(context.arg);

        } else if (context.method === "return") {
          context.abrupt("return", context.arg);
        }

        state = GenStateExecuting;

        var record = tryCatch(innerFn, self, context);
        if (record.type === "normal") {
          // If an exception is thrown from innerFn, we leave state ===
          // GenStateExecuting and loop back for another invocation.
          state = context.done
            ? GenStateCompleted
            : GenStateSuspendedYield;

          if (record.arg === ContinueSentinel) {
            continue;
          }

          return {
            value: record.arg,
            done: context.done
          };

        } else if (record.type === "throw") {
          state = GenStateCompleted;
          // Dispatch the exception by looping back around to the
          // context.dispatchException(context.arg) call above.
          context.method = "throw";
          context.arg = record.arg;
        }
      }
    };
  }

  // Call delegate.iterator[context.method](context.arg) and handle the
  // result, either by returning a { value, done } result from the
  // delegate iterator, or by modifying context.method and context.arg,
  // setting context.delegate to null, and returning the ContinueSentinel.
  function maybeInvokeDelegate(delegate, context) {
    var method = delegate.iterator[context.method];
    if (method === undefined) {
      // A .throw or .return when the delegate iterator has no .throw
      // method always terminates the yield* loop.
      context.delegate = null;

      if (context.method === "throw") {
        if (delegate.iterator.return) {
          // If the delegate iterator has a return method, give it a
          // chance to clean up.
          context.method = "return";
          context.arg = undefined;
          maybeInvokeDelegate(delegate, context);

          if (context.method === "throw") {
            // If maybeInvokeDelegate(context) changed context.method from
            // "return" to "throw", let that override the TypeError below.
            return ContinueSentinel;
          }
        }

        context.method = "throw";
        context.arg = new TypeError(
          "The iterator does not provide a 'throw' method");
      }

      return ContinueSentinel;
    }

    var record = tryCatch(method, delegate.iterator, context.arg);

    if (record.type === "throw") {
      context.method = "throw";
      context.arg = record.arg;
      context.delegate = null;
      return ContinueSentinel;
    }

    var info = record.arg;

    if (! info) {
      context.method = "throw";
      context.arg = new TypeError("iterator result is not an object");
      context.delegate = null;
      return ContinueSentinel;
    }

    if (info.done) {
      // Assign the result of the finished delegate to the temporary
      // variable specified by delegate.resultName (see delegateYield).
      context[delegate.resultName] = info.value;

      // Resume execution at the desired location (see delegateYield).
      context.next = delegate.nextLoc;

      // If context.method was "throw" but the delegate handled the
      // exception, let the outer generator proceed normally. If
      // context.method was "next", forget context.arg since it has been
      // "consumed" by the delegate iterator. If context.method was
      // "return", allow the original .return call to continue in the
      // outer generator.
      if (context.method !== "return") {
        context.method = "next";
        context.arg = undefined;
      }

    } else {
      // Re-yield the result returned by the delegate method.
      return info;
    }

    // The delegate iterator is finished, so forget it and continue with
    // the outer generator.
    context.delegate = null;
    return ContinueSentinel;
  }

  // Define Generator.prototype.{next,throw,return} in terms of the
  // unified ._invoke helper method.
  defineIteratorMethods(Gp);

  Gp[toStringTagSymbol] = "Generator";

  // A Generator should always return itself as the iterator object when the
  // @@iterator function is called on it. Some browsers' implementations of the
  // iterator prototype chain incorrectly implement this, causing the Generator
  // object to not be returned from this call. This ensures that doesn't happen.
  // See https://github.com/facebook/regenerator/issues/274 for more details.
  Gp[iteratorSymbol] = function() {
    return this;
  };

  Gp.toString = function() {
    return "[object Generator]";
  };

  function pushTryEntry(locs) {
    var entry = { tryLoc: locs[0] };

    if (1 in locs) {
      entry.catchLoc = locs[1];
    }

    if (2 in locs) {
      entry.finallyLoc = locs[2];
      entry.afterLoc = locs[3];
    }

    this.tryEntries.push(entry);
  }

  function resetTryEntry(entry) {
    var record = entry.completion || {};
    record.type = "normal";
    delete record.arg;
    entry.completion = record;
  }

  function Context(tryLocsList) {
    // The root entry object (effectively a try statement without a catch
    // or a finally block) gives us a place to store values thrown from
    // locations where there is no enclosing try statement.
    this.tryEntries = [{ tryLoc: "root" }];
    tryLocsList.forEach(pushTryEntry, this);
    this.reset(true);
  }

  runtime.keys = function(object) {
    var keys = [];
    for (var key in object) {
      keys.push(key);
    }
    keys.reverse();

    // Rather than returning an object with a next method, we keep
    // things simple and return the next function itself.
    return function next() {
      while (keys.length) {
        var key = keys.pop();
        if (key in object) {
          next.value = key;
          next.done = false;
          return next;
        }
      }

      // To avoid creating an additional object, we just hang the .value
      // and .done properties off the next function object itself. This
      // also ensures that the minifier will not anonymize the function.
      next.done = true;
      return next;
    };
  };

  function values(iterable) {
    if (iterable) {
      var iteratorMethod = iterable[iteratorSymbol];
      if (iteratorMethod) {
        return iteratorMethod.call(iterable);
      }

      if (typeof iterable.next === "function") {
        return iterable;
      }

      if (!isNaN(iterable.length)) {
        var i = -1, next = function next() {
          while (++i < iterable.length) {
            if (hasOwn.call(iterable, i)) {
              next.value = iterable[i];
              next.done = false;
              return next;
            }
          }

          next.value = undefined;
          next.done = true;

          return next;
        };

        return next.next = next;
      }
    }

    // Return an iterator with no values.
    return { next: doneResult };
  }
  runtime.values = values;

  function doneResult() {
    return { value: undefined, done: true };
  }

  Context.prototype = {
    constructor: Context,

    reset: function(skipTempReset) {
      this.prev = 0;
      this.next = 0;
      // Resetting context._sent for legacy support of Babel's
      // function.sent implementation.
      this.sent = this._sent = undefined;
      this.done = false;
      this.delegate = null;

      this.method = "next";
      this.arg = undefined;

      this.tryEntries.forEach(resetTryEntry);

      if (!skipTempReset) {
        for (var name in this) {
          // Not sure about the optimal order of these conditions:
          if (name.charAt(0) === "t" &&
              hasOwn.call(this, name) &&
              !isNaN(+name.slice(1))) {
            this[name] = undefined;
          }
        }
      }
    },

    stop: function() {
      this.done = true;

      var rootEntry = this.tryEntries[0];
      var rootRecord = rootEntry.completion;
      if (rootRecord.type === "throw") {
        throw rootRecord.arg;
      }

      return this.rval;
    },

    dispatchException: function(exception) {
      if (this.done) {
        throw exception;
      }

      var context = this;
      function handle(loc, caught) {
        record.type = "throw";
        record.arg = exception;
        context.next = loc;

        if (caught) {
          // If the dispatched exception was caught by a catch block,
          // then let that catch block handle the exception normally.
          context.method = "next";
          context.arg = undefined;
        }

        return !! caught;
      }

      for (var i = this.tryEntries.length - 1; i >= 0; --i) {
        var entry = this.tryEntries[i];
        var record = entry.completion;

        if (entry.tryLoc === "root") {
          // Exception thrown outside of any try block that could handle
          // it, so set the completion value of the entire function to
          // throw the exception.
          return handle("end");
        }

        if (entry.tryLoc <= this.prev) {
          var hasCatch = hasOwn.call(entry, "catchLoc");
          var hasFinally = hasOwn.call(entry, "finallyLoc");

          if (hasCatch && hasFinally) {
            if (this.prev < entry.catchLoc) {
              return handle(entry.catchLoc, true);
            } else if (this.prev < entry.finallyLoc) {
              return handle(entry.finallyLoc);
            }

          } else if (hasCatch) {
            if (this.prev < entry.catchLoc) {
              return handle(entry.catchLoc, true);
            }

          } else if (hasFinally) {
            if (this.prev < entry.finallyLoc) {
              return handle(entry.finallyLoc);
            }

          } else {
            throw new Error("try statement without catch or finally");
          }
        }
      }
    },

    abrupt: function(type, arg) {
      for (var i = this.tryEntries.length - 1; i >= 0; --i) {
        var entry = this.tryEntries[i];
        if (entry.tryLoc <= this.prev &&
            hasOwn.call(entry, "finallyLoc") &&
            this.prev < entry.finallyLoc) {
          var finallyEntry = entry;
          break;
        }
      }

      if (finallyEntry &&
          (type === "break" ||
           type === "continue") &&
          finallyEntry.tryLoc <= arg &&
          arg <= finallyEntry.finallyLoc) {
        // Ignore the finally entry if control is not jumping to a
        // location outside the try/catch block.
        finallyEntry = null;
      }

      var record = finallyEntry ? finallyEntry.completion : {};
      record.type = type;
      record.arg = arg;

      if (finallyEntry) {
        this.method = "next";
        this.next = finallyEntry.finallyLoc;
        return ContinueSentinel;
      }

      return this.complete(record);
    },

    complete: function(record, afterLoc) {
      if (record.type === "throw") {
        throw record.arg;
      }

      if (record.type === "break" ||
          record.type === "continue") {
        this.next = record.arg;
      } else if (record.type === "return") {
        this.rval = this.arg = record.arg;
        this.method = "return";
        this.next = "end";
      } else if (record.type === "normal" && afterLoc) {
        this.next = afterLoc;
      }

      return ContinueSentinel;
    },

    finish: function(finallyLoc) {
      for (var i = this.tryEntries.length - 1; i >= 0; --i) {
        var entry = this.tryEntries[i];
        if (entry.finallyLoc === finallyLoc) {
          this.complete(entry.completion, entry.afterLoc);
          resetTryEntry(entry);
          return ContinueSentinel;
        }
      }
    },

    "catch": function(tryLoc) {
      for (var i = this.tryEntries.length - 1; i >= 0; --i) {
        var entry = this.tryEntries[i];
        if (entry.tryLoc === tryLoc) {
          var record = entry.completion;
          if (record.type === "throw") {
            var thrown = record.arg;
            resetTryEntry(entry);
          }
          return thrown;
        }
      }

      // The context.catch method must only be called with a location
      // argument that corresponds to a known catch block.
      throw new Error("illegal catch attempt");
    },

    delegateYield: function(iterable, resultName, nextLoc) {
      this.delegate = {
        iterator: values(iterable),
        resultName: resultName,
        nextLoc: nextLoc
      };

      if (this.method === "next") {
        // Deliberately forget the last sent value so that we don't
        // accidentally pass it on to the delegate.
        this.arg = undefined;
      }

      return ContinueSentinel;
    }
  };
})(
  // In sloppy mode, unbound `this` refers to the global object, fallback to
  // Function constructor if we're in global strict mode. That is sadly a form
  // of indirect eval which violates Content Security Policy.
  (function() { return this })() || Function("return this")()
);


/***/ }),

/***/ "./source/js/components/Pager.jsx":
/*!****************************************!*\
  !*** ./source/js/components/Pager.jsx ***!
  \****************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _propTypes = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");

var _propTypes2 = _interopRequireDefault(_propTypes);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var Pager = function (_Component) {
    (0, _inherits3.default)(Pager, _Component);

    function Pager(props) {
        (0, _classCallCheck3.default)(this, Pager);

        var _this = (0, _possibleConstructorReturn3.default)(this, (Pager.__proto__ || (0, _getPrototypeOf2.default)(Pager)).call(this, props));

        _this.onKeyUpHandle = function (e, totalPage) {
            var code = e.keyCode ? e.keyCode : e.which;
            if (code === 13) {
                var value = parseInt(_this.state.inputValue);
                if (value > 0 && value <= totalPage) {
                    _this.goToPage(value, _this.props.pageOption.PageSize);
                }
            }
        };

        _this.onInputChangeHandler = function (e) {
            var pattern = /^\d*$/;
            var value = e.target.value;

            var isValidNumber = pattern.test(value);
            if (isValidNumber) {
                _this._updateState(value);
            }
        };

        _this.state = {
            inputValue: _this.props.pageOption.PageIndex
        };
        return _this;
    }

    (0, _createClass3.default)(Pager, [{
        key: 'goToPage',
        value: function goToPage(index, pageSize) {
            this._updateState(index);
            this.props.onPaging && this.props.onPaging(index, pageSize);
        }
    }, {
        key: 'onClickHandle',
        value: function onClickHandle(isDisable, handleFunc) {
            if (!isDisable) {
                handleFunc();
            }
        }
    }, {
        key: '_updateState',
        value: function _updateState(inputValue) {
            this.setState({ inputValue: inputValue });
        }
    }, {
        key: '_renderPageSizeSelection',
        value: function _renderPageSizeSelection(isDisable, pageIndex, pageSize) {
            var _this2 = this;

            return _react2.default.createElement(
                'select',
                { className: 'pagesize-select', disabled: isDisable, value: pageSize, onChange: function onChange(e) {
                        return _this2.goToPage(1, Number(e.target.value));
                    } },
                this._renderPageSizeOption(this.props.pageOption.PageList)
            );
        }
    }, {
        key: '_renderPageSizeOption',
        value: function _renderPageSizeOption(pageList) {
            return pageList.map(function (item, index) {
                return _react2.default.createElement(
                    'option',
                    { key: 'pagesize' + index, value: item },
                    item
                );
            });
        }
    }, {
        key: '_renderPageIcon',
        value: function _renderPageIcon(iconClass, isDisable, onClickFunc) {
            var _this3 = this;

            return _react2.default.createElement('a', { href: 'javascript:;', className: 'icon ' + iconClass + ' ' + (isDisable ? "disable" : ""), onClick: function onClick() {
                    return _this3.onClickHandle(isDisable, onClickFunc);
                } });
        }
    }, {
        key: '_renderPageInput',
        value: function _renderPageInput(index, totalPage) {
            var _this4 = this;

            return _react2.default.createElement(
                'div',
                { className: 'page-input' },
                _react2.default.createElement(
                    'span',
                    null,
                    'Page '
                ),
                _react2.default.createElement('input', { type: 'text', className: 'page-form', value: index, maxLength: '4', size: '2',
                    onChange: this.onInputChangeHandler,
                    onKeyUp: function onKeyUp(e) {
                        return _this4.onKeyUpHandle(e, totalPage);
                    } }),
                _react2.default.createElement(
                    'span',
                    null,
                    ' of ' + totalPage,
                    ' '
                )
            );
        }
    }, {
        key: '_renderPageLabel',
        value: function _renderPageLabel(index, totalPage) {
            return _react2.default.createElement(
                'div',
                { className: 'page-input' },
                _react2.default.createElement(
                    'span',
                    null,
                    'Page ',
                    index + ' of ' + totalPage + ' '
                )
            );
        }
    }, {
        key: '_renderPageInfo',
        value: function _renderPageInfo(index, pageSize, totalItem) {
            var viewFrom = (index - 1) * pageSize + 1;
            var viewTo = index * pageSize;
            return _react2.default.createElement(
                'span',
                { className: 'page-info' },
                'View ' + viewFrom + '-' + (viewTo > totalItem ? totalItem : viewTo) + ' of ' + totalItem
            );
        }
    }, {
        key: 'render',
        value: function render() {
            var _this5 = this;

            var _props = this.props,
                isShowPagingInfo = _props.isShowPagingInfo,
                isAllowInputPageIndex = _props.isAllowInputPageIndex,
                pageOption = _props.pageOption;
            var PageIndex = pageOption.PageIndex,
                PageSize = pageOption.PageSize,
                TotalItem = pageOption.TotalItem,
                PageList = pageOption.PageList;
            var inputValue = this.state.inputValue;


            var isDisable = isNaN(parseInt(inputValue));

            var isShow = TotalItem > PageList[0];
            var totalPage = Math.ceil(TotalItem / PageSize);

            return _react2.default.createElement(
                'div',
                { className: 'page-container text-center', style: { display: '' + (isShow ? 'block' : 'none') } },
                this._renderPageIcon("page-first", PageIndex === 1, function () {
                    return _this5.goToPage(1, PageSize);
                }),
                this._renderPageIcon("page-prev", isDisable || PageIndex <= 1 || PageIndex > totalPage, function () {
                    return _this5.goToPage(PageIndex - 1, PageSize);
                }),
                isAllowInputPageIndex ? this._renderPageInput(inputValue, totalPage) : this._renderPageLabel(PageIndex, totalPage),
                this._renderPageIcon("page-next", isDisable || PageIndex >= totalPage || PageIndex < 1, function () {
                    return _this5.goToPage(PageIndex + 1, PageSize);
                }),
                this._renderPageIcon("page-last", PageIndex === totalPage, function () {
                    return _this5.goToPage(totalPage, PageSize);
                }),
                this._renderPageSizeSelection(isDisable, PageIndex, PageSize),
                isShowPagingInfo && this._renderPageInfo(PageIndex, PageSize, TotalItem)
            );
        }
    }]);
    return Pager;
}(_react.Component);

Pager.propTypes = {
    pageOption: _propTypes2.default.shape({
        PageIndex: _propTypes2.default.number,
        PageSize: _propTypes2.default.number,
        PageList: _propTypes2.default.arrayOf(_propTypes2.default.number),
        TotalItem: _propTypes2.default.number
    }),
    onPaging: _propTypes2.default.func,
    isShowPagingInfo: _propTypes2.default.bool,
    isAllowInputPageIndex: _propTypes2.default.bool
};
Pager.defaultProps = {
    pageOption: {
        PageIndex: 1,
        PageSize: 50,
        PageList: [50, 100, 200],
        TotalItem: 0
    },
    isShowPagingInfo: false,
    isAllowInputPageIndex: true
};
exports.default = Pager;

/***/ }),

/***/ "./source/js/components/multiple-select/components/MultipleSelect.jsx":
/*!****************************************************************************!*\
  !*** ./source/js/components/multiple-select/components/MultipleSelect.jsx ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _propTypes = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");

var _propTypes2 = _interopRequireDefault(_propTypes);

var _MultipleSelectLabel = __webpack_require__(/*! ./MultipleSelectLabel */ "./source/js/components/multiple-select/components/MultipleSelectLabel.jsx");

var _MultipleSelectLabel2 = _interopRequireDefault(_MultipleSelectLabel);

var _MultipleSelectOptionList = __webpack_require__(/*! ./MultipleSelectOptionList */ "./source/js/components/multiple-select/components/MultipleSelectOptionList.jsx");

var _MultipleSelectOptionList2 = _interopRequireDefault(_MultipleSelectOptionList);

var _OptionAll = __webpack_require__(/*! ./OptionAll */ "./source/js/components/multiple-select/components/OptionAll.jsx");

var _OptionAll2 = _interopRequireDefault(_OptionAll);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var KEY_NAME = "key";
var VALUE_NAME = "value";
var STATUS_NAME = "checked";

var id = 1;

var MultipleSelect = function (_Component) {
    (0, _inherits3.default)(MultipleSelect, _Component);

    function MultipleSelect(props) {
        (0, _classCallCheck3.default)(this, MultipleSelect);

        var _this = (0, _possibleConstructorReturn3.default)(this, (MultipleSelect.__proto__ || (0, _getPrototypeOf2.default)(MultipleSelect)).call(this, props));

        _this.onChangeHandler = function (item) {
            var _selectedItem;

            var _this$props = _this.props,
                keyField = _this$props.keyField,
                valueField = _this$props.valueField,
                statusField = _this$props.statusField,
                dataSource = _this$props.dataSource;


            var selectedItem = (_selectedItem = {}, (0, _defineProperty3.default)(_selectedItem, keyField, item.key), (0, _defineProperty3.default)(_selectedItem, valueField, item.value), (0, _defineProperty3.default)(_selectedItem, statusField, item.checked), _selectedItem);

            var newDataSource = dataSource.map(function (option) {
                var _ref;

                var newItem = (0, _assign2.default)({}, option);

                if (newItem[keyField] === item.key) {
                    newItem[statusField] = item.checked;
                }

                return _ref = {}, (0, _defineProperty3.default)(_ref, KEY_NAME, item[keyField]), (0, _defineProperty3.default)(_ref, VALUE_NAME, item[valueField]), (0, _defineProperty3.default)(_ref, STATUS_NAME, item[statusField]), _ref;
            });

            _this._callBackToParent(selectedItem, newDataSource);
        };

        _this.onToggle = function () {
            _this.setState(function (prevState) {
                return { showOptionList: !prevState.showOptionList };
            });
        };

        _this.checkAllHandler = function (checked) {
            var _this$props2 = _this.props,
                keyField = _this$props2.keyField,
                valueField = _this$props2.valueField,
                dataSource = _this$props2.dataSource;

            var newDataSource = dataSource.map(function (item) {
                return {
                    key: item[keyField],
                    value: item[valueField],
                    checked: checked
                };
            });

            _this._callBackToParent(null, newDataSource);
        };

        _this._close = function () {
            _this.setState({ showOptionList: false });
        };

        _this._handleDocumentClick = function (event) {
            var clickOutside = _this.wrapper && !_this.wrapper.contains(event.target);

            if (clickOutside && _this.state.showOptionList) {
                _this._close();
            }
        };

        _this.id = 'multiple-select-' + (props.id || id++);

        _this.state = {
            showOptionList: false
        };
        return _this;
    }

    (0, _createClass3.default)(MultipleSelect, [{
        key: 'componentDidMount',
        value: function componentDidMount() {
            document.addEventListener('click', this._handleDocumentClick);
        }
    }, {
        key: 'componentWillUnmount',
        value: function componentWillUnmount() {
            document.removeEventListener('click', this._handleDocumentClick);
        }
    }, {
        key: '_callBackToParent',
        value: function _callBackToParent(selectedItem, newDataSource) {
            var selectedItemsKey = this._getSelectedItemKey(newDataSource);
            this.props.onChange && this.props.onChange(selectedItem, selectedItemsKey);
        }
    }, {
        key: '_convertDataSource',
        value: function _convertDataSource() {
            var _props = this.props,
                keyField = _props.keyField,
                valueField = _props.valueField,
                statusField = _props.statusField,
                dataSource = _props.dataSource;


            return dataSource.map(function (item) {
                var _ref2;

                return _ref2 = {}, (0, _defineProperty3.default)(_ref2, KEY_NAME, item[keyField]), (0, _defineProperty3.default)(_ref2, VALUE_NAME, item[valueField]), (0, _defineProperty3.default)(_ref2, STATUS_NAME, item[statusField]), _ref2;
            });
        }
    }, {
        key: '_getSelectedItemKey',
        value: function _getSelectedItemKey(dataSource) {
            var selectedItemKey = [];

            for (var i = 0; i < dataSource.length; i++) {
                var item = dataSource[i];

                if (item.checked) {
                    selectedItemKey.push(item.key);
                }
            }

            return selectedItemKey.join(",");
        }
    }, {
        key: '_renderOptionAll',
        value: function _renderOptionAll(dataSource) {
            var checkedItemCount = dataSource.filter(function (x) {
                return x.checked;
            }).length;
            var checkedAll = checkedItemCount === dataSource.length;

            return _react2.default.createElement(_OptionAll2.default, { id: this.id, checked: checkedAll, label: this.props.optionAllLabel, onChange: this.checkAllHandler });
        }
    }, {
        key: 'render',
        value: function render() {
            var _this2 = this;

            var _props2 = this.props,
                noneSelectedLabel = _props2.noneSelectedLabel,
                maxDisplayItemCount = _props2.maxDisplayItemCount,
                hasAllOption = _props2.hasAllOption;

            var dataSource = this._convertDataSource();
            var selectedItems = dataSource.filter(function (item) {
                return item.checked;
            });

            return _react2.default.createElement(
                'div',
                { className: 'multiple-select-container', id: this.id, ref: function ref(element) {
                        return _this2.wrapper = element;
                    } },
                _react2.default.createElement(_MultipleSelectLabel2.default, {
                    selectedItems: selectedItems,
                    onToggle: this.onToggle,
                    noneSelectedLabel: noneSelectedLabel,
                    maxDisplayItemCount: maxDisplayItemCount }),
                _react2.default.createElement(
                    'div',
                    { className: 'multiple-select-default multiple-select-options-container',
                        style: { display: this.state.showOptionList ? "block" : "none" } },
                    hasAllOption && this._renderOptionAll(dataSource),
                    _react2.default.createElement(_MultipleSelectOptionList2.default, { id: this.id,
                        dataSource: dataSource,
                        onChange: this.onChangeHandler })
                )
            );
        }
    }]);
    return MultipleSelect;
}(_react.Component);

MultipleSelect.propTypes = {
    dataSource: _propTypes2.default.arrayOf(_propTypes2.default.object).isRequired,
    keyField: _propTypes2.default.string,
    valueField: _propTypes2.default.string,
    statusField: _propTypes2.default.string,
    noneSelectedLabel: _propTypes2.default.string,
    optionAllLabel: _propTypes2.default.string,
    maxDisplayItemCount: _propTypes2.default.number,
    onChange: _propTypes2.default.func,
    hasAllOption: _propTypes2.default.bool
};
MultipleSelect.defaultProps = {
    keyField: KEY_NAME,
    valueField: VALUE_NAME,
    statusField: STATUS_NAME,
    hasAllOption: true
};
exports.default = MultipleSelect;

/***/ }),

/***/ "./source/js/components/multiple-select/components/MultipleSelectLabel.jsx":
/*!*********************************************************************************!*\
  !*** ./source/js/components/multiple-select/components/MultipleSelectLabel.jsx ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _propTypes = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");

var _propTypes2 = _interopRequireDefault(_propTypes);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var NO_SELECTED_LABEL = "Select options";
var MAX_SELECTED_ITEM_FOR_DISPLAY = 3;

function getSelectedItemsString(props) {
    var selectedItems = props.selectedItems,
        noneSelectedLabel = props.noneSelectedLabel,
        maxDisplayItemCount = props.maxDisplayItemCount;

    var selectedValues = selectedItems.map(function (item) {
        return item.value;
    });

    var displayText = noneSelectedLabel;
    var selectedItemCount = selectedValues.length;

    if (selectedItemCount >= maxDisplayItemCount) {
        displayText = selectedItemCount + ' selected';
    } else if (selectedItemCount >= 1) {
        displayText = selectedValues.join(", ");
    }

    return displayText;
}

var MultipleSelectLabel = function MultipleSelectLabel(props) {
    var selectedItemString = getSelectedItemsString(props);

    return _react2.default.createElement(
        'button',
        { type: 'button', className: 'multiple-select-default multiple-select-label',
            onClick: props.onToggle },
        _react2.default.createElement(
            'span',
            { className: 'text-display', title: selectedItemString },
            selectedItemString
        ),
        _react2.default.createElement('b', { className: 'caret' })
    );
};

MultipleSelectLabel.propTypes = {
    selectedItems: _propTypes2.default.arrayOf(_propTypes2.default.object).isRequired,
    noneSelectedLabel: _propTypes2.default.string,
    maxDisplayItemCount: _propTypes2.default.number
};

MultipleSelectLabel.defaultProps = {
    noneSelectedLabel: NO_SELECTED_LABEL,
    maxDisplayItemCount: MAX_SELECTED_ITEM_FOR_DISPLAY
};

exports.default = MultipleSelectLabel;

/***/ }),

/***/ "./source/js/components/multiple-select/components/MultipleSelectOptionList.jsx":
/*!**************************************************************************************!*\
  !*** ./source/js/components/multiple-select/components/MultipleSelectOptionList.jsx ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _propTypes = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");

var _propTypes2 = _interopRequireDefault(_propTypes);

var _Option = __webpack_require__(/*! ./Option */ "./source/js/components/multiple-select/components/Option.jsx");

var _Option2 = _interopRequireDefault(_Option);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var MultipleSelectOptionList = function MultipleSelectOptionList(props) {
    var dataSource = props.dataSource,
        onChange = props.onChange,
        id = props.id;


    return _react2.default.createElement(
        'ul',
        { className: 'multiple-select-options' },
        dataSource.map(function (item, index) {
            return _react2.default.createElement(_Option2.default, {
                id: id + '-optionItem-' + index,
                key: id + 'optionItem' + index,
                itemData: item,
                onChange: onChange });
        })
    );
};

MultipleSelectOptionList.propTypes = {
    dataSource: _propTypes2.default.arrayOf(_propTypes2.default.object).isRequired,
    id: _propTypes2.default.oneOfType([_propTypes2.default.number, _propTypes2.default.string]).isRequired,
    onChange: _propTypes2.default.func
};

exports.default = MultipleSelectOptionList;

/***/ }),

/***/ "./source/js/components/multiple-select/components/Option.jsx":
/*!********************************************************************!*\
  !*** ./source/js/components/multiple-select/components/Option.jsx ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _propTypes = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");

var _propTypes2 = _interopRequireDefault(_propTypes);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var Option = function Option(props) {
    var itemData = props.itemData,
        onChange = props.onChange,
        id = props.id;


    var handleChange = function handleChange(event) {
        event.stopPropagation();

        var newItemData = {
            key: itemData.key,
            value: itemData.value,
            checked: !itemData.checked
        };
        onChange && onChange(newItemData);
    };

    return _react2.default.createElement(
        'li',
        { className: 'multiple-select-item' },
        _react2.default.createElement('input', {
            id: id,
            type: 'checkbox',
            className: 'option-checkbox',
            checked: itemData.checked,
            onChange: handleChange
        }),
        _react2.default.createElement(
            'label',
            { className: 'option-label', htmlFor: id },
            itemData.value
        )
    );
};

Option.propTypes = {
    itemData: _propTypes2.default.object.isRequired,
    id: _propTypes2.default.oneOfType([_propTypes2.default.number, _propTypes2.default.string]).isRequired,
    onChange: _propTypes2.default.func
};

exports.default = Option;

/***/ }),

/***/ "./source/js/components/multiple-select/components/OptionAll.jsx":
/*!***********************************************************************!*\
  !*** ./source/js/components/multiple-select/components/OptionAll.jsx ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _propTypes = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");

var _propTypes2 = _interopRequireDefault(_propTypes);

var _Option = __webpack_require__(/*! ./Option */ "./source/js/components/multiple-select/components/Option.jsx");

var _Option2 = _interopRequireDefault(_Option);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var LABEL = "All";
var DEFAULT_CHECKED = false;

var OptionAll = function OptionAll(props) {
    var id = props.id,
        checked = props.checked,
        label = props.label,
        onChange = props.onChange;


    var itemData = {
        key: "All",
        value: label,
        checked: checked
    };

    var changeHandler = function changeHandler(_ref) {
        var checked = _ref.checked;

        onChange && onChange(checked);
    };

    return _react2.default.createElement(
        'ul',
        { className: 'multiple-select-options multiple-select-option-all' },
        _react2.default.createElement(_Option2.default, {
            id: id + '-optionItemAll',
            itemData: itemData,
            onChange: changeHandler })
    );
};

OptionAll.propTypes = {
    onChange: _propTypes2.default.func,
    id: _propTypes2.default.oneOfType([_propTypes2.default.number, _propTypes2.default.string]).isRequired,
    label: _propTypes2.default.string,
    checked: _propTypes2.default.bool
};

OptionAll.defaultProps = {
    label: LABEL,
    checked: DEFAULT_CHECKED
};

exports.default = OptionAll;

/***/ }),

/***/ "./source/js/components/multiple-select/index.jsx":
/*!********************************************************!*\
  !*** ./source/js/components/multiple-select/index.jsx ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _MultipleSelect = __webpack_require__(/*! ./components/MultipleSelect */ "./source/js/components/multiple-select/components/MultipleSelect.jsx");

var _MultipleSelect2 = _interopRequireDefault(_MultipleSelect);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = _MultipleSelect2.default;

/***/ }),

/***/ "./source/js/layouts/AppLayout.jsx":
/*!*****************************************!*\
  !*** ./source/js/layouts/AppLayout.jsx ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactDom = __webpack_require__(/*! react-dom */ "./node_modules/react-dom/index.js");

var _reactDom2 = _interopRequireDefault(_reactDom);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var AppLayout = function (_Component) {
  (0, _inherits3.default)(AppLayout, _Component);

  function AppLayout(props) {
    (0, _classCallCheck3.default)(this, AppLayout);
    return (0, _possibleConstructorReturn3.default)(this, (AppLayout.__proto__ || (0, _getPrototypeOf2.default)(AppLayout)).call(this, props));
  }

  (0, _createClass3.default)(AppLayout, [{
    key: 'render',
    value: function render() {
      return _react2.default.createElement(
        'div',
        { id: 'container', className: 'main-container' },
        this.props.children
      );
    }
  }]);
  return AppLayout;
}(_react.Component);

exports.default = AppLayout;

/***/ }),

/***/ "./source/js/modules/ajaxRequest.js":
/*!******************************************!*\
  !*** ./source/js/modules/ajaxRequest.js ***!
  \******************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.post = undefined;

var _axios = __webpack_require__(/*! axios */ "./node_modules/axios/index.js");

var _axios2 = _interopRequireDefault(_axios);

var _utils = __webpack_require__(/*! ./utils */ "./source/js/modules/utils.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var _xsrfToken = null;

var getXsrfToken = function getXsrfToken() {
    if (!_xsrfToken) {
        _xsrfToken = document.querySelector("input[name=__RequestVerificationToken]").value;
    }

    return _xsrfToken;
};

var post = exports.post = function post(relativeUrl, params) {
    var url = (0, _utils.resolveClientUrl)(relativeUrl);
    return _axios2.default.post(url, params, {
        headers: {
            'X-Requested-With': 'XMLHttpRequest',
            '__RequestVerificationToken': getXsrfToken()
        }
    });
};

/***/ }),

/***/ "./source/js/modules/createReducer.js":
/*!********************************************!*\
  !*** ./source/js/modules/createReducer.js ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.default = createReducer;
function createReducer(initialState, handlers) {
    return function reducer() {
        var state = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : initialState;
        var action = arguments[1];

        if (handlers.hasOwnProperty(action.type)) {
            return handlers[action.type](state, action);
        } else {
            return state;
        }
    };
}

/***/ }),

/***/ "./source/js/modules/dataHandler.js":
/*!******************************************!*\
  !*** ./source/js/modules/dataHandler.js ***!
  \******************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.updateObjectInArray = exports.deepClone = exports.removeItem = undefined;

var _extends2 = __webpack_require__(/*! babel-runtime/helpers/extends */ "./node_modules/babel-runtime/helpers/extends.js");

var _extends3 = _interopRequireDefault(_extends2);

var _stringify = __webpack_require__(/*! babel-runtime/core-js/json/stringify */ "./node_modules/babel-runtime/core-js/json/stringify.js");

var _stringify2 = _interopRequireDefault(_stringify);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var removeItem = exports.removeItem = function removeItem(index, dataList) {
    return dataList.filter(function (item, i) {
        return i !== index;
    });
};

var deepClone = exports.deepClone = function deepClone(data) {
    return JSON.parse((0, _stringify2.default)(data));
};

var updateObjectInArray = exports.updateObjectInArray = function updateObjectInArray(array, indexItem, updatedItem) {
    return array.map(function (item, index) {
        if (index !== indexItem) {
            return item;
        }
        return (0, _extends3.default)({}, item, updatedItem);
    });
};

/***/ }),

/***/ "./source/js/modules/numberFormat.js":
/*!*******************************************!*\
  !*** ./source/js/modules/numberFormat.js ***!
  \*******************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.formatDecimal = formatDecimal;
exports.format = format;
exports.round = round;
function formatDecimal(value) {
    return format(value, 2);
}

function format(value) {
    var digits = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : 0;

    if (typeof value !== "number" || isNaN(value)) {
        return "";
    }

    var roundedValue = round(value, digits);

    return roundedValue.toFixed(digits).replace(/(\d)(?=(\d{3})+(\.|$))/g, "$1,");
}

function round(value, digits) {
    var sign = value < 0 ? -1 : 1;
    var absoluteValue = Math.abs(value);
    var rounded = Number(Math.round(absoluteValue + 'e' + digits) + 'e-' + digits);

    return sign * rounded;
}

/***/ }),

/***/ "./source/js/modules/utils.js":
/*!************************************!*\
  !*** ./source/js/modules/utils.js ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
var _rootUrl = document.getElementById('RootUrl').value;

var resolveClientUrl = exports.resolveClientUrl = function resolveClientUrl(relativeUrl) {
    return _rootUrl + relativeUrl;
};

function formatUnit(value) {
    return value < 10 ? '0' + value : value;
}

var formatDateTime = exports.formatDateTime = function formatDateTime(dateTimeValue, format) {
    if (!dateTimeValue) {
        return "";
    }

    if (!format) {
        format = "MM/dd/yyyy";
    }

    var dateTime = typeof dateTimeValue === 'string' ? new Date(dateTimeValue) : dateTimeValue;
    var hours = dateTime.getHours();
    var minutes = dateTime.getMinutes();
    var seconds = dateTime.getSeconds();
    var ampm = hours >= 12 ? 'PM' : 'AM';

    var shortedHours = hours % 12;
    shortedHours = shortedHours === 0 ? 12 : shortedHours; // the hour '0' should be '12'

    var date = dateTime.getDate();
    var month = dateTime.getMonth() + 1;

    var result = format.replace("MM", formatUnit(month)).replace("dd", formatUnit(date)).replace("yyyy", dateTime.getFullYear()).replace("hh", formatUnit(shortedHours)).replace("mm", formatUnit(minutes)).replace("ss", formatUnit(seconds)).replace("tt", ampm).replace("HH", dateTime.getHours());

    return result;
};

var parseDateTime = exports.parseDateTime = function parseDateTime(value) {
    var reISO = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*))(?:Z|(\+|-)([\d|:]*))?$/;
    var reMsAjax = /^\/Date\((d|-|.*)\)[\/|\\]$/;

    if (typeof value === 'string') {
        var a = reISO.exec(value);
        if (a) return new Date(value);
        a = reMsAjax.exec(value);
        if (a) {
            var b = a[1].split(/[-+,.]/);
            return new Date(b[0] ? +b[0] : 0 - +b[1]);
        }
    }

    return value;
};

var isValidNumber = exports.isValidNumber = function isValidNumber(inputValue) {
    var regex = /^\d*$/;
    return regex.test(inputValue);
};

/***/ }),

/***/ "./source/js/pages/commission-group/actions/index.js":
/*!***********************************************************!*\
  !*** ./source/js/pages/commission-group/actions/index.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.deleteCommGroup = exports.getSelectedCommGroup = exports.updateCommissionGroup = exports.insertCommissionGroup = exports.setSelectedGroup = exports.setPagingOption = exports.getGroupSettingList = undefined;

var _regenerator = __webpack_require__(/*! babel-runtime/regenerator */ "./node_modules/babel-runtime/regenerator/index.js");

var _regenerator2 = _interopRequireDefault(_regenerator);

var _asyncToGenerator2 = __webpack_require__(/*! babel-runtime/helpers/asyncToGenerator */ "./node_modules/babel-runtime/helpers/asyncToGenerator.js");

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

var _ajaxRequest = __webpack_require__(/*! ../../../modules/ajaxRequest */ "./source/js/modules/ajaxRequest.js");

var _types = __webpack_require__(/*! ./types */ "./source/js/pages/commission-group/actions/types.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var fetchGroupSettingData = function fetchGroupSettingData(param) {
    var url = 'Portal/CommissionGroup/GetCommissionSettingList';

    return (0, _ajaxRequest.post)(url, param);
};

var setGridData = function setGridData(data) {
    return {
        type: _types.SET_GRIDDATA,
        data: data
    };
};

var getGroupSettingList = exports.getGroupSettingList = function getGroupSettingList(param) {
    return function () {
        var _ref = (0, _asyncToGenerator3.default)( /*#__PURE__*/_regenerator2.default.mark(function _callee(dispatch) {
            var response, _response$data, CommissionSettingList, TotalRows, PageIndex, PageSize, pagingOption;

            return _regenerator2.default.wrap(function _callee$(_context) {
                while (1) {
                    switch (_context.prev = _context.next) {
                        case 0:
                            _context.next = 2;
                            return fetchGroupSettingData(param);

                        case 2:
                            response = _context.sent;
                            _response$data = response.data, CommissionSettingList = _response$data.CommissionSettingList, TotalRows = _response$data.TotalRows;


                            dispatch(setGridData(CommissionSettingList));

                            PageIndex = param.PageIndex, PageSize = param.PageSize;
                            pagingOption = {
                                PageIndex: PageIndex,
                                PageSize: PageSize,
                                TotalItem: TotalRows
                            };

                            dispatch(setPagingOption(pagingOption));

                        case 8:
                        case 'end':
                            return _context.stop();
                    }
                }
            }, _callee, undefined);
        }));

        return function (_x) {
            return _ref.apply(this, arguments);
        };
    }();
};

var setPagingOption = exports.setPagingOption = function setPagingOption(data) {
    return {
        type: _types.SET_PAGING_OPTION,
        data: data
    };
};

var setSelectedGroup = exports.setSelectedGroup = function setSelectedGroup(data) {
    return {
        type: _types.SET_SELECTED_GROUP,
        data: data
    };
};

var insertCommissionGroup = exports.insertCommissionGroup = function insertCommissionGroup(param) {
    var url = 'Portal/CommissionGroup/InsertCommissionGroup';

    return function () {
        return (0, _ajaxRequest.post)(url, param);
    };
};

var updateCommissionGroup = exports.updateCommissionGroup = function updateCommissionGroup(param) {
    var url = 'Portal/CommissionGroup/UpdateCommissionGroup';

    return function () {
        return (0, _ajaxRequest.post)(url, param);
    };
};

var getSelectedCommGroup = exports.getSelectedCommGroup = function getSelectedCommGroup(param) {
    return function () {
        var _ref2 = (0, _asyncToGenerator3.default)( /*#__PURE__*/_regenerator2.default.mark(function _callee2(dispatch) {
            var url, response;
            return _regenerator2.default.wrap(function _callee2$(_context2) {
                while (1) {
                    switch (_context2.prev = _context2.next) {
                        case 0:
                            url = 'Portal/CommissionGroup/GetCommissionGroup';
                            _context2.next = 3;
                            return (0, _ajaxRequest.post)(url, param);

                        case 3:
                            response = _context2.sent;


                            dispatch(setSelectedGroup(response.data));

                        case 5:
                        case 'end':
                            return _context2.stop();
                    }
                }
            }, _callee2, undefined);
        }));

        return function (_x2) {
            return _ref2.apply(this, arguments);
        };
    }();
};

var deleteCommGroup = exports.deleteCommGroup = function deleteCommGroup(param) {
    var url = 'Portal/CommissionGroup/DeleteCommissionGroup';

    return function () {
        return (0, _ajaxRequest.post)(url, param);
    };
};

/***/ }),

/***/ "./source/js/pages/commission-group/actions/types.js":
/*!***********************************************************!*\
  !*** ./source/js/pages/commission-group/actions/types.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
var SET_GRIDDATA = exports.SET_GRIDDATA = 'SET_GRIDDATA';
var SET_PAGING_OPTION = exports.SET_PAGING_OPTION = 'SET_PAGING_OPTION';

var SET_SELECTED_GROUP = exports.SET_SELECTED_GROUP = 'SET_SELECTED_GROUP';

/***/ }),

/***/ "./source/js/pages/commission-group/components/AppContainer.jsx":
/*!**********************************************************************!*\
  !*** ./source/js/pages/commission-group/components/AppContainer.jsx ***!
  \**********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _AppLayout = __webpack_require__(/*! ../../../layouts/AppLayout */ "./source/js/layouts/AppLayout.jsx");

var _AppLayout2 = _interopRequireDefault(_AppLayout);

var _Filter = __webpack_require__(/*! ./Filter */ "./source/js/pages/commission-group/components/Filter.jsx");

var _Filter2 = _interopRequireDefault(_Filter);

var _Content = __webpack_require__(/*! ./Content */ "./source/js/pages/commission-group/components/Content.jsx");

var _Content2 = _interopRequireDefault(_Content);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var AppContainer = function (_Component) {
    (0, _inherits3.default)(AppContainer, _Component);

    function AppContainer(props) {
        (0, _classCallCheck3.default)(this, AppContainer);
        return (0, _possibleConstructorReturn3.default)(this, (AppContainer.__proto__ || (0, _getPrototypeOf2.default)(AppContainer)).call(this, props));
    }

    (0, _createClass3.default)(AppContainer, [{
        key: "render",
        value: function render() {
            return _react2.default.createElement(
                _AppLayout2.default,
                null,
                _react2.default.createElement(_Filter2.default, null),
                _react2.default.createElement(_Content2.default, null)
            );
        }
    }]);
    return AppContainer;
}(_react.Component);

exports.default = AppContainer;

/***/ }),

/***/ "./source/js/pages/commission-group/components/Content.jsx":
/*!*****************************************************************!*\
  !*** ./source/js/pages/commission-group/components/Content.jsx ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.Content = undefined;

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _reactstrap = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/dist/reactstrap.es.js");

var _GridData = __webpack_require__(/*! ./grouplist/GridData */ "./source/js/pages/commission-group/components/grouplist/GridData.jsx");

var _GridData2 = _interopRequireDefault(_GridData);

var _Pager = __webpack_require__(/*! ../../../components/Pager */ "./source/js/components/Pager.jsx");

var _Pager2 = _interopRequireDefault(_Pager);

var _addgroup = __webpack_require__(/*! ./addgroup */ "./source/js/pages/commission-group/components/addgroup/index.jsx");

var _addgroup2 = _interopRequireDefault(_addgroup);

var _editgroup = __webpack_require__(/*! ./editgroup */ "./source/js/pages/commission-group/components/editgroup/index.jsx");

var _editgroup2 = _interopRequireDefault(_editgroup);

var _actions = __webpack_require__(/*! ../actions */ "./source/js/pages/commission-group/actions/index.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var Content = exports.Content = function (_Component) {
    (0, _inherits3.default)(Content, _Component);

    function Content(props) {
        (0, _classCallCheck3.default)(this, Content);

        var _this = (0, _possibleConstructorReturn3.default)(this, (Content.__proto__ || (0, _getPrototypeOf2.default)(Content)).call(this, props));

        _this.handleAddNewGroup = function () {
            _this.setState({ Popup: _react2.default.createElement(_addgroup2.default, { onClose: _this.handleHide }) });
        };

        _this.handleEditGroup = function (itemId) {
            _this.setState({ Popup: _react2.default.createElement(_editgroup2.default, { onClose: _this.handleHide, itemId: itemId }) });
        };

        _this.handleHide = function () {
            _this.setState({ Popup: null });
        };

        _this.onPaging = function (pageIndex, pageSize) {
            _this.props.getGroupSettingList({ PageIndex: pageIndex, PageSize: pageSize });
        };

        _this.state = {
            Popup: null
        };
        return _this;
    }

    (0, _createClass3.default)(Content, [{
        key: 'render',
        value: function render() {
            var Popup = this.state.Popup;

            var showPopup = Popup !== null;

            return _react2.default.createElement(
                'div',
                { className: 'data-content' },
                _react2.default.createElement(
                    'div',
                    { className: 'mx-2 my-2' },
                    _react2.default.createElement(
                        _reactstrap.Button,
                        { outline: true, type: 'button', onClick: this.handleAddNewGroup },
                        'Add New Group'
                    )
                ),
                _react2.default.createElement(_Pager2.default, { pageOption: this.props.pagingOption, onPaging: this.onPaging }),
                _react2.default.createElement(
                    'div',
                    { className: 'my-1' },
                    _react2.default.createElement(_GridData2.default, { onEdit: this.handleEditGroup })
                ),
                _react2.default.createElement(
                    _reactstrap.Modal,
                    { isOpen: showPopup, toggle: this.handleHide, size: 'lg', backdrop: 'static' },
                    _react2.default.createElement(
                        _reactstrap.ModalHeader,
                        { toggle: this.handleHide },
                        _react2.default.createElement('span', { className: 'iconpopup' }),
                        'Add a Commission Group'
                    ),
                    _react2.default.createElement(
                        _reactstrap.ModalBody,
                        null,
                        _react2.default.createElement(
                            'div',
                            { className: 'modal-body-wrapper' },
                            Popup
                        )
                    )
                )
            );
        }
    }]);
    return Content;
}(_react.Component);

function mapStateToProps(_ref) {
    var pagingOption = _ref.pagingOption;

    return (0, _assign2.default)({}, { pagingOption: pagingOption });
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ getGroupSettingList: _actions.getGroupSettingList }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProps, mapDispatchToProps)(Content);

/***/ }),

/***/ "./source/js/pages/commission-group/components/Filter.jsx":
/*!****************************************************************!*\
  !*** ./source/js/pages/commission-group/components/Filter.jsx ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _reactstrap = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/dist/reactstrap.es.js");

var _actions = __webpack_require__(/*! ../actions */ "./source/js/pages/commission-group/actions/index.js");

var _constant = __webpack_require__(/*! ../modules/constant */ "./source/js/pages/commission-group/modules/constant.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var Filter = function (_Component) {
    (0, _inherits3.default)(Filter, _Component);

    function Filter(props) {
        (0, _classCallCheck3.default)(this, Filter);

        var _this = (0, _possibleConstructorReturn3.default)(this, (Filter.__proto__ || (0, _getPrototypeOf2.default)(Filter)).call(this, props));

        _this.handleClick = function (event) {
            event.preventDefault();
            _this._submit();
        };

        _this.handleChange = function (event) {
            _this.groupName = event.target.value;
        };

        _this.groupName = '';
        return _this;
    }

    (0, _createClass3.default)(Filter, [{
        key: 'componentDidMount',
        value: function componentDidMount() {
            this._submit();
        }
    }, {
        key: '_submit',
        value: function _submit() {
            var param = (0, _assign2.default)({}, _constant.defaultPagingOption, { GroupName: this.groupName });
            this.props.getGroupSettingList(param);
        }
    }, {
        key: 'render',
        value: function render() {
            return _react2.default.createElement(
                'div',
                { className: 'filter-container' },
                _react2.default.createElement(
                    _reactstrap.Form,
                    { inline: true, onSubmit: this.handleClick, style: { backgroundColor: 'transparent' } },
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(
                            _reactstrap.Label,
                            { className: 'mr-sm-2' },
                            'Group Name'
                        ),
                        ' ',
                        _react2.default.createElement(_reactstrap.Input, { type: 'text', className: 'mr-sm-2 search-box', onChange: this.handleChange })
                    ),
                    _react2.default.createElement(
                        _reactstrap.Button,
                        { outline: true, color: 'primary', type: 'button', onClick: this.handleClick },
                        _react2.default.createElement('span', { className: 'oi oi-magnifying-glass' }),
                        ' Search'
                    )
                )
            );
        }
    }]);
    return Filter;
}(_react.Component);

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ getGroupSettingList: _actions.getGroupSettingList }, dispatch);
}

exports.default = (0, _reactRedux.connect)(null, mapDispatchToProps)(Filter);

/***/ }),

/***/ "./source/js/pages/commission-group/components/addgroup/AddGroup.jsx":
/*!***************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/addgroup/AddGroup.jsx ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.AddGroup = undefined;

var _regenerator = __webpack_require__(/*! babel-runtime/regenerator */ "./node_modules/babel-runtime/regenerator/index.js");

var _regenerator2 = _interopRequireDefault(_regenerator);

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _extends2 = __webpack_require__(/*! babel-runtime/helpers/extends */ "./node_modules/babel-runtime/helpers/extends.js");

var _extends3 = _interopRequireDefault(_extends2);

var _asyncToGenerator2 = __webpack_require__(/*! babel-runtime/helpers/asyncToGenerator */ "./node_modules/babel-runtime/helpers/asyncToGenerator.js");

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _reactstrap = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/dist/reactstrap.es.js");

var _actions = __webpack_require__(/*! ../../actions */ "./source/js/pages/commission-group/actions/index.js");

var _service = __webpack_require__(/*! ../../modules/service */ "./source/js/pages/commission-group/modules/service.js");

var _deduction = __webpack_require__(/*! ../shared/deduction */ "./source/js/pages/commission-group/components/shared/deduction/index.jsx");

var _deduction2 = _interopRequireDefault(_deduction);

var _QmTurnOverSetting = __webpack_require__(/*! ../shared/QmTurnOverSetting */ "./source/js/pages/commission-group/components/shared/QmTurnOverSetting.jsx");

var _QmTurnOverSetting2 = _interopRequireDefault(_QmTurnOverSetting);

var _tiersetting = __webpack_require__(/*! ../shared/tiersetting */ "./source/js/pages/commission-group/components/shared/tiersetting/index.jsx");

var _tiersetting2 = _interopRequireDefault(_tiersetting);

var _CommGroupInfo = __webpack_require__(/*! ../shared/CommGroupInfo */ "./source/js/pages/commission-group/components/shared/CommGroupInfo.jsx");

var _CommGroupInfo2 = _interopRequireDefault(_CommGroupInfo);

var _constant = __webpack_require__(/*! ../../modules/constant */ "./source/js/pages/commission-group/modules/constant.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var AddGroup = exports.AddGroup = function (_React$Component) {
    (0, _inherits3.default)(AddGroup, _React$Component);

    function AddGroup(props) {
        var _this2 = this;

        (0, _classCallCheck3.default)(this, AddGroup);

        var _this = (0, _possibleConstructorReturn3.default)(this, (AddGroup.__proto__ || (0, _getPrototypeOf2.default)(AddGroup)).call(this, props));

        _this.handleSave = (0, _asyncToGenerator3.default)( /*#__PURE__*/_regenerator2.default.mark(function _callee() {
            var selectedGroup, validationResult, platformFeeList, param, response, result, gettingParam;
            return _regenerator2.default.wrap(function _callee$(_context) {
                while (1) {
                    switch (_context.prev = _context.next) {
                        case 0:
                            selectedGroup = _this.props.selectedGroup;
                            validationResult = (0, _service.validate)(selectedGroup);

                            if (validationResult.isValid) {
                                _context.next = 6;
                                break;
                            }

                            _this.setState({
                                isShowMessage: !validationResult.isValid,
                                isSuccess: validationResult.isValid,
                                message: validationResult.messageList
                            });
                            _context.next = 16;
                            break;

                        case 6:
                            platformFeeList = selectedGroup.PlatformFeeList.filter(function (item) {
                                return !!item.ProductIds;
                            });
                            param = (0, _extends3.default)({}, selectedGroup, { PlatformFeeList: platformFeeList });

                            console.log(param);

                            _context.next = 11;
                            return _this.props.insertCommissionGroup(param);

                        case 11:
                            response = _context.sent;
                            result = response.data;

                            console.log(result);
                            if (result.IsSuccessful) {
                                window.setTimeout(_this.props.onClose, 3000);
                                gettingParam = (0, _assign2.default)({}, _constant.defaultPagingOption);

                                _this.props.getGroupSettingList(gettingParam);
                            }

                            _this.setState({
                                isShowMessage: true,
                                isSuccess: result.IsSuccessful,
                                message: [result.IsSuccessful ? 'Add Commission Group Successfully' : result.Message]
                            });

                        case 16:
                        case 'end':
                            return _context.stop();
                    }
                }
            }, _callee, _this2);
        }));


        _this.state = {
            isShowMessage: false,
            isSuccess: true,
            message: []
        };
        return _this;
    }

    (0, _createClass3.default)(AddGroup, [{
        key: 'componentDidMount',
        value: function componentDidMount() {
            var _props = this.props,
                deductionList = _props.deductionList,
                productList = _props.productList;

            var commGroup = (0, _service.buildDefaultCommGroup)(deductionList, productList);

            this.props.setSelectedGroup(commGroup);
        }
    }, {
        key: 'componentWillUnmount',
        value: function componentWillUnmount() {
            this.props.setSelectedGroup(null);
        }
    }, {
        key: 'render',
        value: function render() {
            var _state = this.state,
                isShowMessage = _state.isShowMessage,
                isSuccess = _state.isSuccess,
                message = _state.message;


            return this.props.selectedGroup && _react2.default.createElement(
                'div',
                { className: 'comm-popup-container' },
                _react2.default.createElement(
                    _reactstrap.Alert,
                    { color: isSuccess ? 'success' : 'danger', isOpen: isShowMessage },
                    _react2.default.createElement('div', { dangerouslySetInnerHTML: { __html: message.join('<br>') } })
                ),
                _react2.default.createElement(_CommGroupInfo2.default, null),
                _react2.default.createElement(_deduction2.default, null),
                _react2.default.createElement(_QmTurnOverSetting2.default, null),
                _react2.default.createElement(_tiersetting2.default, null),
                _react2.default.createElement(
                    'button',
                    { type: 'button', className: 'my-2 btn btn-success btn-save', onClick: this.handleSave },
                    'SAVE'
                )
            );
        }
    }]);
    return AddGroup;
}(_react2.default.Component);

function mapStateToProp(_ref2) {
    var deductionList = _ref2.deductionList,
        productList = _ref2.productList,
        selectedGroup = _ref2.selectedGroup;

    return { deductionList: deductionList, productList: productList, selectedGroup: selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ insertCommissionGroup: _actions.insertCommissionGroup, getGroupSettingList: _actions.getGroupSettingList, setSelectedGroup: _actions.setSelectedGroup }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProp, mapDispatchToProps)(AddGroup);

/***/ }),

/***/ "./source/js/pages/commission-group/components/addgroup/index.jsx":
/*!************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/addgroup/index.jsx ***!
  \************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _AddGroup = __webpack_require__(/*! ./AddGroup */ "./source/js/pages/commission-group/components/addgroup/AddGroup.jsx");

var _AddGroup2 = _interopRequireDefault(_AddGroup);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = _AddGroup2.default;

/***/ }),

/***/ "./source/js/pages/commission-group/components/editgroup/EditGroup.jsx":
/*!*****************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/editgroup/EditGroup.jsx ***!
  \*****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.EditGroup = undefined;

var _regenerator = __webpack_require__(/*! babel-runtime/regenerator */ "./node_modules/babel-runtime/regenerator/index.js");

var _regenerator2 = _interopRequireDefault(_regenerator);

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _extends2 = __webpack_require__(/*! babel-runtime/helpers/extends */ "./node_modules/babel-runtime/helpers/extends.js");

var _extends3 = _interopRequireDefault(_extends2);

var _asyncToGenerator2 = __webpack_require__(/*! babel-runtime/helpers/asyncToGenerator */ "./node_modules/babel-runtime/helpers/asyncToGenerator.js");

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _reactstrap = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/dist/reactstrap.es.js");

var _actions = __webpack_require__(/*! ../../actions */ "./source/js/pages/commission-group/actions/index.js");

var _deduction = __webpack_require__(/*! ../shared/deduction */ "./source/js/pages/commission-group/components/shared/deduction/index.jsx");

var _deduction2 = _interopRequireDefault(_deduction);

var _QmTurnOverSetting = __webpack_require__(/*! ../shared/QmTurnOverSetting */ "./source/js/pages/commission-group/components/shared/QmTurnOverSetting.jsx");

var _QmTurnOverSetting2 = _interopRequireDefault(_QmTurnOverSetting);

var _tiersetting = __webpack_require__(/*! ../shared/tiersetting */ "./source/js/pages/commission-group/components/shared/tiersetting/index.jsx");

var _tiersetting2 = _interopRequireDefault(_tiersetting);

var _CommGroupInfo = __webpack_require__(/*! ../shared/CommGroupInfo */ "./source/js/pages/commission-group/components/shared/CommGroupInfo.jsx");

var _CommGroupInfo2 = _interopRequireDefault(_CommGroupInfo);

var _constant = __webpack_require__(/*! ../../modules/constant */ "./source/js/pages/commission-group/modules/constant.js");

var _service = __webpack_require__(/*! ../../modules/service */ "./source/js/pages/commission-group/modules/service.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var EditGroup = exports.EditGroup = function (_React$Component) {
    (0, _inherits3.default)(EditGroup, _React$Component);

    function EditGroup(props) {
        var _this2 = this;

        (0, _classCallCheck3.default)(this, EditGroup);

        var _this = (0, _possibleConstructorReturn3.default)(this, (EditGroup.__proto__ || (0, _getPrototypeOf2.default)(EditGroup)).call(this, props));

        _this.handleSave = (0, _asyncToGenerator3.default)( /*#__PURE__*/_regenerator2.default.mark(function _callee() {
            var selectedGroup, validationResult, platformFeeList, param, response, result, gettingParam;
            return _regenerator2.default.wrap(function _callee$(_context) {
                while (1) {
                    switch (_context.prev = _context.next) {
                        case 0:
                            selectedGroup = _this.props.selectedGroup;
                            validationResult = (0, _service.validate)(selectedGroup);

                            if (validationResult.isValid) {
                                _context.next = 6;
                                break;
                            }

                            _this.setState({
                                isShowMessage: !validationResult.isValid,
                                isSuccess: validationResult.isValid,
                                message: validationResult.messageList
                            });
                            _context.next = 16;
                            break;

                        case 6:
                            platformFeeList = selectedGroup.PlatformFeeList.filter(function (item) {
                                return !!item.ProductIds;
                            });
                            param = (0, _extends3.default)({}, selectedGroup, { PlatformFeeList: platformFeeList });

                            console.log(param);

                            _context.next = 11;
                            return _this.props.updateCommissionGroup(param);

                        case 11:
                            response = _context.sent;
                            result = response.data;

                            console.log(result);
                            if (result.IsSuccessful) {
                                window.setTimeout(_this.props.onClose, 3000);
                                gettingParam = (0, _assign2.default)({}, _constant.defaultPagingOption);

                                _this.props.getGroupSettingList(gettingParam);
                            }

                            _this.setState({
                                isShowMessage: true,
                                isSuccess: result.IsSuccessful,
                                message: [result.IsSuccessful ? 'Update Commission Group Successfully' : result.Message]
                            });

                        case 16:
                        case 'end':
                            return _context.stop();
                    }
                }
            }, _callee, _this2);
        }));


        _this.state = {
            isShowMessage: false,
            isSuccess: true,
            message: []
        };
        return _this;
    }

    (0, _createClass3.default)(EditGroup, [{
        key: 'componentDidMount',
        value: function componentDidMount() {
            this.props.getSelectedCommGroup({ GroupId: this.props.itemId });
        }
    }, {
        key: 'componentWillUnmount',
        value: function componentWillUnmount() {
            this.props.setSelectedGroup(null);
        }
    }, {
        key: 'render',
        value: function render() {
            var _state = this.state,
                isShowMessage = _state.isShowMessage,
                isSuccess = _state.isSuccess,
                message = _state.message;


            return this.props.selectedGroup && _react2.default.createElement(
                'div',
                { className: 'comm-popup-container' },
                _react2.default.createElement(
                    _reactstrap.Alert,
                    { color: isSuccess ? 'success' : 'danger', isOpen: isShowMessage },
                    _react2.default.createElement('div', { dangerouslySetInnerHTML: { __html: message.join('<br>') } })
                ),
                _react2.default.createElement(_CommGroupInfo2.default, null),
                _react2.default.createElement(_deduction2.default, null),
                _react2.default.createElement(_QmTurnOverSetting2.default, null),
                _react2.default.createElement(_tiersetting2.default, null),
                _react2.default.createElement(
                    'button',
                    { type: 'button', className: 'my-2 btn btn-success btn-save', onClick: this.handleSave },
                    'SAVE'
                )
            );
        }
    }]);
    return EditGroup;
}(_react2.default.Component);

function mapStateToProp(_ref2) {
    var selectedGroup = _ref2.selectedGroup;

    return { selectedGroup: selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ getSelectedCommGroup: _actions.getSelectedCommGroup, getGroupSettingList: _actions.getGroupSettingList, setSelectedGroup: _actions.setSelectedGroup, updateCommissionGroup: _actions.updateCommissionGroup }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProp, mapDispatchToProps)(EditGroup);

/***/ }),

/***/ "./source/js/pages/commission-group/components/editgroup/index.jsx":
/*!*************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/editgroup/index.jsx ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _EditGroup = __webpack_require__(/*! ./EditGroup */ "./source/js/pages/commission-group/components/editgroup/EditGroup.jsx");

var _EditGroup2 = _interopRequireDefault(_EditGroup);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = _EditGroup2.default;

/***/ }),

/***/ "./source/js/pages/commission-group/components/grouplist/GridData.jsx":
/*!****************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/grouplist/GridData.jsx ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.GridData = undefined;

var _objectWithoutProperties2 = __webpack_require__(/*! babel-runtime/helpers/objectWithoutProperties */ "./node_modules/babel-runtime/helpers/objectWithoutProperties.js");

var _objectWithoutProperties3 = _interopRequireDefault(_objectWithoutProperties2);

var _regenerator = __webpack_require__(/*! babel-runtime/regenerator */ "./node_modules/babel-runtime/regenerator/index.js");

var _regenerator2 = _interopRequireDefault(_regenerator);

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _asyncToGenerator2 = __webpack_require__(/*! babel-runtime/helpers/asyncToGenerator */ "./node_modules/babel-runtime/helpers/asyncToGenerator.js");

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _constant = __webpack_require__(/*! ../../modules/constant */ "./source/js/pages/commission-group/modules/constant.js");

var _numberFormat = __webpack_require__(/*! ../../../../modules/numberFormat */ "./source/js/modules/numberFormat.js");

var _actions = __webpack_require__(/*! ../../actions */ "./source/js/pages/commission-group/actions/index.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function rawHtml(content) {
    return _react2.default.createElement('p', { dangerouslySetInnerHTML: { __html: content } });
}

var GridData = exports.GridData = function (_Component) {
    (0, _inherits3.default)(GridData, _Component);

    function GridData(props) {
        var _this2 = this;

        (0, _classCallCheck3.default)(this, GridData);

        var _this = (0, _possibleConstructorReturn3.default)(this, (GridData.__proto__ || (0, _getPrototypeOf2.default)(GridData)).call(this, props));

        _this.handleDelete = function () {
            var _ref = (0, _asyncToGenerator3.default)( /*#__PURE__*/_regenerator2.default.mark(function _callee(groupId, groupName) {
                var isConfirmed, response, result, gettingParam;
                return _regenerator2.default.wrap(function _callee$(_context) {
                    while (1) {
                        switch (_context.prev = _context.next) {
                            case 0:
                                isConfirmed = window.confirm('Are you sure you want to delete the commission group ' + groupName + ' ?');

                                if (!isConfirmed) {
                                    _context.next = 7;
                                    break;
                                }

                                _context.next = 4;
                                return _this.props.deleteCommGroup({ groupId: groupId });

                            case 4:
                                response = _context.sent;
                                result = response.data;


                                if (result.IsSuccessful) {
                                    gettingParam = (0, _assign2.default)({}, _constant.defaultPagingOption);

                                    _this.props.getGroupSettingList(gettingParam);

                                    window.alert("Delete Commission Group Successfully");
                                } else {
                                    window.alert(result.Message);
                                }

                            case 7:
                            case 'end':
                                return _context.stop();
                        }
                    }
                }, _callee, _this2);
            }));

            return function (_x, _x2) {
                return _ref.apply(this, arguments);
            };
        }();

        var productList = _this.props.productList;

        _this.products = {};
        for (var i = 0; i < productList.length; i++) {
            _this.products[productList[i].ProductId] = productList[i].ProductName;
        }
        return _this;
    }

    (0, _createClass3.default)(GridData, [{
        key: '_convertToProductNameText',
        value: function _convertToProductNameText(productIdList) {
            var _this3 = this;

            return productIdList.split(',').reduce(function (accumulator, currentItem) {
                var productName = _this3.products[currentItem.trim()];
                if (productName) {
                    accumulator.push(productName);
                }

                return accumulator;
            }, []).join(',');
        }
    }, {
        key: '_renderGroupData',
        value: function _renderGroupData() {
            var _this4 = this;

            var gridData = this.props.gridData;

            var rows = [];

            var _loop = function _loop(index) {
                var item = gridData[index];

                var TierSettingList = item.TierSettingList,
                    PlatformFeeList = item.PlatformFeeList,
                    OperationalFee = item.OperationalFee,
                    BankingFee = item.BankingFee,
                    group = (0, _objectWithoutProperties3.default)(item, ['TierSettingList', 'PlatformFeeList', 'OperationalFee', 'BankingFee']);

                var tierSettingRowSpan = TierSettingList.length;
                var platformFeeRowSpan = PlatformFeeList.length;
                var rowSpan = tierSettingRowSpan + platformFeeRowSpan;

                for (var iTier = 0; iTier < TierSettingList.length; iTier++) {
                    var tierSetting = TierSettingList[iTier];
                    var isFirstItem = iTier === 0;

                    rows.push(_react2.default.createElement(
                        'tr',
                        { key: 'row-' + index + '-' + iTier },
                        isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-center', rowSpan: rowSpan },
                            group.RowIdx
                        ),
                        isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-center', rowSpan: rowSpan },
                            rawHtml(group.GroupName)
                        ),
                        isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-center', rowSpan: rowSpan },
                            group.NoAffs
                        ),
                        isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-center', rowSpan: rowSpan },
                            rawHtml(group.Description)
                        ),
                        isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-center', rowSpan: rowSpan },
                            _react2.default.createElement(
                                'p',
                                null,
                                group.Creator
                            ),
                            _react2.default.createElement(
                                'p',
                                null,
                                group.CreatedDateText
                            )
                        ),
                        isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-center font-weight-bold cell-tier', rowSpan: tierSettingRowSpan },
                            'Tier Setting'
                        ),
                        _react2.default.createElement(
                            'td',
                            { className: 'text-center cell-tier' },
                            tierSetting.TierId
                        ),
                        _react2.default.createElement(
                            'td',
                            { className: 'text-right cell-tier' },
                            (0, _numberFormat.format)(tierSetting.MinAmount)
                        ),
                        _react2.default.createElement(
                            'td',
                            { className: 'text-right cell-tier' },
                            tierSetting.MaxAmount === _constant.INFINITY_AM ? _constant.INFINITY_SYMBOL : (0, _numberFormat.format)(tierSetting.MaxAmount)
                        ),
                        _react2.default.createElement(
                            'td',
                            { className: 'text-right cell-tier' },
                            (0, _numberFormat.format)(tierSetting.MinQm)
                        ),
                        _react2.default.createElement(
                            'td',
                            { className: 'text-right cell-tier' },
                            tierSetting.MaxQm === _constant.INFINITY_QM ? _constant.INFINITY_SYMBOL : (0, _numberFormat.format)(tierSetting.MaxQm)
                        ),
                        _react2.default.createElement(
                            'td',
                            { className: 'text-right cell-tier' },
                            (0, _numberFormat.formatDecimal)(tierSetting.Commission)
                        ),
                        isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-right', rowSpan: rowSpan },
                            OperationalFee && (0, _numberFormat.formatDecimal)(OperationalFee.DeductedPercentage)
                        ),
                        isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-right', rowSpan: rowSpan },
                            BankingFee && (0, _numberFormat.formatDecimal)(BankingFee.DeductedPercentage)
                        ),
                        isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-right', rowSpan: rowSpan },
                            (0, _numberFormat.formatDecimal)(group.QmTurnover)
                        ),
                        isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-center', rowSpan: rowSpan },
                            _react2.default.createElement(
                                'button',
                                { className: 'btn btn-link', onClick: function onClick(e) {
                                        return _this4.props.onEdit(item.GroupId);
                                    } },
                                _react2.default.createElement('span', { className: 'oi oi-pencil text-secondary' })
                            ),
                            _react2.default.createElement(
                                'button',
                                { className: 'btn btn-link', onClick: function onClick() {
                                        return _this4.handleDelete(item.GroupId, item.GroupName);
                                    } },
                                _react2.default.createElement('span', { className: 'oi oi-x text-danger' })
                            )
                        )
                    ));
                }

                for (var iPlatformFee = 0; iPlatformFee < PlatformFeeList.length; iPlatformFee++) {
                    var platformFee = PlatformFeeList[iPlatformFee];
                    var _isFirstItem = iPlatformFee === 0;

                    rows.push(_react2.default.createElement(
                        'tr',
                        { key: 'row-platform-' + index + '-' + iPlatformFee },
                        _isFirstItem && _react2.default.createElement(
                            'td',
                            { className: 'text-center font-weight-bold cell-platform', rowSpan: platformFeeRowSpan },
                            'Platform Fee'
                        ),
                        _react2.default.createElement(
                            'td',
                            { className: 'cell-platform', colSpan: '5' },
                            _this4._convertToProductNameText(platformFee.ProductIds)
                        ),
                        _react2.default.createElement(
                            'td',
                            { className: 'text-right cell-platform' },
                            (0, _numberFormat.formatDecimal)(platformFee.DeductedPercentage)
                        )
                    ));
                }
            };

            for (var index = 0; index < gridData.length; index++) {
                _loop(index);
            }

            return rows;
        }
    }, {
        key: 'render',
        value: function render() {
            return _react2.default.createElement(
                'div',
                { className: 'table-responsive table-group-setting' },
                _react2.default.createElement(
                    'table',
                    { className: 'table table-bordered table-hover table-sm', style: { tableLayout: "fixed" } },
                    _react2.default.createElement(
                        'thead',
                        null,
                        _react2.default.createElement(
                            'tr',
                            null,
                            _react2.default.createElement('th', { rowSpan: '2', className: 'th-smallest' }),
                            _react2.default.createElement(
                                'th',
                                { rowSpan: '2', className: 'th-medium' },
                                'Name'
                            ),
                            _react2.default.createElement(
                                'th',
                                { rowSpan: '2', className: 'th-small' },
                                'No.Aff'
                            ),
                            _react2.default.createElement(
                                'th',
                                { rowSpan: '2', className: 'th-medium' },
                                'Description'
                            ),
                            _react2.default.createElement(
                                'th',
                                { rowSpan: '2' },
                                'Creation'
                            ),
                            _react2.default.createElement(
                                'th',
                                { colSpan: '10' },
                                'Policy'
                            ),
                            _react2.default.createElement(
                                'th',
                                { rowSpan: '2', className: 'th-small' },
                                'Action'
                            )
                        ),
                        _react2.default.createElement(
                            'tr',
                            null,
                            _react2.default.createElement('th', { className: 'cell-tier' }),
                            _react2.default.createElement(
                                'th',
                                { className: 'cell-tier th-smallest' },
                                'Tier'
                            ),
                            _react2.default.createElement(
                                'th',
                                { className: 'cell-tier', title: 'Minimum all members\' winlost amount' },
                                'Min AM($)'
                            ),
                            _react2.default.createElement(
                                'th',
                                { className: 'cell-tier', title: 'Maximum all members\' winlost amount' },
                                'Max AM($)'
                            ),
                            _react2.default.createElement(
                                'th',
                                { className: 'cell-tier', title: 'Minimum number of qualified members' },
                                'Min QM'
                            ),
                            _react2.default.createElement(
                                'th',
                                { className: 'cell-tier', title: 'Maximum number of qualified members' },
                                'Max QM'
                            ),
                            _react2.default.createElement(
                                'th',
                                { className: 'cell-tier' },
                                'Comm Rate'
                            ),
                            _react2.default.createElement(
                                'th',
                                null,
                                'Operational Fee'
                            ),
                            _react2.default.createElement(
                                'th',
                                null,
                                'Banking Fee'
                            ),
                            _react2.default.createElement(
                                'th',
                                null,
                                'Qm Turnover'
                            )
                        )
                    ),
                    _react2.default.createElement(
                        'tbody',
                        null,
                        this._renderGroupData()
                    )
                )
            );
        }
    }]);
    return GridData;
}(_react.Component);

function mapStateToProps(_ref2) {
    var gridData = _ref2.gridData,
        productList = _ref2.productList;

    return (0, _assign2.default)({}, { gridData: gridData, productList: productList });
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ deleteCommGroup: _actions.deleteCommGroup, getGroupSettingList: _actions.getGroupSettingList }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProps, mapDispatchToProps)(GridData);

/***/ }),

/***/ "./source/js/pages/commission-group/components/shared/CommGroupInfo.jsx":
/*!******************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/shared/CommGroupInfo.jsx ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.CommGroupInfo = undefined;

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _actions = __webpack_require__(/*! ../../actions */ "./source/js/pages/commission-group/actions/index.js");

var _reactstrap = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/dist/reactstrap.es.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var CommGroupInfo = exports.CommGroupInfo = function (_React$Component) {
    (0, _inherits3.default)(CommGroupInfo, _React$Component);

    function CommGroupInfo(props) {
        (0, _classCallCheck3.default)(this, CommGroupInfo);

        var _this = (0, _possibleConstructorReturn3.default)(this, (CommGroupInfo.__proto__ || (0, _getPrototypeOf2.default)(CommGroupInfo)).call(this, props));

        _this.handleChange = function (event) {
            var _event$target = event.target,
                name = _event$target.name,
                value = _event$target.value;

            _this.props.setSelectedGroup((0, _defineProperty3.default)({}, name, value));
        };

        return _this;
    }

    (0, _createClass3.default)(CommGroupInfo, [{
        key: 'render',
        value: function render() {
            var _props$selectedGroup = this.props.selectedGroup,
                GroupName = _props$selectedGroup.GroupName,
                Description = _props$selectedGroup.Description;


            return _react2.default.createElement(
                'div',
                null,
                _react2.default.createElement(
                    _reactstrap.FormGroup,
                    { row: true },
                    _react2.default.createElement(
                        _reactstrap.Label,
                        { 'for': 'GroupName', sm: 2 },
                        'Group Name: '
                    ),
                    _react2.default.createElement(
                        _reactstrap.Col,
                        { sm: 8 },
                        _react2.default.createElement(_reactstrap.Input, { type: 'text', name: 'GroupName', id: 'GroupName', value: GroupName, onChange: this.handleChange })
                    ),
                    _react2.default.createElement(
                        _reactstrap.Col,
                        { sm: 2 },
                        _react2.default.createElement(
                            'p',
                            { className: 'text-danger required' },
                            '*'
                        )
                    )
                ),
                _react2.default.createElement(
                    _reactstrap.FormGroup,
                    { row: true },
                    _react2.default.createElement(
                        _reactstrap.Label,
                        { 'for': 'Description', sm: 2 },
                        'Description: '
                    ),
                    _react2.default.createElement(
                        _reactstrap.Col,
                        { sm: 10 },
                        _react2.default.createElement(_reactstrap.Input, { type: 'text', name: 'Description', id: 'Description', value: Description, onChange: this.handleChange })
                    )
                )
            );
        }
    }]);
    return CommGroupInfo;
}(_react2.default.Component);

function mapStateToProp(_ref) {
    var selectedGroup = _ref.selectedGroup;

    return { selectedGroup: selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ setSelectedGroup: _actions.setSelectedGroup }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProp, mapDispatchToProps)(CommGroupInfo);

/***/ }),

/***/ "./source/js/pages/commission-group/components/shared/QmTurnOverSetting.jsx":
/*!**********************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/shared/QmTurnOverSetting.jsx ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.QmTurnOverSetting = undefined;

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _actions = __webpack_require__(/*! ../../actions */ "./source/js/pages/commission-group/actions/index.js");

var _reactstrap = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/dist/reactstrap.es.js");

var _numberFormat = __webpack_require__(/*! ../../../../modules/numberFormat */ "./source/js/modules/numberFormat.js");

var _utils = __webpack_require__(/*! ../../../../modules/utils */ "./source/js/modules/utils.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var QmTurnOverSetting = exports.QmTurnOverSetting = function (_React$Component) {
    (0, _inherits3.default)(QmTurnOverSetting, _React$Component);

    function QmTurnOverSetting(props) {
        (0, _classCallCheck3.default)(this, QmTurnOverSetting);

        var _this = (0, _possibleConstructorReturn3.default)(this, (QmTurnOverSetting.__proto__ || (0, _getPrototypeOf2.default)(QmTurnOverSetting)).call(this, props));

        _this.handleChange = function (event) {
            var _event$target = event.target,
                name = _event$target.name,
                value = _event$target.value;

            var actualValue = value.replace(/,/g, '');
            var isValid = (0, _utils.isValidNumber)(actualValue);

            if (isValid) {
                _this.props.setSelectedGroup((0, _defineProperty3.default)({}, name, Number(actualValue)));
            }
        };

        return _this;
    }

    (0, _createClass3.default)(QmTurnOverSetting, [{
        key: 'render',
        value: function render() {
            var _props$selectedGroup = this.props.selectedGroup,
                QmTurnover = _props$selectedGroup.QmTurnover,
                NextQmTurnover = _props$selectedGroup.NextQmTurnover,
                GroupId = _props$selectedGroup.GroupId;

            var isCreating = GroupId === 0;

            return _react2.default.createElement(
                'fieldset',
                null,
                _react2.default.createElement(
                    'legend',
                    null,
                    _react2.default.createElement(
                        'h6',
                        { className: 'font-weight-bold' },
                        'QM Turnover'
                    )
                ),
                _react2.default.createElement(
                    'p',
                    null,
                    'A member is considered a Qualified Member (QM) when his/her turnover in a month is equivalent to'
                ),
                isCreating ? _react2.default.createElement(
                    _reactstrap.Form,
                    { inline: true },
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(
                            _reactstrap.Label,
                            { 'for': 'QmTurnover', className: 'mr-sm-2' },
                            'USD'
                        ),
                        _react2.default.createElement(_reactstrap.Input, { name: 'QmTurnover', id: 'QmTurnover', value: (0, _numberFormat.format)(QmTurnover),
                            onChange: this.handleChange })
                    )
                ) : _react2.default.createElement(
                    _reactstrap.Form,
                    { inline: true },
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(
                            _reactstrap.Label,
                            { className: 'mr-sm-2' },
                            'Current: ',
                            (0, _numberFormat.format)(QmTurnover || 0),
                            ' | Next month: '
                        )
                    ),
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(
                            _reactstrap.Label,
                            { 'for': 'NextQmTurnover', className: 'mr-sm-2' },
                            'USD'
                        ),
                        _react2.default.createElement(_reactstrap.Input, { name: 'NextQmTurnover', id: 'NextQmTurnover', value: (0, _numberFormat.format)(NextQmTurnover || 0),
                            onChange: this.handleChange })
                    )
                )
            );
        }
    }]);
    return QmTurnOverSetting;
}(_react2.default.Component);

function mapStateToProp(_ref) {
    var selectedGroup = _ref.selectedGroup;

    return { selectedGroup: selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ setSelectedGroup: _actions.setSelectedGroup }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProp, mapDispatchToProps)(QmTurnOverSetting);

/***/ }),

/***/ "./source/js/pages/commission-group/components/shared/deduction/DeductionSetting.jsx":
/*!*******************************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/shared/deduction/DeductionSetting.jsx ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.DeductionSetting = undefined;

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _actions = __webpack_require__(/*! ../../../actions */ "./source/js/pages/commission-group/actions/index.js");

var _PlatformFeeSetting = __webpack_require__(/*! ./PlatformFeeSetting */ "./source/js/pages/commission-group/components/shared/deduction/PlatformFeeSetting.jsx");

var _PlatformFeeSetting2 = _interopRequireDefault(_PlatformFeeSetting);

var _FeeSettingItem = __webpack_require__(/*! ./FeeSettingItem */ "./source/js/pages/commission-group/components/shared/deduction/FeeSettingItem.jsx");

var _FeeSettingItem2 = _interopRequireDefault(_FeeSettingItem);

var _service = __webpack_require__(/*! ../../../modules/service */ "./source/js/pages/commission-group/modules/service.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var DeductionSetting = exports.DeductionSetting = function (_Component) {
    (0, _inherits3.default)(DeductionSetting, _Component);

    function DeductionSetting(props) {
        (0, _classCallCheck3.default)(this, DeductionSetting);

        var _this = (0, _possibleConstructorReturn3.default)(this, (DeductionSetting.__proto__ || (0, _getPrototypeOf2.default)(DeductionSetting)).call(this, props));

        _this.handleSelectOperationalFee = function (value) {
            var OperationalFee = _this.props.selectedGroup.OperationalFee;

            _this.props.setSelectedGroup({
                OperationalFee: (0, _assign2.default)({}, OperationalFee, value)
            });
        };

        _this.handleSelectBankingFee = function (value) {
            var BankingFee = _this.props.selectedGroup.BankingFee;

            _this.props.setSelectedGroup({
                BankingFee: (0, _assign2.default)({}, BankingFee, value)
            });
        };

        return _this;
    }

    (0, _createClass3.default)(DeductionSetting, [{
        key: 'render',
        value: function render() {
            var _props$selectedGroup = this.props.selectedGroup,
                OperationalFee = _props$selectedGroup.OperationalFee,
                BankingFee = _props$selectedGroup.BankingFee,
                GroupId = _props$selectedGroup.GroupId;

            var isCreating = GroupId === 0;

            return _react2.default.createElement(
                'fieldset',
                null,
                _react2.default.createElement(
                    'legend',
                    null,
                    _react2.default.createElement(
                        'h6',
                        { className: 'font-weight-bold' },
                        'Deductions'
                    )
                ),
                _react2.default.createElement(_PlatformFeeSetting2.default, null),
                _react2.default.createElement(_FeeSettingItem2.default, { label: 'Operational Fee%', basedValueName: '| Gross Win Loss |',
                    onChangeCommission: this.handleSelectOperationalFee,
                    data: OperationalFee,
                    isCreating: isCreating }),
                _react2.default.createElement(_FeeSettingItem2.default, { label: 'Banking Fee%', basedValueName: '(Deposit + Withdrawal)',
                    onChangeCommission: this.handleSelectBankingFee,
                    data: BankingFee,
                    isCreating: isCreating })
            );
        }
    }]);
    return DeductionSetting;
}(_react.Component);

function mapStateToProp(_ref) {
    var selectedGroup = _ref.selectedGroup;

    return { selectedGroup: selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ setSelectedGroup: _actions.setSelectedGroup }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProp, mapDispatchToProps)(DeductionSetting);

/***/ }),

/***/ "./source/js/pages/commission-group/components/shared/deduction/FeeSettingItem.jsx":
/*!*****************************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/shared/deduction/FeeSettingItem.jsx ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactstrap = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/dist/reactstrap.es.js");

var _numberFormat = __webpack_require__(/*! ../../../../../modules/numberFormat */ "./source/js/modules/numberFormat.js");

var _constant = __webpack_require__(/*! ../../../modules/constant */ "./source/js/pages/commission-group/modules/constant.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var FeeSettingItem = function (_React$Component) {
    (0, _inherits3.default)(FeeSettingItem, _React$Component);

    function FeeSettingItem(props) {
        (0, _classCallCheck3.default)(this, FeeSettingItem);

        var _this = (0, _possibleConstructorReturn3.default)(this, (FeeSettingItem.__proto__ || (0, _getPrototypeOf2.default)(FeeSettingItem)).call(this, props));

        _this.handleChangeComm = function (event) {
            var _event$target = event.target,
                name = _event$target.name,
                value = _event$target.value;

            _this.props.onChangeCommission((0, _defineProperty3.default)({}, name, Number(value)));
        };

        return _this;
    }

    (0, _createClass3.default)(FeeSettingItem, [{
        key: 'render',
        value: function render() {
            var _props = this.props,
                label = _props.label,
                data = _props.data,
                basedValueName = _props.basedValueName,
                onChange = _props.onChange,
                isCreating = _props.isCreating;

            return _react2.default.createElement(
                _reactstrap.FormGroup,
                { row: true },
                _react2.default.createElement(
                    _reactstrap.Label,
                    { sm: 4, className: 'font-weight-bold' },
                    label
                ),
                isCreating ? _react2.default.createElement(
                    _reactstrap.Col,
                    { sm: 2, className: 'select-comm' },
                    _react2.default.createElement(
                        'select',
                        { name: 'DeductedPercentage', className: 'form-control', onChange: onChange,
                            defaultValue: (0, _numberFormat.formatDecimal)(data && data.DeductedPercentage) },
                        _constant.commissionValueList.map(function (item, i) {
                            return _react2.default.createElement(
                                'option',
                                { key: 'feecomm-' + i, value: item },
                                item
                            );
                        })
                    )
                ) : _react2.default.createElement(
                    _reactstrap.Col,
                    { sm: 5, className: 'select-comm' },
                    _react2.default.createElement(
                        _reactstrap.Form,
                        { inline: true },
                        _react2.default.createElement(
                            _reactstrap.FormGroup,
                            { className: 'mb-2 mr-sm-2 mb-sm-0' },
                            _react2.default.createElement(
                                _reactstrap.Label,
                                { className: 'mr-sm-2' },
                                'Current: ',
                                (0, _numberFormat.formatDecimal)(data && data.DeductedPercentage || 0),
                                ' | Next month: '
                            )
                        ),
                        _react2.default.createElement(
                            _reactstrap.FormGroup,
                            { className: 'mb-2 mr-sm-2 mb-sm-0' },
                            _react2.default.createElement(
                                'select',
                                { name: 'NextDeductedPercentage', className: 'form-control',
                                    defaultValue: (0, _numberFormat.formatDecimal)(data && data.NextDeductedPercentage || 0),
                                    onChange: this.handleChangeComm, style: { width: 60 } },
                                _constant.commissionValueList.map(function (item, i) {
                                    return _react2.default.createElement(
                                        'option',
                                        { key: 'platformfee-nextmonth-comm-' + i, value: item },
                                        item
                                    );
                                })
                            )
                        )
                    )
                ),
                _react2.default.createElement(
                    _reactstrap.Col,
                    { sm: 3 },
                    _react2.default.createElement(
                        _reactstrap.Input,
                        { plaintext: true },
                        basedValueName
                    )
                )
            );
        }
    }]);
    return FeeSettingItem;
}(_react2.default.Component);

exports.default = FeeSettingItem;

/***/ }),

/***/ "./source/js/pages/commission-group/components/shared/deduction/PlatformFeeItem.jsx":
/*!******************************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/shared/deduction/PlatformFeeItem.jsx ***!
  \******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _multipleSelect = __webpack_require__(/*! ../../../../../components/multiple-select */ "./source/js/components/multiple-select/index.jsx");

var _multipleSelect2 = _interopRequireDefault(_multipleSelect);

var _reactstrap = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/dist/reactstrap.es.js");

var _constant = __webpack_require__(/*! ../../../modules/constant */ "./source/js/pages/commission-group/modules/constant.js");

var _numberFormat = __webpack_require__(/*! ../../../../../modules/numberFormat */ "./source/js/modules/numberFormat.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var PlatformFeeItem = function (_React$Component) {
    (0, _inherits3.default)(PlatformFeeItem, _React$Component);

    function PlatformFeeItem(props) {
        (0, _classCallCheck3.default)(this, PlatformFeeItem);

        var _this = (0, _possibleConstructorReturn3.default)(this, (PlatformFeeItem.__proto__ || (0, _getPrototypeOf2.default)(PlatformFeeItem)).call(this, props));

        _this.handleChange = function (selectedItem) {
            var onSelectProduct = _this.props.onSelectProduct;

            selectedItem && onSelectProduct && onSelectProduct(selectedItem);
        };

        _this.handleChangeComm = function (event) {
            var _event$target = event.target,
                name = _event$target.name,
                value = _event$target.value;

            _this.props.onChangeCommission((0, _defineProperty3.default)({}, name, Number(value)));
        };

        return _this;
    }

    (0, _createClass3.default)(PlatformFeeItem, [{
        key: "render",
        value: function render() {
            var _props = this.props,
                data = _props.data,
                handleClick = _props.handleClick,
                btnText = _props.btnText,
                isInserting = _props.isInserting;
            var productList = data.productList,
                commission = data.commission,
                nextCommission = data.nextCommission;


            return _react2.default.createElement(
                _reactstrap.FormGroup,
                { row: true },
                _react2.default.createElement(
                    _reactstrap.Col,
                    { sm: 4 },
                    _react2.default.createElement(
                        "div",
                        { className: "select-product" },
                        _react2.default.createElement(_multipleSelect2.default, {
                            dataSource: productList,
                            keyField: "ProductId",
                            valueField: "ProductName",
                            statusField: "Checked",
                            hasAllOption: false,
                            maxDisplayItemCount: 1,
                            noneSelectedLabel: "Please Select",
                            onChange: this.handleChange })
                    )
                ),
                isInserting ? _react2.default.createElement(
                    _reactstrap.Col,
                    { sm: 2, className: "select-comm" },
                    _react2.default.createElement(
                        "select",
                        { name: "DeductedPercentage", className: "form-control", value: (0, _numberFormat.formatDecimal)(commission),
                            onChange: this.handleChangeComm },
                        _constant.commissionValueList.map(function (item, i) {
                            return _react2.default.createElement(
                                "option",
                                { key: "platformfee-comm-" + i, value: item },
                                item
                            );
                        })
                    )
                ) : _react2.default.createElement(
                    _reactstrap.Col,
                    { sm: 5, className: "select-comm" },
                    _react2.default.createElement(
                        _reactstrap.Form,
                        { inline: true },
                        _react2.default.createElement(
                            _reactstrap.FormGroup,
                            { className: "mb-2 mr-sm-2 mb-sm-0" },
                            _react2.default.createElement(
                                _reactstrap.Label,
                                { className: "mr-sm-2" },
                                "Current: ",
                                (0, _numberFormat.formatDecimal)(commission || 0),
                                " | Next month: "
                            )
                        ),
                        _react2.default.createElement(
                            _reactstrap.FormGroup,
                            { className: "mb-2 mr-sm-2 mb-sm-0" },
                            _react2.default.createElement(
                                "select",
                                { name: "NextDeductedPercentage", className: "form-control",
                                    value: (0, _numberFormat.formatDecimal)(nextCommission || 0),
                                    onChange: this.handleChangeComm, style: { width: 60 } },
                                _constant.commissionValueList.map(function (item, i) {
                                    return _react2.default.createElement(
                                        "option",
                                        { key: "platformfee-nextmonth-comm-" + i, value: item },
                                        item
                                    );
                                })
                            )
                        )
                    )
                ),
                _react2.default.createElement(
                    _reactstrap.Col,
                    { sm: 2 },
                    _react2.default.createElement(
                        _reactstrap.Input,
                        { plaintext: true },
                        "| Gross Win Loss |"
                    )
                ),
                _react2.default.createElement(
                    _reactstrap.Col,
                    { sm: 1 },
                    _react2.default.createElement(
                        "button",
                        { type: "button", className: "btn btn-action", onClick: handleClick },
                        btnText
                    )
                )
            );
        }
    }]);
    return PlatformFeeItem;
}(_react2.default.Component);

exports.default = PlatformFeeItem;

/***/ }),

/***/ "./source/js/pages/commission-group/components/shared/deduction/PlatformFeeSetting.jsx":
/*!*********************************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/shared/deduction/PlatformFeeSetting.jsx ***!
  \*********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.PlatformFeeSetting = undefined;

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _extends2 = __webpack_require__(/*! babel-runtime/helpers/extends */ "./node_modules/babel-runtime/helpers/extends.js");

var _extends3 = _interopRequireDefault(_extends2);

var _toConsumableArray2 = __webpack_require__(/*! babel-runtime/helpers/toConsumableArray */ "./node_modules/babel-runtime/helpers/toConsumableArray.js");

var _toConsumableArray3 = _interopRequireDefault(_toConsumableArray2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _actions = __webpack_require__(/*! ../../../actions */ "./source/js/pages/commission-group/actions/index.js");

var _PlatformFeeItem = __webpack_require__(/*! ./PlatformFeeItem */ "./source/js/pages/commission-group/components/shared/deduction/PlatformFeeItem.jsx");

var _PlatformFeeItem2 = _interopRequireDefault(_PlatformFeeItem);

var _dataHandler = __webpack_require__(/*! ../../../../../modules/dataHandler */ "./source/js/modules/dataHandler.js");

var _service = __webpack_require__(/*! ../../../modules/service */ "./source/js/pages/commission-group/modules/service.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var PlatformFeeSetting = exports.PlatformFeeSetting = function (_Component) {
    (0, _inherits3.default)(PlatformFeeSetting, _Component);

    function PlatformFeeSetting(props) {
        (0, _classCallCheck3.default)(this, PlatformFeeSetting);

        var _this = (0, _possibleConstructorReturn3.default)(this, (PlatformFeeSetting.__proto__ || (0, _getPrototypeOf2.default)(PlatformFeeSetting)).call(this, props));

        _this.handleAdd = function () {
            var PlatformFeeList = _this.props.selectedGroup.PlatformFeeList;

            var platformFeeList = [].concat((0, _toConsumableArray3.default)(PlatformFeeList), [(0, _extends3.default)({}, _this.defaultItem)]);

            _this.props.setSelectedGroup({ PlatformFeeList: platformFeeList });
        };

        _this.handleRemove = function (index) {
            var PlatformFeeList = _this.props.selectedGroup.PlatformFeeList;

            var platformFeeList = (0, _dataHandler.deepClone)(PlatformFeeList);

            var rootProductIds = platformFeeList[0].ProductIds.split(',').filter(function (p) {
                return !!p;
            });
            var checkedProductIds = platformFeeList[index].ProductIds.split(',').filter(function (p) {
                return !!p;
            });

            for (var i = 0; i < checkedProductIds.length; i++) {
                var productId = checkedProductIds[i];

                rootProductIds.push(productId);
            }

            platformFeeList[0].ProductIds = rootProductIds.join(',');

            platformFeeList.splice(index, 1);

            _this.props.setSelectedGroup({ PlatformFeeList: platformFeeList });
        };

        _this.handleSelect = function (index, selectedItem) {
            var PlatformFeeList = _this.props.selectedGroup.PlatformFeeList;

            var isChecked = selectedItem.Checked;
            var platformFeeList = (0, _dataHandler.deepClone)(PlatformFeeList);
            var settingCount = platformFeeList.length;

            if (settingCount > 1) {
                if (isChecked) {
                    var currentProductIds = platformFeeList[index].ProductIds.split(',').filter(function (p) {
                        return !!p;
                    });
                    currentProductIds.push(selectedItem.ProductId.toString());
                    platformFeeList[index].ProductIds = currentProductIds.join(',');

                    for (var i = 0; i < platformFeeList.length; i++) {
                        if (i !== index) {
                            platformFeeList[i].ProductIds = platformFeeList[i].ProductIds.split(',').filter(function (p) {
                                return p && p !== selectedItem.ProductId.toString();
                            }).join(',');
                        }
                    }
                } else {
                    platformFeeList[index].ProductIds = platformFeeList[index].ProductIds.split(',').filter(function (p) {
                        return p && p !== selectedItem.ProductId.toString();
                    }).join(',');

                    var firstItemProductIds = platformFeeList[0].ProductIds.split(',').filter(function (p) {
                        return !!p;
                    });
                    firstItemProductIds.push(selectedItem.ProductId.toString());

                    platformFeeList[0].ProductIds = firstItemProductIds.join(',');
                }
            }

            _this.props.setSelectedGroup({ PlatformFeeList: platformFeeList });
        };

        _this.handleChangeCommission = function (index, value) {
            var PlatformFeeList = _this.props.selectedGroup.PlatformFeeList;

            var newCommissionSetting = (0, _assign2.default)({}, PlatformFeeList[index], value);

            var platformFeeList = (0, _dataHandler.updateObjectInArray)(PlatformFeeList, index, newCommissionSetting);

            _this.props.setSelectedGroup({ PlatformFeeList: platformFeeList });
        };

        var _this$props = _this.props,
            deductionList = _this$props.deductionList,
            productList = _this$props.productList;

        var deductionSetting = (0, _service.getDeductionSetting)(deductionList);
        var defaultPlatformSetting = (0, _service.createDefaultPlatformfee)(productList, deductionSetting.platformFeeSetting);

        _this.defaultItem = {
            DeductionId: defaultPlatformSetting.deductionId,
            ProductIds: '',
            DeductedPercentage: defaultPlatformSetting.commission,
            NextDeductedPercentage: defaultPlatformSetting.commission
        };
        return _this;
    }

    (0, _createClass3.default)(PlatformFeeSetting, [{
        key: 'render',
        value: function render() {
            var _this2 = this;

            var _props = this.props,
                productList = _props.productList,
                selectedGroup = _props.selectedGroup;
            var PlatformFeeList = selectedGroup.PlatformFeeList,
                GroupId = selectedGroup.GroupId;

            var platformFeeData = PlatformFeeList.map(function (item) {
                return {
                    productList: (0, _service.convertProductList)(productList, item.ProductIds),
                    commission: item.DeductedPercentage,
                    nextCommission: item.NextDeductedPercentage
                };
            });

            return _react2.default.createElement(
                'div',
                { className: 'platformfee-group' },
                _react2.default.createElement(
                    'p',
                    { className: 'font-weight-bold' },
                    'Platform Fee %'
                ),
                platformFeeData.map(function (item, i) {
                    var isFirst = i === 0;
                    var isInserting = GroupId === 0;
                    var handleClick = isFirst ? _this2.handleAdd : function () {
                        return _this2.handleRemove(i);
                    };

                    return _react2.default.createElement(_PlatformFeeItem2.default, { data: item, key: 'platformfee-' + i,
                        onSelectProduct: function onSelectProduct(selectedItem) {
                            return _this2.handleSelect(i, selectedItem);
                        },
                        onChangeCommission: function onChangeCommission(value) {
                            return _this2.handleChangeCommission(i, value);
                        },
                        handleClick: handleClick,
                        btnText: isFirst ? '+' : '-',
                        isInserting: isInserting });
                })
            );
        }
    }]);
    return PlatformFeeSetting;
}(_react.Component);

function mapStateToProp(_ref) {
    var deductionList = _ref.deductionList,
        productList = _ref.productList,
        selectedGroup = _ref.selectedGroup;

    return { deductionList: deductionList, productList: productList, selectedGroup: selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ setSelectedGroup: _actions.setSelectedGroup }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProp, mapDispatchToProps)(PlatformFeeSetting);

/***/ }),

/***/ "./source/js/pages/commission-group/components/shared/deduction/index.jsx":
/*!********************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/shared/deduction/index.jsx ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _DeductionSetting = __webpack_require__(/*! ./DeductionSetting */ "./source/js/pages/commission-group/components/shared/deduction/DeductionSetting.jsx");

var _DeductionSetting2 = _interopRequireDefault(_DeductionSetting);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = _DeductionSetting2.default;

/***/ }),

/***/ "./source/js/pages/commission-group/components/shared/tiersetting/TierSetting.jsx":
/*!****************************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/shared/tiersetting/TierSetting.jsx ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.TierSetting = undefined;

var _extends2 = __webpack_require__(/*! babel-runtime/helpers/extends */ "./node_modules/babel-runtime/helpers/extends.js");

var _extends3 = _interopRequireDefault(_extends2);

var _toConsumableArray2 = __webpack_require__(/*! babel-runtime/helpers/toConsumableArray */ "./node_modules/babel-runtime/helpers/toConsumableArray.js");

var _toConsumableArray3 = _interopRequireDefault(_toConsumableArray2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _actions = __webpack_require__(/*! ../../../actions */ "./source/js/pages/commission-group/actions/index.js");

var _TierSettingItem = __webpack_require__(/*! ./TierSettingItem */ "./source/js/pages/commission-group/components/shared/tiersetting/TierSettingItem.jsx");

var _TierSettingItem2 = _interopRequireDefault(_TierSettingItem);

var _dataHandler = __webpack_require__(/*! ../../../../../modules/dataHandler */ "./source/js/modules/dataHandler.js");

var _utils = __webpack_require__(/*! ../../../../../modules/utils */ "./source/js/modules/utils.js");

var _constant = __webpack_require__(/*! ../../../modules/constant */ "./source/js/pages/commission-group/modules/constant.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var TierSetting = exports.TierSetting = function (_Component) {
    (0, _inherits3.default)(TierSetting, _Component);

    function TierSetting(props) {
        (0, _classCallCheck3.default)(this, TierSetting);

        var _this = (0, _possibleConstructorReturn3.default)(this, (TierSetting.__proto__ || (0, _getPrototypeOf2.default)(TierSetting)).call(this, props));

        _this.handleAdd = function () {
            var TierSettingList = _this.props.selectedGroup.TierSettingList;

            var defaultMinAmount = 0;
            var defaultMinQm = 0;

            var itemCount = TierSettingList.length;
            if (itemCount > 0) {
                var lastItem = TierSettingList[itemCount - 1];
                defaultMinAmount = lastItem.MaxAmount + 1;
                defaultMinQm = lastItem.MaxQm + 1;
            }

            var newTierSetting = {
                TierId: itemCount + 1,
                MinAmount: defaultMinAmount,
                MaxAmount: defaultMinAmount,
                MinQm: defaultMinQm,
                MaxQm: defaultMinQm,
                Commission: 0
            };

            _this.props.setSelectedGroup({ TierSettingList: [].concat((0, _toConsumableArray3.default)(TierSettingList), [newTierSetting]) });
        };

        _this.handleRemove = function (index) {
            var TierSettingList = _this.props.selectedGroup.TierSettingList;

            var tierSettingList = (0, _dataHandler.removeItem)(index, TierSettingList).map(function (item, index) {
                return (0, _extends3.default)({}, item, {
                    TierId: index + 1
                });
            });

            _this.props.setSelectedGroup({ TierSettingList: tierSettingList });
        };

        _this.handleChangeInput = function (tierId, tierItemValue) {
            console.log(tierItemValue);
            var MinAmount = tierItemValue.MinAmount,
                MaxAmount = tierItemValue.MaxAmount,
                MinQm = tierItemValue.MinQm,
                MaxQm = tierItemValue.MaxQm;

            if ((!MinAmount || (0, _utils.isValidNumber)(MinAmount)) && (!MaxAmount || (0, _utils.isValidNumber)(MaxAmount)) && (!MinQm || (0, _utils.isValidNumber)(MinQm)) && (!MaxQm || (0, _utils.isValidNumber)(MaxQm))) {
                var TierSettingList = _this.props.selectedGroup.TierSettingList;


                var tierSettingList = TierSettingList.map(function (item) {
                    if (item.TierId === tierId) {
                        return (0, _extends3.default)({}, item, tierItemValue);
                    }

                    return item;
                });

                _this.props.setSelectedGroup({ TierSettingList: tierSettingList });
            }
        };

        return _this;
    }

    (0, _createClass3.default)(TierSetting, [{
        key: 'render',
        value: function render() {
            var _this2 = this;

            var TierSettingList = this.props.selectedGroup.TierSettingList;

            var canAddTier = TierSettingList.filter(function (item) {
                return item.MaxAmount === _constant.INFINITY_AM || item.MaxQm === _constant.INFINITY_QM;
            }).length > 0;

            return _react2.default.createElement(
                'fieldset',
                null,
                _react2.default.createElement(
                    'legend',
                    null,
                    _react2.default.createElement(
                        'h6',
                        { className: 'font-weight-bold' },
                        'Tier Setting'
                    )
                ),
                _react2.default.createElement(
                    'table',
                    { className: 'table table-bordered tier-setting-table', style: { tableLayout: "fixed" } },
                    _react2.default.createElement(
                        'thead',
                        null,
                        _react2.default.createElement(
                            'tr',
                            null,
                            _react2.default.createElement(
                                'th',
                                { style: { width: 40 } },
                                'Tier'
                            ),
                            _react2.default.createElement(
                                'th',
                                null,
                                'Min AM'
                            ),
                            _react2.default.createElement(
                                'th',
                                null,
                                'Max AM'
                            ),
                            _react2.default.createElement(
                                'th',
                                null,
                                'Min QM'
                            ),
                            _react2.default.createElement(
                                'th',
                                null,
                                'Max QM'
                            ),
                            _react2.default.createElement(
                                'th',
                                { style: { width: 85 } },
                                'Commission'
                            ),
                            _react2.default.createElement(
                                'th',
                                { style: { width: 50 } },
                                'Action'
                            )
                        )
                    ),
                    _react2.default.createElement(
                        'tbody',
                        null,
                        TierSettingList.length ? TierSettingList.map(function (item, i) {
                            return _react2.default.createElement(_TierSettingItem2.default, { data: item, key: 'tier-' + i,
                                isDisabledInfinity: i < TierSettingList.length - 1,
                                onRemove: function onRemove() {
                                    return _this2.handleRemove(i);
                                },
                                onChange: _this2.handleChangeInput });
                        }) : _react2.default.createElement(
                            'tr',
                            null,
                            _react2.default.createElement(
                                'td',
                                { colSpan: '7', className: 'text-center' },
                                'There is no information available'
                            )
                        )
                    )
                ),
                _react2.default.createElement(
                    'div',
                    null,
                    _react2.default.createElement(
                        'button',
                        { type: 'button', className: 'btn btn-addTier btn-secondary', onClick: this.handleAdd, disabled: canAddTier },
                        _react2.default.createElement('span', { className: 'oi oi-plus' }),
                        ' Add Tier'
                    )
                )
            );
        }
    }]);
    return TierSetting;
}(_react.Component);

function mapStateToProp(_ref) {
    var selectedGroup = _ref.selectedGroup;

    return { selectedGroup: selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ setSelectedGroup: _actions.setSelectedGroup }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProp, mapDispatchToProps)(TierSetting);

/***/ }),

/***/ "./source/js/pages/commission-group/components/shared/tiersetting/TierSettingItem.jsx":
/*!********************************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/shared/tiersetting/TierSettingItem.jsx ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactstrap = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/dist/reactstrap.es.js");

var _constant = __webpack_require__(/*! ../../../modules/constant */ "./source/js/pages/commission-group/modules/constant.js");

var _numberFormat = __webpack_require__(/*! ../../../../../modules/numberFormat */ "./source/js/modules/numberFormat.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var TierSettingItem = function (_React$Component) {
    (0, _inherits3.default)(TierSettingItem, _React$Component);

    function TierSettingItem(props) {
        (0, _classCallCheck3.default)(this, TierSettingItem);

        var _this = (0, _possibleConstructorReturn3.default)(this, (TierSettingItem.__proto__ || (0, _getPrototypeOf2.default)(TierSettingItem)).call(this, props));

        _this.handleChange = function (event) {
            var target = event.target;
            var value = Number(target.value.replace(/,/g, ''));
            var name = target.name;
            var _this$props = _this.props,
                data = _this$props.data,
                onChange = _this$props.onChange;


            var newItem = (0, _defineProperty3.default)({}, name, value || 0);

            onChange && onChange(data.TierId, newItem);
        };

        _this.handleCheck = function (event) {
            var isChecked = event.target.checked;
            var _this$props2 = _this.props,
                data = _this$props2.data,
                onChange = _this$props2.onChange;


            var newState = isChecked ? (0, _assign2.default)({}, data, { MaxAmount: _constant.INFINITY_AM, MaxQm: _constant.INFINITY_QM }) : (0, _assign2.default)({}, data, { MaxAmount: data.MinAmount, MaxQm: data.MinQm });

            onChange && onChange(data.TierId, newState);
        };

        return _this;
    }

    (0, _createClass3.default)(TierSettingItem, [{
        key: 'render',
        value: function render() {
            var _props = this.props,
                data = _props.data,
                onRemove = _props.onRemove,
                isDisabledInfinity = _props.isDisabledInfinity;
            var TierId = data.TierId,
                MinAmount = data.MinAmount,
                MaxAmount = data.MaxAmount,
                MinQm = data.MinQm,
                MaxQm = data.MaxQm,
                Commission = data.Commission;

            var isDisabled = MaxAmount === _constant.INFINITY_AM || MaxQm === _constant.INFINITY_QM;

            return _react2.default.createElement(
                'tr',
                null,
                _react2.default.createElement(
                    'td',
                    { className: 'text-center' },
                    TierId
                ),
                _react2.default.createElement(
                    'td',
                    null,
                    _react2.default.createElement(_reactstrap.Input, { type: 'text', name: 'MinAmount', id: 'MinAmount',
                        value: (0, _numberFormat.format)(MinAmount), onChange: this.handleChange })
                ),
                _react2.default.createElement(
                    'td',
                    null,
                    _react2.default.createElement(
                        _reactstrap.Form,
                        { inline: true },
                        _react2.default.createElement(_reactstrap.Input, { type: 'text', name: 'MaxAmount', id: 'MaxAmount', className: 'form-max', disabled: isDisabled,
                            value: isDisabled ? _constant.INFINITY_SYMBOL : (0, _numberFormat.format)(MaxAmount), onChange: this.handleChange }),
                        _react2.default.createElement(
                            _reactstrap.FormGroup,
                            { check: true, className: 'ml-sm-1' },
                            _react2.default.createElement(
                                _reactstrap.Label,
                                { check: true },
                                _react2.default.createElement(_reactstrap.Input, { type: 'checkbox', checked: isDisabled,
                                    onChange: this.handleCheck, disabled: isDisabledInfinity }),
                                _constant.INFINITY_SYMBOL
                            )
                        )
                    )
                ),
                _react2.default.createElement(
                    'td',
                    null,
                    _react2.default.createElement(_reactstrap.Input, { type: 'text', name: 'MinQm', id: 'MinQm', value: (0, _numberFormat.format)(MinQm), onChange: this.handleChange })
                ),
                _react2.default.createElement(
                    'td',
                    null,
                    _react2.default.createElement(
                        _reactstrap.Form,
                        { inline: true },
                        _react2.default.createElement(_reactstrap.Input, { type: 'text', name: 'MaxQm', id: 'MaxQm', className: 'form-max', disabled: isDisabled,
                            value: isDisabled ? _constant.INFINITY_SYMBOL : (0, _numberFormat.format)(MaxQm), onChange: this.handleChange }),
                        _react2.default.createElement(
                            _reactstrap.FormGroup,
                            { check: true, className: 'ml-sm-1' },
                            _react2.default.createElement(
                                _reactstrap.Label,
                                { check: true },
                                _react2.default.createElement(_reactstrap.Input, { type: 'checkbox', checked: isDisabled,
                                    onChange: this.handleCheck, disabled: isDisabledInfinity }),
                                _constant.INFINITY_SYMBOL
                            )
                        )
                    )
                ),
                _react2.default.createElement(
                    'td',
                    null,
                    _react2.default.createElement(
                        'select',
                        { className: 'form-control', name: 'Commission',
                            onChange: this.handleChange, value: (0, _numberFormat.formatDecimal)(Commission) },
                        _constant.commissionValueList.map(function (item, i) {
                            return _react2.default.createElement(
                                'option',
                                { key: 'comm-' + TierId + '-' + i, value: item },
                                item
                            );
                        })
                    )
                ),
                _react2.default.createElement(
                    'td',
                    { className: 'text-center' },
                    _react2.default.createElement(
                        'a',
                        { href: 'javacript:;', onClick: onRemove },
                        _react2.default.createElement('span', { className: 'oi oi-x text-danger' })
                    )
                )
            );
        }
    }]);
    return TierSettingItem;
}(_react2.default.Component);

exports.default = TierSettingItem;

/***/ }),

/***/ "./source/js/pages/commission-group/components/shared/tiersetting/index.jsx":
/*!**********************************************************************************!*\
  !*** ./source/js/pages/commission-group/components/shared/tiersetting/index.jsx ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _TierSetting = __webpack_require__(/*! ./TierSetting */ "./source/js/pages/commission-group/components/shared/tiersetting/TierSetting.jsx");

var _TierSetting2 = _interopRequireDefault(_TierSetting);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = _TierSetting2.default;

/***/ }),

/***/ "./source/js/pages/commission-group/index.jsx":
/*!****************************************************!*\
  !*** ./source/js/pages/commission-group/index.jsx ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactDom = __webpack_require__(/*! react-dom */ "./node_modules/react-dom/index.js");

var _reactDom2 = _interopRequireDefault(_reactDom);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _reduxThunk = __webpack_require__(/*! redux-thunk */ "./node_modules/redux-thunk/es/index.js");

var _reduxThunk2 = _interopRequireDefault(_reduxThunk);

var _reducers = __webpack_require__(/*! ./reducers */ "./source/js/pages/commission-group/reducers/index.js");

var _reducers2 = _interopRequireDefault(_reducers);

var _AppContainer = __webpack_require__(/*! ./components/AppContainer */ "./source/js/pages/commission-group/components/AppContainer.jsx");

var _AppContainer2 = _interopRequireDefault(_AppContainer);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function configureStore(initialState) {
  var enhancer = (0, _redux.compose)((0, _redux.applyMiddleware)(_reduxThunk2.default // lets us dispatch() functions
  ));

  return (0, _redux.createStore)(_reducers2.default, initialState, enhancer);
}

var store = configureStore(window.initialState);

var App = function (_React$Component) {
  (0, _inherits3.default)(App, _React$Component);

  function App(props) {
    (0, _classCallCheck3.default)(this, App);
    return (0, _possibleConstructorReturn3.default)(this, (App.__proto__ || (0, _getPrototypeOf2.default)(App)).call(this, props));
  }

  (0, _createClass3.default)(App, [{
    key: "render",
    value: function render() {
      return _react2.default.createElement(
        _reactRedux.Provider,
        { store: store },
        _react2.default.createElement(_AppContainer2.default, null)
      );
    }
  }]);
  return App;
}(_react2.default.Component);

exports.default = App;


_reactDom2.default.render(_react2.default.createElement(App, null), document.getElementById("root"));

/***/ }),

/***/ "./source/js/pages/commission-group/modules/constant.js":
/*!**************************************************************!*\
  !*** ./source/js/pages/commission-group/modules/constant.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.deductionNameList = exports.messages = exports.commissionValueList = exports.defaultPagingOption = exports.INFINITY_SYMBOL = exports.INFINITY_QM = exports.INFINITY_AM = undefined;

var _numberFormat = __webpack_require__(/*! ../../../modules/numberFormat */ "./source/js/modules/numberFormat.js");

var INFINITY_AM = exports.INFINITY_AM = 100000000000000;
var INFINITY_QM = exports.INFINITY_QM = 1000000000;
var INFINITY_SYMBOL = exports.INFINITY_SYMBOL = '∞';

var defaultPagingOption = exports.defaultPagingOption = {
    PageIndex: 1,
    PageSize: 20,
    TotalItem: 0,
    PageList: [10, 20, 50, 100, 500]
};

function generateCommissionValues() {
    var min = 0,
        max = 1;
    var commissionList = ['0.00'];

    while (min < max) {
        min = min + 0.01;
        commissionList.push((0, _numberFormat.formatDecimal)(min));
    }

    return commissionList;
}
var _commValueList = generateCommissionValues();

var commissionValueList = exports.commissionValueList = _commValueList;

var _validationMessage = {
    requireGroupName: 'Group Name cannot be empty. Please check!',
    requireTier: 'Tier Setting must have at least 1 tier',
    requireInfinity: 'Last Tier Setting must have maximum value=∞',
    requireQmTurnover: 'QM Turnover value is required',
    amountConstrain: 'Max Amount must be greater than or equal to Min Amount',
    qmConstrain: 'Max QM must be greater than or equal to Min QM'
};
var messages = exports.messages = _validationMessage;

var _deductionNameList = {
    platformFee: 'Platform Fee',
    operationalFee: 'Operational Fee',
    bankingFee: 'Banking Fee'
};
var deductionNameList = exports.deductionNameList = _deductionNameList;

/***/ }),

/***/ "./source/js/pages/commission-group/modules/service.js":
/*!*************************************************************!*\
  !*** ./source/js/pages/commission-group/modules/service.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.validate = exports.convertProductList = exports.convertDeductionList = exports.createDefaultPlatformfee = exports.buildPlatformFeeList = exports.createDefaultBankingSetting = exports.createDefaultOperationalSetting = exports.getDeductionSetting = exports.buildDefaultCommGroup = undefined;

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _constant = __webpack_require__(/*! ../modules/constant */ "./source/js/pages/commission-group/modules/constant.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var emptyCommGroup = {
    GroupId: 0,
    GroupName: '',
    Description: '',
    QmTurnover: 0,
    TierSettingList: [],
    PlatformFeeList: [],
    OperationalFee: {},
    BankingFee: {}
};

var buildDefaultCommGroup = exports.buildDefaultCommGroup = function buildDefaultCommGroup(deductionList, productList) {
    var deductionSetting = getDeductionSetting(deductionList);

    var platformFeeParam = { productList: productList, deductionSetting: deductionSetting, platformFeeList: [] };
    var platformFeeList = buildPlatformFeeList(platformFeeParam);

    var operationalFee = createDefaultOperationalSetting(deductionSetting.operationalFeeSetting);
    var bankingFee = createDefaultBankingSetting(deductionSetting.bankingFeeSetting);

    return (0, _assign2.default)({}, emptyCommGroup, {
        PlatformFeeList: platformFeeList,
        OperationalFee: operationalFee,
        BankingFee: bankingFee
    });
};

var getDeductionSetting = exports.getDeductionSetting = function getDeductionSetting(deductionList) {
    var deductionObj = convertDeductionList(deductionList);
    var platformFeeSetting = deductionObj[_constant.deductionNameList.platformFee];
    var operationalFeeSetting = deductionObj[_constant.deductionNameList.operationalFee];
    var bankingFeeSetting = deductionObj[_constant.deductionNameList.bankingFee];

    return {
        platformFeeSetting: platformFeeSetting,
        operationalFeeSetting: operationalFeeSetting,
        bankingFeeSetting: bankingFeeSetting
    };
};

var createDefaultOperationalSetting = exports.createDefaultOperationalSetting = function createDefaultOperationalSetting(operationalFeeSetting) {
    return {
        DeductionId: operationalFeeSetting.DeductionId,
        ProductIds: 0,
        DeductedPercentage: operationalFeeSetting.DeductedPercentage
    };
};

var createDefaultBankingSetting = exports.createDefaultBankingSetting = function createDefaultBankingSetting(bankingFeeSetting) {
    return {
        DeductionId: bankingFeeSetting.DeductionId,
        ProductIds: 0,
        DeductedPercentage: bankingFeeSetting.DeductedPercentage
    };
};

var buildPlatformFeeList = exports.buildPlatformFeeList = function buildPlatformFeeList(_ref) {
    var productList = _ref.productList,
        deductionSetting = _ref.deductionSetting,
        productIds = _ref.productIds,
        _ref$platformFeeList = _ref.platformFeeList,
        platformFeeList = _ref$platformFeeList === undefined ? [] : _ref$platformFeeList;
    var platformFeeSetting = deductionSetting.platformFeeSetting;


    var products = convertProductList(productList, productIds);
    var convertedPlatformFeeList = platformFeeList.length ? platformFeeList : [{
        DeductionId: platformFeeSetting.DeductionId,
        ProductIds: products.map(function (p) {
            return p.ProductId;
        }).join(','),
        DeductedPercentage: platformFeeSetting.DeductedPercentage
    }];

    return convertedPlatformFeeList;
};

var createDefaultPlatformfee = exports.createDefaultPlatformfee = function createDefaultPlatformfee(productList, platformFeeSetting) {
    return {
        deductionId: platformFeeSetting.DeductionId,
        productList: convertProductList(productList),
        commission: platformFeeSetting.DeductedPercentage
    };
};

var convertDeductionList = exports.convertDeductionList = function convertDeductionList(deductionList) {
    var deductions = {};
    for (var i = 0; i < deductionList.length; i++) {
        var item = deductionList[i];
        deductions[item.DeductionName] = item;
    }

    return deductions;
};

var convertProductList = exports.convertProductList = function convertProductList(productList) {
    var productIds = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : '';

    var idList = productIds.split(',').filter(function (p) {
        return !!p;
    });
    var isCheckAll = idList.length === productList.length;

    return productList.map(function (item) {
        return {
            ProductId: item.ProductId,
            ProductName: item.ProductName,
            Checked: isCheckAll || idList.filter(function (i) {
                return Number(i) === item.ProductId;
            }).length > 0
        };
    });
};

var validate = exports.validate = function validate(selectedGroup) {
    var isValid = true;
    var messageList = [];

    if (!selectedGroup.GroupName) {
        isValid = false;
        messageList.push(_constant.messages.requireGroupName);
    }

    if (isNaN(selectedGroup.QmTurnover) || typeof selectedGroup.QmTurnover !== 'number') {
        isValid = false;
        messageList.push(_constant.messages.requireQmTurnover);
    }

    if (selectedGroup.TierSettingList.length === 0) {
        isValid = false;
        messageList.push(_constant.messages.requireTier);
    } else if (selectedGroup.TierSettingList.filter(function (item) {
        return item.MaxAmount === _constant.INFINITY_AM && item.MaxQm === _constant.INFINITY_QM;
    }).length === 0) {
        isValid = false;
        messageList.push(_constant.messages.requireInfinity);
    } else if (selectedGroup.TierSettingList.filter(function (item) {
        return item.MinAmount > item.MaxAmount;
    }).length > 0) {
        isValid = false;
        messageList.push(_constant.messages.amountConstrain);
    } else if (selectedGroup.TierSettingList.filter(function (item) {
        return item.MinQm > item.MaxQm;
    }).length > 0) {
        isValid = false;
        messageList.push(_constant.messages.qmConstrain);
    }

    return { isValid: isValid, messageList: messageList };
};

/***/ }),

/***/ "./source/js/pages/commission-group/reducers/index.js":
/*!************************************************************!*\
  !*** ./source/js/pages/commission-group/reducers/index.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _types = __webpack_require__(/*! ../actions/types */ "./source/js/pages/commission-group/actions/types.js");

var _createReducer4 = __webpack_require__(/*! ../../../modules/createReducer */ "./source/js/modules/createReducer.js");

var _createReducer5 = _interopRequireDefault(_createReducer4);

var _constant = __webpack_require__(/*! ../modules/constant */ "./source/js/pages/commission-group/modules/constant.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var gridData = (0, _createReducer5.default)([], (0, _defineProperty3.default)({}, _types.SET_GRIDDATA, function (prevState, action) {
    return action.data;
}));

var pagingOption = (0, _createReducer5.default)(_constant.defaultPagingOption, (0, _defineProperty3.default)({}, _types.SET_PAGING_OPTION, function (prevState, action) {
    return (0, _assign2.default)({}, prevState, action.data);
}));

var selectedGroup = (0, _createReducer5.default)(null, (0, _defineProperty3.default)({}, _types.SET_SELECTED_GROUP, function (prevState, action) {
    return (0, _assign2.default)({}, prevState, action.data);
}));

var productList = (0, _createReducer5.default)([], {});
var deductionList = (0, _createReducer5.default)([], {});

exports.default = (0, _redux.combineReducers)({ gridData: gridData, pagingOption: pagingOption, productList: productList, deductionList: deductionList, selectedGroup: selectedGroup });

/***/ }),

/***/ "dll-reference react_4b9f8d5b9e3dd1ee3943":
/*!*********************************************!*\
  !*** external "react_4b9f8d5b9e3dd1ee3943" ***!
  \*********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = react_4b9f8d5b9e3dd1ee3943;

/***/ }),

/***/ "dll-reference reactstrap_4b9f8d5b9e3dd1ee3943":
/*!**************************************************!*\
  !*** external "reactstrap_4b9f8d5b9e3dd1ee3943" ***!
  \**************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = reactstrap_4b9f8d5b9e3dd1ee3943;

/***/ })

/******/ });
//# sourceMappingURL=commission-group.js.map