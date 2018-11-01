/******/ (function(modules) { // webpackBootstrap
/******/ 	// install a JSONP callback for chunk loading
/******/ 	function webpackJsonpCallback(data) {
/******/ 		var chunkIds = data[0];
/******/ 		var moreModules = data[1];
/******/
/******/
/******/ 		// add "moreModules" to the modules object,
/******/ 		// then flag all "chunkIds" as loaded and fire callback
/******/ 		var moduleId, chunkId, i = 0, resolves = [];
/******/ 		for(;i < chunkIds.length; i++) {
/******/ 			chunkId = chunkIds[i];
/******/ 			if(installedChunks[chunkId]) {
/******/ 				resolves.push(installedChunks[chunkId][0]);
/******/ 			}
/******/ 			installedChunks[chunkId] = 0;
/******/ 		}
/******/ 		for(moduleId in moreModules) {
/******/ 			if(Object.prototype.hasOwnProperty.call(moreModules, moduleId)) {
/******/ 				modules[moduleId] = moreModules[moduleId];
/******/ 			}
/******/ 		}
/******/ 		if(parentJsonpFunction) parentJsonpFunction(data);
/******/
/******/ 		while(resolves.length) {
/******/ 			resolves.shift()();
/******/ 		}
/******/
/******/ 	};
/******/
/******/
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// object to store loaded and loading chunks
/******/ 	// undefined = chunk not loaded, null = chunk preloaded/prefetched
/******/ 	// Promise = chunk loading, 0 = chunk loaded
/******/ 	var installedChunks = {
/******/ 		"Areas/Portal/www/dist/js/pages/affiliate-info": 0
/******/ 	};
/******/
/******/
/******/
/******/ 	// script path function
/******/ 	function jsonpScriptSrc(chunkId) {
/******/ 		return __webpack_require__.p + "Areas/Portal/www/dist/js/dynamic-chunks/" + ({"vendors~xlsx-style":"vendors~xlsx-style","xlsx-style":"xlsx-style"}[chunkId]||chunkId) + ".chunk.js"
/******/ 	}
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
/******/ 	// This file contains only the entry chunk.
/******/ 	// The chunk loading function for additional chunks
/******/ 	__webpack_require__.e = function requireEnsure(chunkId) {
/******/ 		var promises = [];
/******/
/******/
/******/ 		// JSONP chunk loading for javascript
/******/
/******/ 		var installedChunkData = installedChunks[chunkId];
/******/ 		if(installedChunkData !== 0) { // 0 means "already installed".
/******/
/******/ 			// a Promise means "currently loading".
/******/ 			if(installedChunkData) {
/******/ 				promises.push(installedChunkData[2]);
/******/ 			} else {
/******/ 				// setup Promise in chunk cache
/******/ 				var promise = new Promise(function(resolve, reject) {
/******/ 					installedChunkData = installedChunks[chunkId] = [resolve, reject];
/******/ 				});
/******/ 				promises.push(installedChunkData[2] = promise);
/******/
/******/ 				// start chunk loading
/******/ 				var head = document.getElementsByTagName('head')[0];
/******/ 				var script = document.createElement('script');
/******/ 				var onScriptComplete;
/******/
/******/ 				script.charset = 'utf-8';
/******/ 				script.timeout = 120;
/******/ 				if (__webpack_require__.nc) {
/******/ 					script.setAttribute("nonce", __webpack_require__.nc);
/******/ 				}
/******/ 				script.src = jsonpScriptSrc(chunkId);
/******/
/******/ 				onScriptComplete = function (event) {
/******/ 					// avoid mem leaks in IE.
/******/ 					script.onerror = script.onload = null;
/******/ 					clearTimeout(timeout);
/******/ 					var chunk = installedChunks[chunkId];
/******/ 					if(chunk !== 0) {
/******/ 						if(chunk) {
/******/ 							var errorType = event && (event.type === 'load' ? 'missing' : event.type);
/******/ 							var realSrc = event && event.target && event.target.src;
/******/ 							var error = new Error('Loading chunk ' + chunkId + ' failed.\n(' + errorType + ': ' + realSrc + ')');
/******/ 							error.type = errorType;
/******/ 							error.request = realSrc;
/******/ 							chunk[1](error);
/******/ 						}
/******/ 						installedChunks[chunkId] = undefined;
/******/ 					}
/******/ 				};
/******/ 				var timeout = setTimeout(function(){
/******/ 					onScriptComplete({ type: 'timeout', target: script });
/******/ 				}, 120000);
/******/ 				script.onerror = script.onload = onScriptComplete;
/******/ 				head.appendChild(script);
/******/ 			}
/******/ 		}
/******/ 		return Promise.all(promises);
/******/ 	};
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
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// on error function for async loading
/******/ 	__webpack_require__.oe = function(err) { console.error(err); throw err; };
/******/
/******/ 	var jsonpArray = window["webpackJsonp"] = window["webpackJsonp"] || [];
/******/ 	var oldJsonpFunction = jsonpArray.push.bind(jsonpArray);
/******/ 	jsonpArray.push = webpackJsonpCallback;
/******/ 	jsonpArray = jsonpArray.slice();
/******/ 	for(var i = 0; i < jsonpArray.length; i++) webpackJsonpCallback(jsonpArray[i]);
/******/ 	var parentJsonpFunction = oldJsonpFunction;
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./Areas/Portal/www/source/js/pages/affiliate-info/index.jsx");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./Areas/Portal/www/node_modules/@9softstudio/react-fixed-table-header/dist/index.js":
/*!*******************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/@9softstudio/react-fixed-table-header/dist/index.js ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

!function(e,t){ true?module.exports=t(__webpack_require__(/*! react */ "./Areas/Portal/www/node_modules/@9softstudio/react-fixed-table-header/node_modules/react/index.js")):undefined}(window,function(e){return function(e){var t={};function n(r){if(t[r])return t[r].exports;var o=t[r]={i:r,l:!1,exports:{}};return e[r].call(o.exports,o,o.exports,n),o.l=!0,o.exports}return n.m=e,n.c=t,n.d=function(e,t,r){n.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:r})},n.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},n.t=function(e,t){if(1&t&&(e=n(e)),8&t)return e;if(4&t&&"object"==typeof e&&e&&e.__esModule)return e;var r=Object.create(null);if(n.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var o in e)n.d(r,o,function(t){return e[t]}.bind(null,o));return r},n.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return n.d(t,"a",t),t},n.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},n.p="/dist/",n(n.s=4)}([function(t,n){t.exports=e},function(e,t,n){e.exports=n(7)()},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});t.SCROLLBAR_WIDTH=19,t.GUTTER=15,t.DEFAULT_MILLISECOND_FOR_WAITING=500,t.DEFAULT_COLUMN_WIDTH=100},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=n(0),a=u(o),i=u(n(1));function u(e){return e&&e.__esModule?e:{default:e}}var l=function(e){function t(e){!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t);var n=function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.onKeyUpHandle=function(e,t){if(13===(e.keyCode?e.keyCode:e.which)){var r=parseInt(n.state.inputValue);r>0&&r<=t&&n.goToPage(r,n.props.pageOption.PageSize)}},n.onInputChangeHandler=function(e){var t=e.target.value;/^\d*$/.test(t)&&n._updateState(t)},n.state={inputValue:n.props.pageOption.PageIndex},n}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.Component),r(t,[{key:"componentDidUpdate",value:function(e,t){var n=this.props.pageOption.PageIndex;n!==e.pageOption.PageIndex&&this._updateState(n)}},{key:"goToPage",value:function(e,t){this.props.onPaging&&this.props.onPaging(e,t)}},{key:"onClickHandle",value:function(e,t){e||t()}},{key:"_updateState",value:function(e){this.setState({inputValue:e})}},{key:"_renderPageSizeSelection",value:function(e,t,n){var r=this;return a.default.createElement("select",{className:"pagesize-select",disabled:e,value:n,onChange:function(e){return r.goToPage(1,Number(e.target.value))}},this._renderPageSizeOption(this.props.pageOption.PageList))}},{key:"_renderPageSizeOption",value:function(e){return e.map(function(e,t){return a.default.createElement("option",{key:"pagesize"+t,value:e},e)})}},{key:"_renderPageIcon",value:function(e,t,n){var r=this;return a.default.createElement("a",{href:"javascript:;",className:"icon "+e+" "+(t?"disable":""),onClick:function(){return r.onClickHandle(t,n)}})}},{key:"_renderPageInput",value:function(e,t){var n=this;return a.default.createElement("div",{className:"page-input"},a.default.createElement("span",null,"Page "),a.default.createElement("input",{type:"text",className:"page-form",value:e,maxLength:"4",size:"2",onChange:this.onInputChangeHandler,onKeyUp:function(e){return n.onKeyUpHandle(e,t)}}),a.default.createElement("span",null," of "+t," "))}},{key:"_renderPageLabel",value:function(e,t){return a.default.createElement("div",{className:"page-input"},a.default.createElement("span",null,"Page ",e+" of "+t+" "))}},{key:"_renderPageInfo",value:function(e,t,n){var r=(e-1)*t+1,o=e*t;return a.default.createElement("span",{className:"page-info"},"View "+r+"-"+(o>n?n:o)+" of "+n)}},{key:"render",value:function(){var e=this,t=this.props,n=t.isShowPagingInfo,r=t.isAllowInputPageIndex,o=t.pageOption,i=o.PageIndex,u=o.PageSize,l=o.TotalItem,c=o.PageList,f=this.state.inputValue,s=isNaN(parseInt(f)),p=l>c[0],d=Math.ceil(l/u);return a.default.createElement("div",{className:"page-container center-align",style:{display:p?"block":"none"}},this._renderPageIcon("page-first",1===i,function(){return e.goToPage(1,u)}),this._renderPageIcon("page-prev",s||i<=1||i>d,function(){return e.goToPage(i-1,u)}),r?this._renderPageInput(f,d):this._renderPageLabel(i,d),this._renderPageIcon("page-next",s||i>=d||i<1,function(){return e.goToPage(i+1,u)}),this._renderPageIcon("page-last",i===d,function(){return e.goToPage(d,u)}),this._renderPageSizeSelection(s,i,u),n&&this._renderPageInfo(i,u,l))}}]),t}();l.propTypes={pageOption:i.default.shape({PageIndex:i.default.number,PageSize:i.default.number,PageList:i.default.arrayOf(i.default.number),TotalItem:i.default.number}),onPaging:i.default.func,isShowPagingInfo:i.default.bool,isAllowInputPageIndex:i.default.bool},l.defaultProps={pageOption:{PageIndex:1,PageSize:50,PageList:[50,100,200],TotalItem:0},isShowPagingInfo:!0,isAllowInputPageIndex:!0},t.default=l},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),t.Pager=t.Cell=t.Row=t.Table=void 0;var r=u(n(5)),o=u(n(10)),a=u(n(11)),i=u(n(3));function u(e){return e&&e.__esModule?e:{default:e}}t.Table=r.default,t.Row=o.default,t.Cell=a.default,t.Pager=i.default},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e},o=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),a=n(0),i=p(a),u=p(n(6)),l=p(n(1)),c=p(n(9)),f=n(2),s=p(n(3));function p(e){return e&&e.__esModule?e:{default:e}}var d=document.body.clientWidth;function h(e,t){var n=void 0;return function(){var r=this,o=arguments;clearTimeout(n),n=setTimeout(function(){e.apply(r,o)},t||f.DEFAULT_MILLISECOND_FOR_WAITING)}}var y=function(e){function t(e){!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t);var n=function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));b.call(n),n.bodyWrapper=null,n.footerWrapper=null;n.Header=(0,c.default)({className:"header-content",isHeader:!0});var r={className:"body-content",getRef:function(e){return n.bodyWrapper=e}};n.Body=(0,c.default)(r);var o={className:"footer-content",getRef:function(e){return n.footerWrapper=e}};n.Footer=(0,c.default)(o);var a=n.props,i=a.maxWidth,u=a.bodyHeight,l=a.adjustedHeight,f=i||d;return n.diffWidth=d-f,n.adjustedHeight=l,n.state={maxWidth:f,contentHeight:u,columnsWidth:n._getColumnsWidth(e.header)},n}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,a.Component),o(t,[{key:"_getColumnsWidth",value:function(e){var t=this,n=Array.isArray(e);if(n&&0===e.length)return null;var r=[],o={};return function n(a,i){for(var u=0;u<a.length;u++){var l=a[u];if(l){var c=l.props,s=Number(c.colSpan);if(s&&s>1){var p=i+1;void 0===o[p]&&(o[p]=0);var d=o[p],h=d+s-1,y=e[p].props.children,b=t._getNextColspanCells(y,d,h);o[p]+=b.length,n(b,p)}else{var g=c.colWidth?c.colWidth:f.DEFAULT_COLUMN_WIDTH;r.push(g)}}}}(n?e[0].props.children:e.props.children,0),r}},{key:"_getNextColspanCells",value:function(e,t,n){for(var r=[],o=t;o<=n;o++){var a=e[o];a.props.colSpan&&(n-=1),r.push(a)}return r}},{key:"componentDidMount",value:function(){this._handleResize(),window.addEventListener("resize",h(this._handleResize))}},{key:"componentWillUnmount",value:function(){window.removeEventListener("resize",h(this._handleResize))}},{key:"componentDidUpdate",value:function(e,t){this.props.header!=e.header&&this.setState({columnsWidth:this._getColumnsWidth(this.props.header)}),e.body!=this.props.body&&function(e,t){var n=-e.scrollTop/(t/15),r=setInterval(function(){0!=e.scrollTop?e.scrollBy(0,n):clearInterval(r)},15)}(this.bodyWrapper,200)}},{key:"_calculateBodyHeight",value:function(){var e=window.innerHeight,t=this.footerWrapper?this.footerWrapper.offsetHeight:0;return e-(this.bodyWrapper.offsetTop+t+70)-this.adjustedHeight}},{key:"_getUpdatedColumnLayout",value:function(){var e=this,t=this.props,n=t.width,r=t.autoWidth,o=t.containerPadding,a=r?this.state.maxWidth:n;return a=a-o-f.SCROLLBAR_WIDTH,this.columnWidthSum&&this.state.columnsWidth.map(function(t){return a/e.columnWidthSum*t})}},{key:"render",value:function(){var e=this.props,t=e.width,n=e.autoWidth,o=e.minWidth,a=e.header,l=e.body,c=e.footer,f=e.isPaging,p=e.pageOption,d=e.onPaging,h=e.containerPadding,y=this.state.maxWidth-h,b=t-h,g=this.columnWidthSum&&this.columnWidthSum!==b?this._getUpdatedColumnLayout():this.state.columnsWidth;if(!g)return null;var m={width:b,autoWidth:n,minWidth:o,maxWidth:y},v=i.default.createElement(u.default,{columnLayout:g}),_=this.Header,O=this.Body,P=this.Footer;return i.default.createElement("div",null,i.default.createElement("div",{className:"table-container",style:{maxWidth:y}},i.default.createElement(_,m,v,a),l&&i.default.createElement(O,r({},m,{maxHeight:this.state.contentHeight}),v,l),c&&i.default.createElement(P,m,v,c)),f&&p&&i.default.createElement(s.default,{pageOption:p,onPaging:d}))}},{key:"columnWidthSum",get:function(){var e=this.state.columnsWidth;return e&&e.length&&!e.find(function(e){return"number"!=typeof e})?e.reduce(function(e,t){return e+t},0):null}}]),t}();y.propTypes={width:l.default.oneOfType([l.default.number,l.default.string]),maxWidth:l.default.number,minWidth:l.default.number,autoWidth:l.default.bool,bodyHeight:l.default.number,autoHeight:l.default.bool,header:l.default.oneOfType([l.default.object,l.default.arrayOf(l.default.object)]).isRequired,body:l.default.arrayOf(l.default.object),footer:l.default.arrayOf(l.default.object),isPaging:l.default.bool,onPaging:l.default.func,pageOption:l.default.object,adjustedHeight:l.default.number,containerPadding:l.default.number},y.defaultProps={width:d,maxWidth:d,autoWidth:!0,isPaging:!1,adjustedHeight:0,bodyHeight:0,autoHeight:!0,containerPadding:30};var b=function(){var e=this;this._handleResize=function(t){if(t&&t.preventDefault(),e.bodyWrapper&&e.props.autoHeight){var n=e._calculateBodyHeight();e.state.contentHeight!==n&&e.setState({contentHeight:n})}var r=document.body.clientWidth-e.diffWidth;e.state.maxWidth!==r&&e.setState({maxWidth:r})}};t.default=y},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=n(0),a=u(o),i=u(n(1));function u(e){return e&&e.__esModule?e:{default:e}}var l=function(e){function t(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.Component),r(t,[{key:"_getRowLayout",value:function(){return this.props.columnLayout.map(function(e,t){var n=isNaN(e)?e:e+"px";return a.default.createElement("td",{key:"cellLayout"+t,style:{width:n}})})}},{key:"render",value:function(){return a.default.createElement("tr",{className:"first-row"},this._getRowLayout())}}]),t}();l.propTypes={columnLayout:i.default.arrayOf(i.default.oneOfType([i.default.number,i.default.string])).isRequired},t.default=l},function(e,t,n){"use strict";var r=n(8);function o(){}e.exports=function(){function e(e,t,n,o,a,i){if(i!==r){var u=new Error("Calling PropTypes validators directly is not supported by the `prop-types` package. Use PropTypes.checkPropTypes() to call them. Read more at http://fb.me/use-check-prop-types");throw u.name="Invariant Violation",u}}function t(){return e}e.isRequired=e;var n={array:e,bool:e,func:e,number:e,object:e,string:e,symbol:e,any:e,arrayOf:t,element:e,instanceOf:t,node:e,objectOf:t,oneOf:t,oneOfType:t,shape:t,exact:t};return n.checkPropTypes=o,n.PropTypes=n,n}},function(e,t,n){"use strict";e.exports="SECRET_DO_NOT_PASS_THIS_OR_YOU_WILL_BE_FIRED"},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e},o=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),a=n(0),i=c(a),u=c(n(1)),l=n(2);function c(e){return e&&e.__esModule?e:{default:e}}function f(e,t){var n={};for(var r in e)t.indexOf(r)>=0||Object.prototype.hasOwnProperty.call(e,r)&&(n[r]=e[r]);return n}t.default=function(e,t){var n,c;return c=n=function(n){function u(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,u),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(u.__proto__||Object.getPrototypeOf(u)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(u,a.Component),o(u,[{key:"render",value:function(){var n=this.props,o=n.tableClass,a=(n.width,n.autoWidth,n.maxWidth,n.minWidth,n.maxHeight,f(n,["tableClass","width","autoWidth","maxWidth","minWidth","maxHeight"])),u=e.isHeader,l=e.getRef,c=f(e,["isHeader","getRef"]);return i.default.createElement("div",r({},a,{style:this.containerStyle,ref:l},c),i.default.createElement("div",{className:"table-wrapper"},i.default.createElement("table",r({style:this.tableStyle,className:o},t),u?i.default.createElement("thead",null,this.props.children):i.default.createElement("tbody",null,this.props.children))))}},{key:"containerWidth",get:function(){var e=this.props,t=e.autoWidth,n=e.maxWidth,r=e.width;return t?n:r}},{key:"containerStyle",get:function(){var e=this.props,t=e.minWidth,n=e.maxHeight;return r({},this.props.style,{width:this.containerWidth,minWidth:t,maxHeight:n})}},{key:"tableStyle",get:function(){var e=this.props.minWidth;return{width:(e?Math.max(this.containerWidth,e):this.containerWidth)-l.SCROLLBAR_WIDTH}}}]),u}(),n.propTypes={tableClass:u.default.string,width:u.default.oneOfType([u.default.number,u.default.string]).isRequired,maxWidth:u.default.number.isRequired,minWidth:u.default.number,autoWidth:u.default.bool,maxHeight:u.default.number},n.defaultProps={tableClass:"table",autoWidth:!0},c}},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=n(0),a=function(e){return e&&e.__esModule?e:{default:e}}(o);var i=function(e){function t(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.Component),r(t,[{key:"render",value:function(){return a.default.createElement("tr",this.props,this.props.children)}}]),t}();t.default=i},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e},o=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),a=n(0),i=l(a),u=l(n(1));function l(e){return e&&e.__esModule?e:{default:e}}var c=function(e){function t(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,a.Component),o(t,[{key:"render",value:function(){var e=this.props,t=e.header,n=(e.colWidth,e.sortable),o=e.sorting,a=e.asc,u=e.onClick,l=function(e,t){var n={};for(var r in e)t.indexOf(r)>=0||Object.prototype.hasOwnProperty.call(e,r)&&(n[r]=e[r]);return n}(e,["header","colWidth","sortable","sorting","asc","onClick"]);return t?n?i.default.createElement("th",r({},l,{className:"sortable "+(o?"sorting":""),onClick:u}),this.props.children,i.default.createElement("span",{className:o?a?"asc":"desc":""})):i.default.createElement("th",l,this.props.children):i.default.createElement("td",l,this.props.children)}}]),t}();c.propTypes={header:u.default.bool,sortable:u.default.bool,sorting:u.default.bool,asc:u.default.bool,onClick:u.default.func},c.defaultProps={header:!1,sortable:!1,sorting:!1,asc:!0,onClick:function(){}},t.default=c}])});

/***/ }),

/***/ "./Areas/Portal/www/node_modules/@9softstudio/react-fixed-table-header/node_modules/react/cjs/react.development.js":
/*!*************************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/@9softstudio/react-fixed-table-header/node_modules/react/cjs/react.development.js ***!
  \*************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/** @license React v16.4.2
 * react.development.js
 *
 * Copyright (c) 2013-present, Facebook, Inc.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */





if (true) {
  (function() {
'use strict';

var _assign = __webpack_require__(/*! object-assign */ "./Areas/Portal/www/node_modules/object-assign/index.js");
var invariant = __webpack_require__(/*! fbjs/lib/invariant */ "./Areas/Portal/www/node_modules/fbjs/lib/invariant.js");
var emptyObject = __webpack_require__(/*! fbjs/lib/emptyObject */ "./Areas/Portal/www/node_modules/fbjs/lib/emptyObject.js");
var warning = __webpack_require__(/*! fbjs/lib/warning */ "./Areas/Portal/www/node_modules/fbjs/lib/warning.js");
var emptyFunction = __webpack_require__(/*! fbjs/lib/emptyFunction */ "./Areas/Portal/www/node_modules/fbjs/lib/emptyFunction.js");
var checkPropTypes = __webpack_require__(/*! prop-types/checkPropTypes */ "./Areas/Portal/www/node_modules/prop-types/checkPropTypes.js");

// TODO: this is special because it gets imported during build.

var ReactVersion = '16.4.2';

// The Symbol used to tag the ReactElement-like types. If there is no native Symbol
// nor polyfill, then a plain number is used for performance.
var hasSymbol = typeof Symbol === 'function' && Symbol.for;

var REACT_ELEMENT_TYPE = hasSymbol ? Symbol.for('react.element') : 0xeac7;
var REACT_PORTAL_TYPE = hasSymbol ? Symbol.for('react.portal') : 0xeaca;
var REACT_FRAGMENT_TYPE = hasSymbol ? Symbol.for('react.fragment') : 0xeacb;
var REACT_STRICT_MODE_TYPE = hasSymbol ? Symbol.for('react.strict_mode') : 0xeacc;
var REACT_PROFILER_TYPE = hasSymbol ? Symbol.for('react.profiler') : 0xead2;
var REACT_PROVIDER_TYPE = hasSymbol ? Symbol.for('react.provider') : 0xeacd;
var REACT_CONTEXT_TYPE = hasSymbol ? Symbol.for('react.context') : 0xeace;
var REACT_ASYNC_MODE_TYPE = hasSymbol ? Symbol.for('react.async_mode') : 0xeacf;
var REACT_FORWARD_REF_TYPE = hasSymbol ? Symbol.for('react.forward_ref') : 0xead0;
var REACT_TIMEOUT_TYPE = hasSymbol ? Symbol.for('react.timeout') : 0xead1;

var MAYBE_ITERATOR_SYMBOL = typeof Symbol === 'function' && Symbol.iterator;
var FAUX_ITERATOR_SYMBOL = '@@iterator';

function getIteratorFn(maybeIterable) {
  if (maybeIterable === null || typeof maybeIterable === 'undefined') {
    return null;
  }
  var maybeIterator = MAYBE_ITERATOR_SYMBOL && maybeIterable[MAYBE_ITERATOR_SYMBOL] || maybeIterable[FAUX_ITERATOR_SYMBOL];
  if (typeof maybeIterator === 'function') {
    return maybeIterator;
  }
  return null;
}

// Relying on the `invariant()` implementation lets us
// have preserve the format and params in the www builds.

// Exports ReactDOM.createRoot


// Experimental error-boundary API that can recover from errors within a single
// render phase

// Suspense
var enableSuspense = false;
// Helps identify side effects in begin-phase lifecycle hooks and setState reducers:


// In some cases, StrictMode should also double-render lifecycles.
// This can be confusing for tests though,
// And it can be bad for performance in production.
// This feature flag can be used to control the behavior:


// To preserve the "Pause on caught exceptions" behavior of the debugger, we
// replay the begin phase of a failed component inside invokeGuardedCallback.


// Warn about deprecated, async-unsafe lifecycles; relates to RFC #6:


// Warn about legacy context API


// Gather advanced timing metrics for Profiler subtrees.


// Only used in www builds.

/**
 * Forked from fbjs/warning:
 * https://github.com/facebook/fbjs/blob/e66ba20ad5be433eb54423f2b097d829324d9de6/packages/fbjs/src/__forks__/warning.js
 *
 * Only change is we use console.warn instead of console.error,
 * and do nothing when 'console' is not supported.
 * This really simplifies the code.
 * ---
 * Similar to invariant but only logs a warning if the condition is not met.
 * This can be used to log issues in development environments in critical
 * paths. Removing the logging code for production environments will keep the
 * same logic and follow the same code paths.
 */

var lowPriorityWarning = function () {};

{
  var printWarning = function (format) {
    for (var _len = arguments.length, args = Array(_len > 1 ? _len - 1 : 0), _key = 1; _key < _len; _key++) {
      args[_key - 1] = arguments[_key];
    }

    var argIndex = 0;
    var message = 'Warning: ' + format.replace(/%s/g, function () {
      return args[argIndex++];
    });
    if (typeof console !== 'undefined') {
      console.warn(message);
    }
    try {
      // --- Welcome to debugging React ---
      // This error was thrown as a convenience so that you can use this stack
      // to find the callsite that caused this warning to fire.
      throw new Error(message);
    } catch (x) {}
  };

  lowPriorityWarning = function (condition, format) {
    if (format === undefined) {
      throw new Error('`warning(condition, format, ...args)` requires a warning ' + 'message argument');
    }
    if (!condition) {
      for (var _len2 = arguments.length, args = Array(_len2 > 2 ? _len2 - 2 : 0), _key2 = 2; _key2 < _len2; _key2++) {
        args[_key2 - 2] = arguments[_key2];
      }

      printWarning.apply(undefined, [format].concat(args));
    }
  };
}

var lowPriorityWarning$1 = lowPriorityWarning;

var didWarnStateUpdateForUnmountedComponent = {};

function warnNoop(publicInstance, callerName) {
  {
    var _constructor = publicInstance.constructor;
    var componentName = _constructor && (_constructor.displayName || _constructor.name) || 'ReactClass';
    var warningKey = componentName + '.' + callerName;
    if (didWarnStateUpdateForUnmountedComponent[warningKey]) {
      return;
    }
    warning(false, "Can't call %s on a component that is not yet mounted. " + 'This is a no-op, but it might indicate a bug in your application. ' + 'Instead, assign to `this.state` directly or define a `state = {};` ' + 'class property with the desired state in the %s component.', callerName, componentName);
    didWarnStateUpdateForUnmountedComponent[warningKey] = true;
  }
}

/**
 * This is the abstract API for an update queue.
 */
var ReactNoopUpdateQueue = {
  /**
   * Checks whether or not this composite component is mounted.
   * @param {ReactClass} publicInstance The instance we want to test.
   * @return {boolean} True if mounted, false otherwise.
   * @protected
   * @final
   */
  isMounted: function (publicInstance) {
    return false;
  },

  /**
   * Forces an update. This should only be invoked when it is known with
   * certainty that we are **not** in a DOM transaction.
   *
   * You may want to call this when you know that some deeper aspect of the
   * component's state has changed but `setState` was not called.
   *
   * This will not invoke `shouldComponentUpdate`, but it will invoke
   * `componentWillUpdate` and `componentDidUpdate`.
   *
   * @param {ReactClass} publicInstance The instance that should rerender.
   * @param {?function} callback Called after component is updated.
   * @param {?string} callerName name of the calling function in the public API.
   * @internal
   */
  enqueueForceUpdate: function (publicInstance, callback, callerName) {
    warnNoop(publicInstance, 'forceUpdate');
  },

  /**
   * Replaces all of the state. Always use this or `setState` to mutate state.
   * You should treat `this.state` as immutable.
   *
   * There is no guarantee that `this.state` will be immediately updated, so
   * accessing `this.state` after calling this method may return the old value.
   *
   * @param {ReactClass} publicInstance The instance that should rerender.
   * @param {object} completeState Next state.
   * @param {?function} callback Called after component is updated.
   * @param {?string} callerName name of the calling function in the public API.
   * @internal
   */
  enqueueReplaceState: function (publicInstance, completeState, callback, callerName) {
    warnNoop(publicInstance, 'replaceState');
  },

  /**
   * Sets a subset of the state. This only exists because _pendingState is
   * internal. This provides a merging strategy that is not available to deep
   * properties which is confusing. TODO: Expose pendingState or don't use it
   * during the merge.
   *
   * @param {ReactClass} publicInstance The instance that should rerender.
   * @param {object} partialState Next partial state to be merged with state.
   * @param {?function} callback Called after component is updated.
   * @param {?string} Name of the calling function in the public API.
   * @internal
   */
  enqueueSetState: function (publicInstance, partialState, callback, callerName) {
    warnNoop(publicInstance, 'setState');
  }
};

/**
 * Base class helpers for the updating state of a component.
 */
function Component(props, context, updater) {
  this.props = props;
  this.context = context;
  this.refs = emptyObject;
  // We initialize the default updater but the real one gets injected by the
  // renderer.
  this.updater = updater || ReactNoopUpdateQueue;
}

Component.prototype.isReactComponent = {};

/**
 * Sets a subset of the state. Always use this to mutate
 * state. You should treat `this.state` as immutable.
 *
 * There is no guarantee that `this.state` will be immediately updated, so
 * accessing `this.state` after calling this method may return the old value.
 *
 * There is no guarantee that calls to `setState` will run synchronously,
 * as they may eventually be batched together.  You can provide an optional
 * callback that will be executed when the call to setState is actually
 * completed.
 *
 * When a function is provided to setState, it will be called at some point in
 * the future (not synchronously). It will be called with the up to date
 * component arguments (state, props, context). These values can be different
 * from this.* because your function may be called after receiveProps but before
 * shouldComponentUpdate, and this new state, props, and context will not yet be
 * assigned to this.
 *
 * @param {object|function} partialState Next partial state or function to
 *        produce next partial state to be merged with current state.
 * @param {?function} callback Called after state is updated.
 * @final
 * @protected
 */
Component.prototype.setState = function (partialState, callback) {
  !(typeof partialState === 'object' || typeof partialState === 'function' || partialState == null) ? invariant(false, 'setState(...): takes an object of state variables to update or a function which returns an object of state variables.') : void 0;
  this.updater.enqueueSetState(this, partialState, callback, 'setState');
};

/**
 * Forces an update. This should only be invoked when it is known with
 * certainty that we are **not** in a DOM transaction.
 *
 * You may want to call this when you know that some deeper aspect of the
 * component's state has changed but `setState` was not called.
 *
 * This will not invoke `shouldComponentUpdate`, but it will invoke
 * `componentWillUpdate` and `componentDidUpdate`.
 *
 * @param {?function} callback Called after update is complete.
 * @final
 * @protected
 */
Component.prototype.forceUpdate = function (callback) {
  this.updater.enqueueForceUpdate(this, callback, 'forceUpdate');
};

/**
 * Deprecated APIs. These APIs used to exist on classic React classes but since
 * we would like to deprecate them, we're not going to move them over to this
 * modern base class. Instead, we define a getter that warns if it's accessed.
 */
{
  var deprecatedAPIs = {
    isMounted: ['isMounted', 'Instead, make sure to clean up subscriptions and pending requests in ' + 'componentWillUnmount to prevent memory leaks.'],
    replaceState: ['replaceState', 'Refactor your code to use setState instead (see ' + 'https://github.com/facebook/react/issues/3236).']
  };
  var defineDeprecationWarning = function (methodName, info) {
    Object.defineProperty(Component.prototype, methodName, {
      get: function () {
        lowPriorityWarning$1(false, '%s(...) is deprecated in plain JavaScript React classes. %s', info[0], info[1]);
        return undefined;
      }
    });
  };
  for (var fnName in deprecatedAPIs) {
    if (deprecatedAPIs.hasOwnProperty(fnName)) {
      defineDeprecationWarning(fnName, deprecatedAPIs[fnName]);
    }
  }
}

function ComponentDummy() {}
ComponentDummy.prototype = Component.prototype;

/**
 * Convenience component with default shallow equality check for sCU.
 */
function PureComponent(props, context, updater) {
  this.props = props;
  this.context = context;
  this.refs = emptyObject;
  this.updater = updater || ReactNoopUpdateQueue;
}

var pureComponentPrototype = PureComponent.prototype = new ComponentDummy();
pureComponentPrototype.constructor = PureComponent;
// Avoid an extra prototype jump for these methods.
_assign(pureComponentPrototype, Component.prototype);
pureComponentPrototype.isPureReactComponent = true;

// an immutable object with a single mutable value
function createRef() {
  var refObject = {
    current: null
  };
  {
    Object.seal(refObject);
  }
  return refObject;
}

/**
 * Keeps track of the current owner.
 *
 * The current owner is the component who should own any components that are
 * currently being constructed.
 */
var ReactCurrentOwner = {
  /**
   * @internal
   * @type {ReactComponent}
   */
  current: null
};

var hasOwnProperty = Object.prototype.hasOwnProperty;

var RESERVED_PROPS = {
  key: true,
  ref: true,
  __self: true,
  __source: true
};

var specialPropKeyWarningShown = void 0;
var specialPropRefWarningShown = void 0;

function hasValidRef(config) {
  {
    if (hasOwnProperty.call(config, 'ref')) {
      var getter = Object.getOwnPropertyDescriptor(config, 'ref').get;
      if (getter && getter.isReactWarning) {
        return false;
      }
    }
  }
  return config.ref !== undefined;
}

function hasValidKey(config) {
  {
    if (hasOwnProperty.call(config, 'key')) {
      var getter = Object.getOwnPropertyDescriptor(config, 'key').get;
      if (getter && getter.isReactWarning) {
        return false;
      }
    }
  }
  return config.key !== undefined;
}

function defineKeyPropWarningGetter(props, displayName) {
  var warnAboutAccessingKey = function () {
    if (!specialPropKeyWarningShown) {
      specialPropKeyWarningShown = true;
      warning(false, '%s: `key` is not a prop. Trying to access it will result ' + 'in `undefined` being returned. If you need to access the same ' + 'value within the child component, you should pass it as a different ' + 'prop. (https://fb.me/react-special-props)', displayName);
    }
  };
  warnAboutAccessingKey.isReactWarning = true;
  Object.defineProperty(props, 'key', {
    get: warnAboutAccessingKey,
    configurable: true
  });
}

function defineRefPropWarningGetter(props, displayName) {
  var warnAboutAccessingRef = function () {
    if (!specialPropRefWarningShown) {
      specialPropRefWarningShown = true;
      warning(false, '%s: `ref` is not a prop. Trying to access it will result ' + 'in `undefined` being returned. If you need to access the same ' + 'value within the child component, you should pass it as a different ' + 'prop. (https://fb.me/react-special-props)', displayName);
    }
  };
  warnAboutAccessingRef.isReactWarning = true;
  Object.defineProperty(props, 'ref', {
    get: warnAboutAccessingRef,
    configurable: true
  });
}

/**
 * Factory method to create a new React element. This no longer adheres to
 * the class pattern, so do not use new to call it. Also, no instanceof check
 * will work. Instead test $$typeof field against Symbol.for('react.element') to check
 * if something is a React Element.
 *
 * @param {*} type
 * @param {*} key
 * @param {string|object} ref
 * @param {*} self A *temporary* helper to detect places where `this` is
 * different from the `owner` when React.createElement is called, so that we
 * can warn. We want to get rid of owner and replace string `ref`s with arrow
 * functions, and as long as `this` and owner are the same, there will be no
 * change in behavior.
 * @param {*} source An annotation object (added by a transpiler or otherwise)
 * indicating filename, line number, and/or other information.
 * @param {*} owner
 * @param {*} props
 * @internal
 */
var ReactElement = function (type, key, ref, self, source, owner, props) {
  var element = {
    // This tag allows us to uniquely identify this as a React Element
    $$typeof: REACT_ELEMENT_TYPE,

    // Built-in properties that belong on the element
    type: type,
    key: key,
    ref: ref,
    props: props,

    // Record the component responsible for creating this element.
    _owner: owner
  };

  {
    // The validation flag is currently mutative. We put it on
    // an external backing store so that we can freeze the whole object.
    // This can be replaced with a WeakMap once they are implemented in
    // commonly used development environments.
    element._store = {};

    // To make comparing ReactElements easier for testing purposes, we make
    // the validation flag non-enumerable (where possible, which should
    // include every environment we run tests in), so the test framework
    // ignores it.
    Object.defineProperty(element._store, 'validated', {
      configurable: false,
      enumerable: false,
      writable: true,
      value: false
    });
    // self and source are DEV only properties.
    Object.defineProperty(element, '_self', {
      configurable: false,
      enumerable: false,
      writable: false,
      value: self
    });
    // Two elements created in two different places should be considered
    // equal for testing purposes and therefore we hide it from enumeration.
    Object.defineProperty(element, '_source', {
      configurable: false,
      enumerable: false,
      writable: false,
      value: source
    });
    if (Object.freeze) {
      Object.freeze(element.props);
      Object.freeze(element);
    }
  }

  return element;
};

/**
 * Create and return a new ReactElement of the given type.
 * See https://reactjs.org/docs/react-api.html#createelement
 */
function createElement(type, config, children) {
  var propName = void 0;

  // Reserved names are extracted
  var props = {};

  var key = null;
  var ref = null;
  var self = null;
  var source = null;

  if (config != null) {
    if (hasValidRef(config)) {
      ref = config.ref;
    }
    if (hasValidKey(config)) {
      key = '' + config.key;
    }

    self = config.__self === undefined ? null : config.__self;
    source = config.__source === undefined ? null : config.__source;
    // Remaining properties are added to a new props object
    for (propName in config) {
      if (hasOwnProperty.call(config, propName) && !RESERVED_PROPS.hasOwnProperty(propName)) {
        props[propName] = config[propName];
      }
    }
  }

  // Children can be more than one argument, and those are transferred onto
  // the newly allocated props object.
  var childrenLength = arguments.length - 2;
  if (childrenLength === 1) {
    props.children = children;
  } else if (childrenLength > 1) {
    var childArray = Array(childrenLength);
    for (var i = 0; i < childrenLength; i++) {
      childArray[i] = arguments[i + 2];
    }
    {
      if (Object.freeze) {
        Object.freeze(childArray);
      }
    }
    props.children = childArray;
  }

  // Resolve default props
  if (type && type.defaultProps) {
    var defaultProps = type.defaultProps;
    for (propName in defaultProps) {
      if (props[propName] === undefined) {
        props[propName] = defaultProps[propName];
      }
    }
  }
  {
    if (key || ref) {
      if (typeof props.$$typeof === 'undefined' || props.$$typeof !== REACT_ELEMENT_TYPE) {
        var displayName = typeof type === 'function' ? type.displayName || type.name || 'Unknown' : type;
        if (key) {
          defineKeyPropWarningGetter(props, displayName);
        }
        if (ref) {
          defineRefPropWarningGetter(props, displayName);
        }
      }
    }
  }
  return ReactElement(type, key, ref, self, source, ReactCurrentOwner.current, props);
}

/**
 * Return a function that produces ReactElements of a given type.
 * See https://reactjs.org/docs/react-api.html#createfactory
 */


function cloneAndReplaceKey(oldElement, newKey) {
  var newElement = ReactElement(oldElement.type, newKey, oldElement.ref, oldElement._self, oldElement._source, oldElement._owner, oldElement.props);

  return newElement;
}

/**
 * Clone and return a new ReactElement using element as the starting point.
 * See https://reactjs.org/docs/react-api.html#cloneelement
 */
function cloneElement(element, config, children) {
  !!(element === null || element === undefined) ? invariant(false, 'React.cloneElement(...): The argument must be a React element, but you passed %s.', element) : void 0;

  var propName = void 0;

  // Original props are copied
  var props = _assign({}, element.props);

  // Reserved names are extracted
  var key = element.key;
  var ref = element.ref;
  // Self is preserved since the owner is preserved.
  var self = element._self;
  // Source is preserved since cloneElement is unlikely to be targeted by a
  // transpiler, and the original source is probably a better indicator of the
  // true owner.
  var source = element._source;

  // Owner will be preserved, unless ref is overridden
  var owner = element._owner;

  if (config != null) {
    if (hasValidRef(config)) {
      // Silently steal the ref from the parent.
      ref = config.ref;
      owner = ReactCurrentOwner.current;
    }
    if (hasValidKey(config)) {
      key = '' + config.key;
    }

    // Remaining properties override existing props
    var defaultProps = void 0;
    if (element.type && element.type.defaultProps) {
      defaultProps = element.type.defaultProps;
    }
    for (propName in config) {
      if (hasOwnProperty.call(config, propName) && !RESERVED_PROPS.hasOwnProperty(propName)) {
        if (config[propName] === undefined && defaultProps !== undefined) {
          // Resolve default props
          props[propName] = defaultProps[propName];
        } else {
          props[propName] = config[propName];
        }
      }
    }
  }

  // Children can be more than one argument, and those are transferred onto
  // the newly allocated props object.
  var childrenLength = arguments.length - 2;
  if (childrenLength === 1) {
    props.children = children;
  } else if (childrenLength > 1) {
    var childArray = Array(childrenLength);
    for (var i = 0; i < childrenLength; i++) {
      childArray[i] = arguments[i + 2];
    }
    props.children = childArray;
  }

  return ReactElement(element.type, key, ref, self, source, owner, props);
}

/**
 * Verifies the object is a ReactElement.
 * See https://reactjs.org/docs/react-api.html#isvalidelement
 * @param {?object} object
 * @return {boolean} True if `object` is a valid component.
 * @final
 */
function isValidElement(object) {
  return typeof object === 'object' && object !== null && object.$$typeof === REACT_ELEMENT_TYPE;
}

var ReactDebugCurrentFrame = {};

{
  // Component that is being worked on
  ReactDebugCurrentFrame.getCurrentStack = null;

  ReactDebugCurrentFrame.getStackAddendum = function () {
    var impl = ReactDebugCurrentFrame.getCurrentStack;
    if (impl) {
      return impl();
    }
    return null;
  };
}

var SEPARATOR = '.';
var SUBSEPARATOR = ':';

/**
 * Escape and wrap key so it is safe to use as a reactid
 *
 * @param {string} key to be escaped.
 * @return {string} the escaped key.
 */
function escape(key) {
  var escapeRegex = /[=:]/g;
  var escaperLookup = {
    '=': '=0',
    ':': '=2'
  };
  var escapedString = ('' + key).replace(escapeRegex, function (match) {
    return escaperLookup[match];
  });

  return '$' + escapedString;
}

/**
 * TODO: Test that a single child and an array with one item have the same key
 * pattern.
 */

var didWarnAboutMaps = false;

var userProvidedKeyEscapeRegex = /\/+/g;
function escapeUserProvidedKey(text) {
  return ('' + text).replace(userProvidedKeyEscapeRegex, '$&/');
}

var POOL_SIZE = 10;
var traverseContextPool = [];
function getPooledTraverseContext(mapResult, keyPrefix, mapFunction, mapContext) {
  if (traverseContextPool.length) {
    var traverseContext = traverseContextPool.pop();
    traverseContext.result = mapResult;
    traverseContext.keyPrefix = keyPrefix;
    traverseContext.func = mapFunction;
    traverseContext.context = mapContext;
    traverseContext.count = 0;
    return traverseContext;
  } else {
    return {
      result: mapResult,
      keyPrefix: keyPrefix,
      func: mapFunction,
      context: mapContext,
      count: 0
    };
  }
}

function releaseTraverseContext(traverseContext) {
  traverseContext.result = null;
  traverseContext.keyPrefix = null;
  traverseContext.func = null;
  traverseContext.context = null;
  traverseContext.count = 0;
  if (traverseContextPool.length < POOL_SIZE) {
    traverseContextPool.push(traverseContext);
  }
}

/**
 * @param {?*} children Children tree container.
 * @param {!string} nameSoFar Name of the key path so far.
 * @param {!function} callback Callback to invoke with each child found.
 * @param {?*} traverseContext Used to pass information throughout the traversal
 * process.
 * @return {!number} The number of children in this subtree.
 */
function traverseAllChildrenImpl(children, nameSoFar, callback, traverseContext) {
  var type = typeof children;

  if (type === 'undefined' || type === 'boolean') {
    // All of the above are perceived as null.
    children = null;
  }

  var invokeCallback = false;

  if (children === null) {
    invokeCallback = true;
  } else {
    switch (type) {
      case 'string':
      case 'number':
        invokeCallback = true;
        break;
      case 'object':
        switch (children.$$typeof) {
          case REACT_ELEMENT_TYPE:
          case REACT_PORTAL_TYPE:
            invokeCallback = true;
        }
    }
  }

  if (invokeCallback) {
    callback(traverseContext, children,
    // If it's the only child, treat the name as if it was wrapped in an array
    // so that it's consistent if the number of children grows.
    nameSoFar === '' ? SEPARATOR + getComponentKey(children, 0) : nameSoFar);
    return 1;
  }

  var child = void 0;
  var nextName = void 0;
  var subtreeCount = 0; // Count of children found in the current subtree.
  var nextNamePrefix = nameSoFar === '' ? SEPARATOR : nameSoFar + SUBSEPARATOR;

  if (Array.isArray(children)) {
    for (var i = 0; i < children.length; i++) {
      child = children[i];
      nextName = nextNamePrefix + getComponentKey(child, i);
      subtreeCount += traverseAllChildrenImpl(child, nextName, callback, traverseContext);
    }
  } else {
    var iteratorFn = getIteratorFn(children);
    if (typeof iteratorFn === 'function') {
      {
        // Warn about using Maps as children
        if (iteratorFn === children.entries) {
          !didWarnAboutMaps ? warning(false, 'Using Maps as children is unsupported and will likely yield ' + 'unexpected results. Convert it to a sequence/iterable of keyed ' + 'ReactElements instead.%s', ReactDebugCurrentFrame.getStackAddendum()) : void 0;
          didWarnAboutMaps = true;
        }
      }

      var iterator = iteratorFn.call(children);
      var step = void 0;
      var ii = 0;
      while (!(step = iterator.next()).done) {
        child = step.value;
        nextName = nextNamePrefix + getComponentKey(child, ii++);
        subtreeCount += traverseAllChildrenImpl(child, nextName, callback, traverseContext);
      }
    } else if (type === 'object') {
      var addendum = '';
      {
        addendum = ' If you meant to render a collection of children, use an array ' + 'instead.' + ReactDebugCurrentFrame.getStackAddendum();
      }
      var childrenString = '' + children;
      invariant(false, 'Objects are not valid as a React child (found: %s).%s', childrenString === '[object Object]' ? 'object with keys {' + Object.keys(children).join(', ') + '}' : childrenString, addendum);
    }
  }

  return subtreeCount;
}

/**
 * Traverses children that are typically specified as `props.children`, but
 * might also be specified through attributes:
 *
 * - `traverseAllChildren(this.props.children, ...)`
 * - `traverseAllChildren(this.props.leftPanelChildren, ...)`
 *
 * The `traverseContext` is an optional argument that is passed through the
 * entire traversal. It can be used to store accumulations or anything else that
 * the callback might find relevant.
 *
 * @param {?*} children Children tree object.
 * @param {!function} callback To invoke upon traversing each child.
 * @param {?*} traverseContext Context for traversal.
 * @return {!number} The number of children in this subtree.
 */
function traverseAllChildren(children, callback, traverseContext) {
  if (children == null) {
    return 0;
  }

  return traverseAllChildrenImpl(children, '', callback, traverseContext);
}

/**
 * Generate a key string that identifies a component within a set.
 *
 * @param {*} component A component that could contain a manual key.
 * @param {number} index Index that is used if a manual key is not provided.
 * @return {string}
 */
function getComponentKey(component, index) {
  // Do some typechecking here since we call this blindly. We want to ensure
  // that we don't block potential future ES APIs.
  if (typeof component === 'object' && component !== null && component.key != null) {
    // Explicit key
    return escape(component.key);
  }
  // Implicit key determined by the index in the set
  return index.toString(36);
}

function forEachSingleChild(bookKeeping, child, name) {
  var func = bookKeeping.func,
      context = bookKeeping.context;

  func.call(context, child, bookKeeping.count++);
}

/**
 * Iterates through children that are typically specified as `props.children`.
 *
 * See https://reactjs.org/docs/react-api.html#reactchildrenforeach
 *
 * The provided forEachFunc(child, index) will be called for each
 * leaf child.
 *
 * @param {?*} children Children tree container.
 * @param {function(*, int)} forEachFunc
 * @param {*} forEachContext Context for forEachContext.
 */
function forEachChildren(children, forEachFunc, forEachContext) {
  if (children == null) {
    return children;
  }
  var traverseContext = getPooledTraverseContext(null, null, forEachFunc, forEachContext);
  traverseAllChildren(children, forEachSingleChild, traverseContext);
  releaseTraverseContext(traverseContext);
}

function mapSingleChildIntoContext(bookKeeping, child, childKey) {
  var result = bookKeeping.result,
      keyPrefix = bookKeeping.keyPrefix,
      func = bookKeeping.func,
      context = bookKeeping.context;


  var mappedChild = func.call(context, child, bookKeeping.count++);
  if (Array.isArray(mappedChild)) {
    mapIntoWithKeyPrefixInternal(mappedChild, result, childKey, emptyFunction.thatReturnsArgument);
  } else if (mappedChild != null) {
    if (isValidElement(mappedChild)) {
      mappedChild = cloneAndReplaceKey(mappedChild,
      // Keep both the (mapped) and old keys if they differ, just as
      // traverseAllChildren used to do for objects as children
      keyPrefix + (mappedChild.key && (!child || child.key !== mappedChild.key) ? escapeUserProvidedKey(mappedChild.key) + '/' : '') + childKey);
    }
    result.push(mappedChild);
  }
}

function mapIntoWithKeyPrefixInternal(children, array, prefix, func, context) {
  var escapedPrefix = '';
  if (prefix != null) {
    escapedPrefix = escapeUserProvidedKey(prefix) + '/';
  }
  var traverseContext = getPooledTraverseContext(array, escapedPrefix, func, context);
  traverseAllChildren(children, mapSingleChildIntoContext, traverseContext);
  releaseTraverseContext(traverseContext);
}

/**
 * Maps children that are typically specified as `props.children`.
 *
 * See https://reactjs.org/docs/react-api.html#reactchildrenmap
 *
 * The provided mapFunction(child, key, index) will be called for each
 * leaf child.
 *
 * @param {?*} children Children tree container.
 * @param {function(*, int)} func The map function.
 * @param {*} context Context for mapFunction.
 * @return {object} Object containing the ordered map of results.
 */
function mapChildren(children, func, context) {
  if (children == null) {
    return children;
  }
  var result = [];
  mapIntoWithKeyPrefixInternal(children, result, null, func, context);
  return result;
}

/**
 * Count the number of children that are typically specified as
 * `props.children`.
 *
 * See https://reactjs.org/docs/react-api.html#reactchildrencount
 *
 * @param {?*} children Children tree container.
 * @return {number} The number of children.
 */
function countChildren(children) {
  return traverseAllChildren(children, emptyFunction.thatReturnsNull, null);
}

/**
 * Flatten a children object (typically specified as `props.children`) and
 * return an array with appropriately re-keyed children.
 *
 * See https://reactjs.org/docs/react-api.html#reactchildrentoarray
 */
function toArray(children) {
  var result = [];
  mapIntoWithKeyPrefixInternal(children, result, null, emptyFunction.thatReturnsArgument);
  return result;
}

/**
 * Returns the first child in a collection of children and verifies that there
 * is only one child in the collection.
 *
 * See https://reactjs.org/docs/react-api.html#reactchildrenonly
 *
 * The current implementation of this function assumes that a single child gets
 * passed without a wrapper, but the purpose of this helper function is to
 * abstract away the particular structure of children.
 *
 * @param {?object} children Child collection structure.
 * @return {ReactElement} The first and only `ReactElement` contained in the
 * structure.
 */
function onlyChild(children) {
  !isValidElement(children) ? invariant(false, 'React.Children.only expected to receive a single React element child.') : void 0;
  return children;
}

function createContext(defaultValue, calculateChangedBits) {
  if (calculateChangedBits === undefined) {
    calculateChangedBits = null;
  } else {
    {
      !(calculateChangedBits === null || typeof calculateChangedBits === 'function') ? warning(false, 'createContext: Expected the optional second argument to be a ' + 'function. Instead received: %s', calculateChangedBits) : void 0;
    }
  }

  var context = {
    $$typeof: REACT_CONTEXT_TYPE,
    _calculateChangedBits: calculateChangedBits,
    _defaultValue: defaultValue,
    _currentValue: defaultValue,
    // As a workaround to support multiple concurrent renderers, we categorize
    // some renderers as primary and others as secondary. We only expect
    // there to be two concurrent renderers at most: React Native (primary) and
    // Fabric (secondary); React DOM (primary) and React ART (secondary).
    // Secondary renderers store their context values on separate fields.
    _currentValue2: defaultValue,
    _changedBits: 0,
    _changedBits2: 0,
    // These are circular
    Provider: null,
    Consumer: null
  };

  context.Provider = {
    $$typeof: REACT_PROVIDER_TYPE,
    _context: context
  };
  context.Consumer = context;

  {
    context._currentRenderer = null;
    context._currentRenderer2 = null;
  }

  return context;
}

function forwardRef(render) {
  {
    !(typeof render === 'function') ? warning(false, 'forwardRef requires a render function but was given %s.', render === null ? 'null' : typeof render) : void 0;

    if (render != null) {
      !(render.defaultProps == null && render.propTypes == null) ? warning(false, 'forwardRef render functions do not support propTypes or defaultProps. ' + 'Did you accidentally pass a React component?') : void 0;
    }
  }

  return {
    $$typeof: REACT_FORWARD_REF_TYPE,
    render: render
  };
}

var describeComponentFrame = function (name, source, ownerName) {
  return '\n    in ' + (name || 'Unknown') + (source ? ' (at ' + source.fileName.replace(/^.*[\\\/]/, '') + ':' + source.lineNumber + ')' : ownerName ? ' (created by ' + ownerName + ')' : '');
};

function isValidElementType(type) {
  return typeof type === 'string' || typeof type === 'function' ||
  // Note: its typeof might be other than 'symbol' or 'number' if it's a polyfill.
  type === REACT_FRAGMENT_TYPE || type === REACT_ASYNC_MODE_TYPE || type === REACT_PROFILER_TYPE || type === REACT_STRICT_MODE_TYPE || type === REACT_TIMEOUT_TYPE || typeof type === 'object' && type !== null && (type.$$typeof === REACT_PROVIDER_TYPE || type.$$typeof === REACT_CONTEXT_TYPE || type.$$typeof === REACT_FORWARD_REF_TYPE);
}

function getComponentName(fiber) {
  var type = fiber.type;

  if (typeof type === 'function') {
    return type.displayName || type.name;
  }
  if (typeof type === 'string') {
    return type;
  }
  switch (type) {
    case REACT_ASYNC_MODE_TYPE:
      return 'AsyncMode';
    case REACT_CONTEXT_TYPE:
      return 'Context.Consumer';
    case REACT_FRAGMENT_TYPE:
      return 'ReactFragment';
    case REACT_PORTAL_TYPE:
      return 'ReactPortal';
    case REACT_PROFILER_TYPE:
      return 'Profiler(' + fiber.pendingProps.id + ')';
    case REACT_PROVIDER_TYPE:
      return 'Context.Provider';
    case REACT_STRICT_MODE_TYPE:
      return 'StrictMode';
    case REACT_TIMEOUT_TYPE:
      return 'Timeout';
  }
  if (typeof type === 'object' && type !== null) {
    switch (type.$$typeof) {
      case REACT_FORWARD_REF_TYPE:
        var functionName = type.render.displayName || type.render.name || '';
        return functionName !== '' ? 'ForwardRef(' + functionName + ')' : 'ForwardRef';
    }
  }
  return null;
}

/**
 * ReactElementValidator provides a wrapper around a element factory
 * which validates the props passed to the element. This is intended to be
 * used only in DEV and could be replaced by a static type checker for languages
 * that support it.
 */

var currentlyValidatingElement = void 0;
var propTypesMisspellWarningShown = void 0;

var getDisplayName = function () {};
var getStackAddendum = function () {};

{
  currentlyValidatingElement = null;

  propTypesMisspellWarningShown = false;

  getDisplayName = function (element) {
    if (element == null) {
      return '#empty';
    } else if (typeof element === 'string' || typeof element === 'number') {
      return '#text';
    } else if (typeof element.type === 'string') {
      return element.type;
    }

    var type = element.type;
    if (type === REACT_FRAGMENT_TYPE) {
      return 'React.Fragment';
    } else if (typeof type === 'object' && type !== null && type.$$typeof === REACT_FORWARD_REF_TYPE) {
      var functionName = type.render.displayName || type.render.name || '';
      return functionName !== '' ? 'ForwardRef(' + functionName + ')' : 'ForwardRef';
    } else {
      return type.displayName || type.name || 'Unknown';
    }
  };

  getStackAddendum = function () {
    var stack = '';
    if (currentlyValidatingElement) {
      var name = getDisplayName(currentlyValidatingElement);
      var owner = currentlyValidatingElement._owner;
      stack += describeComponentFrame(name, currentlyValidatingElement._source, owner && getComponentName(owner));
    }
    stack += ReactDebugCurrentFrame.getStackAddendum() || '';
    return stack;
  };
}

function getDeclarationErrorAddendum() {
  if (ReactCurrentOwner.current) {
    var name = getComponentName(ReactCurrentOwner.current);
    if (name) {
      return '\n\nCheck the render method of `' + name + '`.';
    }
  }
  return '';
}

function getSourceInfoErrorAddendum(elementProps) {
  if (elementProps !== null && elementProps !== undefined && elementProps.__source !== undefined) {
    var source = elementProps.__source;
    var fileName = source.fileName.replace(/^.*[\\\/]/, '');
    var lineNumber = source.lineNumber;
    return '\n\nCheck your code at ' + fileName + ':' + lineNumber + '.';
  }
  return '';
}

/**
 * Warn if there's no key explicitly set on dynamic arrays of children or
 * object keys are not valid. This allows us to keep track of children between
 * updates.
 */
var ownerHasKeyUseWarning = {};

function getCurrentComponentErrorInfo(parentType) {
  var info = getDeclarationErrorAddendum();

  if (!info) {
    var parentName = typeof parentType === 'string' ? parentType : parentType.displayName || parentType.name;
    if (parentName) {
      info = '\n\nCheck the top-level render call using <' + parentName + '>.';
    }
  }
  return info;
}

/**
 * Warn if the element doesn't have an explicit key assigned to it.
 * This element is in an array. The array could grow and shrink or be
 * reordered. All children that haven't already been validated are required to
 * have a "key" property assigned to it. Error statuses are cached so a warning
 * will only be shown once.
 *
 * @internal
 * @param {ReactElement} element Element that requires a key.
 * @param {*} parentType element's parent's type.
 */
function validateExplicitKey(element, parentType) {
  if (!element._store || element._store.validated || element.key != null) {
    return;
  }
  element._store.validated = true;

  var currentComponentErrorInfo = getCurrentComponentErrorInfo(parentType);
  if (ownerHasKeyUseWarning[currentComponentErrorInfo]) {
    return;
  }
  ownerHasKeyUseWarning[currentComponentErrorInfo] = true;

  // Usually the current owner is the offender, but if it accepts children as a
  // property, it may be the creator of the child that's responsible for
  // assigning it a key.
  var childOwner = '';
  if (element && element._owner && element._owner !== ReactCurrentOwner.current) {
    // Give the component that originally created this child.
    childOwner = ' It was passed a child from ' + getComponentName(element._owner) + '.';
  }

  currentlyValidatingElement = element;
  {
    warning(false, 'Each child in an array or iterator should have a unique "key" prop.' + '%s%s See https://fb.me/react-warning-keys for more information.%s', currentComponentErrorInfo, childOwner, getStackAddendum());
  }
  currentlyValidatingElement = null;
}

/**
 * Ensure that every element either is passed in a static location, in an
 * array with an explicit keys property defined, or in an object literal
 * with valid key property.
 *
 * @internal
 * @param {ReactNode} node Statically passed child of any type.
 * @param {*} parentType node's parent's type.
 */
function validateChildKeys(node, parentType) {
  if (typeof node !== 'object') {
    return;
  }
  if (Array.isArray(node)) {
    for (var i = 0; i < node.length; i++) {
      var child = node[i];
      if (isValidElement(child)) {
        validateExplicitKey(child, parentType);
      }
    }
  } else if (isValidElement(node)) {
    // This element was passed in a valid location.
    if (node._store) {
      node._store.validated = true;
    }
  } else if (node) {
    var iteratorFn = getIteratorFn(node);
    if (typeof iteratorFn === 'function') {
      // Entry iterators used to provide implicit keys,
      // but now we print a separate warning for them later.
      if (iteratorFn !== node.entries) {
        var iterator = iteratorFn.call(node);
        var step = void 0;
        while (!(step = iterator.next()).done) {
          if (isValidElement(step.value)) {
            validateExplicitKey(step.value, parentType);
          }
        }
      }
    }
  }
}

/**
 * Given an element, validate that its props follow the propTypes definition,
 * provided by the type.
 *
 * @param {ReactElement} element
 */
function validatePropTypes(element) {
  var type = element.type;
  var name = void 0,
      propTypes = void 0;
  if (typeof type === 'function') {
    // Class or functional component
    name = type.displayName || type.name;
    propTypes = type.propTypes;
  } else if (typeof type === 'object' && type !== null && type.$$typeof === REACT_FORWARD_REF_TYPE) {
    // ForwardRef
    var functionName = type.render.displayName || type.render.name || '';
    name = functionName !== '' ? 'ForwardRef(' + functionName + ')' : 'ForwardRef';
    propTypes = type.propTypes;
  } else {
    return;
  }
  if (propTypes) {
    currentlyValidatingElement = element;
    checkPropTypes(propTypes, element.props, 'prop', name, getStackAddendum);
    currentlyValidatingElement = null;
  } else if (type.PropTypes !== undefined && !propTypesMisspellWarningShown) {
    propTypesMisspellWarningShown = true;
    warning(false, 'Component %s declared `PropTypes` instead of `propTypes`. Did you misspell the property assignment?', name || 'Unknown');
  }
  if (typeof type.getDefaultProps === 'function') {
    !type.getDefaultProps.isReactClassApproved ? warning(false, 'getDefaultProps is only used on classic React.createClass ' + 'definitions. Use a static property named `defaultProps` instead.') : void 0;
  }
}

/**
 * Given a fragment, validate that it can only be provided with fragment props
 * @param {ReactElement} fragment
 */
function validateFragmentProps(fragment) {
  currentlyValidatingElement = fragment;

  var keys = Object.keys(fragment.props);
  for (var i = 0; i < keys.length; i++) {
    var key = keys[i];
    if (key !== 'children' && key !== 'key') {
      warning(false, 'Invalid prop `%s` supplied to `React.Fragment`. ' + 'React.Fragment can only have `key` and `children` props.%s', key, getStackAddendum());
      break;
    }
  }

  if (fragment.ref !== null) {
    warning(false, 'Invalid attribute `ref` supplied to `React.Fragment`.%s', getStackAddendum());
  }

  currentlyValidatingElement = null;
}

function createElementWithValidation(type, props, children) {
  var validType = isValidElementType(type);

  // We warn in this case but don't throw. We expect the element creation to
  // succeed and there will likely be errors in render.
  if (!validType) {
    var info = '';
    if (type === undefined || typeof type === 'object' && type !== null && Object.keys(type).length === 0) {
      info += ' You likely forgot to export your component from the file ' + "it's defined in, or you might have mixed up default and named imports.";
    }

    var sourceInfo = getSourceInfoErrorAddendum(props);
    if (sourceInfo) {
      info += sourceInfo;
    } else {
      info += getDeclarationErrorAddendum();
    }

    info += getStackAddendum() || '';

    var typeString = void 0;
    if (type === null) {
      typeString = 'null';
    } else if (Array.isArray(type)) {
      typeString = 'array';
    } else {
      typeString = typeof type;
    }

    warning(false, 'React.createElement: type is invalid -- expected a string (for ' + 'built-in components) or a class/function (for composite ' + 'components) but got: %s.%s', typeString, info);
  }

  var element = createElement.apply(this, arguments);

  // The result can be nullish if a mock or a custom function is used.
  // TODO: Drop this when these are no longer allowed as the type argument.
  if (element == null) {
    return element;
  }

  // Skip key warning if the type isn't valid since our key validation logic
  // doesn't expect a non-string/function type and can throw confusing errors.
  // We don't want exception behavior to differ between dev and prod.
  // (Rendering will throw with a helpful message and as soon as the type is
  // fixed, the key warnings will appear.)
  if (validType) {
    for (var i = 2; i < arguments.length; i++) {
      validateChildKeys(arguments[i], type);
    }
  }

  if (type === REACT_FRAGMENT_TYPE) {
    validateFragmentProps(element);
  } else {
    validatePropTypes(element);
  }

  return element;
}

function createFactoryWithValidation(type) {
  var validatedFactory = createElementWithValidation.bind(null, type);
  validatedFactory.type = type;
  // Legacy hook: remove it
  {
    Object.defineProperty(validatedFactory, 'type', {
      enumerable: false,
      get: function () {
        lowPriorityWarning$1(false, 'Factory.type is deprecated. Access the class directly ' + 'before passing it to createFactory.');
        Object.defineProperty(this, 'type', {
          value: type
        });
        return type;
      }
    });
  }

  return validatedFactory;
}

function cloneElementWithValidation(element, props, children) {
  var newElement = cloneElement.apply(this, arguments);
  for (var i = 2; i < arguments.length; i++) {
    validateChildKeys(arguments[i], newElement.type);
  }
  validatePropTypes(newElement);
  return newElement;
}

var React = {
  Children: {
    map: mapChildren,
    forEach: forEachChildren,
    count: countChildren,
    toArray: toArray,
    only: onlyChild
  },

  createRef: createRef,
  Component: Component,
  PureComponent: PureComponent,

  createContext: createContext,
  forwardRef: forwardRef,

  Fragment: REACT_FRAGMENT_TYPE,
  StrictMode: REACT_STRICT_MODE_TYPE,
  unstable_AsyncMode: REACT_ASYNC_MODE_TYPE,
  unstable_Profiler: REACT_PROFILER_TYPE,

  createElement: createElementWithValidation,
  cloneElement: cloneElementWithValidation,
  createFactory: createFactoryWithValidation,
  isValidElement: isValidElement,

  version: ReactVersion,

  __SECRET_INTERNALS_DO_NOT_USE_OR_YOU_WILL_BE_FIRED: {
    ReactCurrentOwner: ReactCurrentOwner,
    // Used by renderers to avoid bundling object-assign twice in UMD bundles:
    assign: _assign
  }
};

if (enableSuspense) {
  React.Timeout = REACT_TIMEOUT_TYPE;
}

{
  _assign(React.__SECRET_INTERNALS_DO_NOT_USE_OR_YOU_WILL_BE_FIRED, {
    // These should not be included in production.
    ReactDebugCurrentFrame: ReactDebugCurrentFrame,
    // Shim for React DOM 16.0.0 which still destructured (but not used) this.
    // TODO: remove in React 17.0.
    ReactComponentTreeHook: {}
  });
}



var React$2 = Object.freeze({
	default: React
});

var React$3 = ( React$2 && React ) || React$2;

// TODO: decide on the top-level export form.
// This is hacky but makes it work with both Rollup and Jest.
var react = React$3.default ? React$3.default : React$3;

module.exports = react;
  })();
}


/***/ }),

/***/ "./Areas/Portal/www/node_modules/@9softstudio/react-fixed-table-header/node_modules/react/index.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/@9softstudio/react-fixed-table-header/node_modules/react/index.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


if (false) {} else {
  module.exports = __webpack_require__(/*! ./cjs/react.development.js */ "./Areas/Portal/www/node_modules/@9softstudio/react-fixed-table-header/node_modules/react/cjs/react.development.js");
}


/***/ }),

/***/ "./Areas/Portal/www/node_modules/axios/index.js":
/*!******************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/index.js ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! ./lib/axios */ "./Areas/Portal/www/node_modules/axios/lib/axios.js");

/***/ }),

/***/ "./Areas/Portal/www/node_modules/axios/lib/adapters/xhr.js":
/*!*****************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/adapters/xhr.js ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");
var settle = __webpack_require__(/*! ./../core/settle */ "./Areas/Portal/www/node_modules/axios/lib/core/settle.js");
var buildURL = __webpack_require__(/*! ./../helpers/buildURL */ "./Areas/Portal/www/node_modules/axios/lib/helpers/buildURL.js");
var parseHeaders = __webpack_require__(/*! ./../helpers/parseHeaders */ "./Areas/Portal/www/node_modules/axios/lib/helpers/parseHeaders.js");
var isURLSameOrigin = __webpack_require__(/*! ./../helpers/isURLSameOrigin */ "./Areas/Portal/www/node_modules/axios/lib/helpers/isURLSameOrigin.js");
var createError = __webpack_require__(/*! ../core/createError */ "./Areas/Portal/www/node_modules/axios/lib/core/createError.js");
var btoa = (typeof window !== 'undefined' && window.btoa && window.btoa.bind(window)) || __webpack_require__(/*! ./../helpers/btoa */ "./Areas/Portal/www/node_modules/axios/lib/helpers/btoa.js");

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
      var cookies = __webpack_require__(/*! ./../helpers/cookies */ "./Areas/Portal/www/node_modules/axios/lib/helpers/cookies.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/axios.js":
/*!**********************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/axios.js ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");
var bind = __webpack_require__(/*! ./helpers/bind */ "./Areas/Portal/www/node_modules/axios/lib/helpers/bind.js");
var Axios = __webpack_require__(/*! ./core/Axios */ "./Areas/Portal/www/node_modules/axios/lib/core/Axios.js");
var defaults = __webpack_require__(/*! ./defaults */ "./Areas/Portal/www/node_modules/axios/lib/defaults.js");

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
axios.Cancel = __webpack_require__(/*! ./cancel/Cancel */ "./Areas/Portal/www/node_modules/axios/lib/cancel/Cancel.js");
axios.CancelToken = __webpack_require__(/*! ./cancel/CancelToken */ "./Areas/Portal/www/node_modules/axios/lib/cancel/CancelToken.js");
axios.isCancel = __webpack_require__(/*! ./cancel/isCancel */ "./Areas/Portal/www/node_modules/axios/lib/cancel/isCancel.js");

// Expose all/spread
axios.all = function all(promises) {
  return Promise.all(promises);
};
axios.spread = __webpack_require__(/*! ./helpers/spread */ "./Areas/Portal/www/node_modules/axios/lib/helpers/spread.js");

module.exports = axios;

// Allow use of default import syntax in TypeScript
module.exports.default = axios;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/axios/lib/cancel/Cancel.js":
/*!******************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/cancel/Cancel.js ***!
  \******************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/axios/lib/cancel/CancelToken.js":
/*!***********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/cancel/CancelToken.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var Cancel = __webpack_require__(/*! ./Cancel */ "./Areas/Portal/www/node_modules/axios/lib/cancel/Cancel.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/cancel/isCancel.js":
/*!********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/cancel/isCancel.js ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


module.exports = function isCancel(value) {
  return !!(value && value.__CANCEL__);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/axios/lib/core/Axios.js":
/*!***************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/core/Axios.js ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var defaults = __webpack_require__(/*! ./../defaults */ "./Areas/Portal/www/node_modules/axios/lib/defaults.js");
var utils = __webpack_require__(/*! ./../utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");
var InterceptorManager = __webpack_require__(/*! ./InterceptorManager */ "./Areas/Portal/www/node_modules/axios/lib/core/InterceptorManager.js");
var dispatchRequest = __webpack_require__(/*! ./dispatchRequest */ "./Areas/Portal/www/node_modules/axios/lib/core/dispatchRequest.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/core/InterceptorManager.js":
/*!****************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/core/InterceptorManager.js ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/core/createError.js":
/*!*********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/core/createError.js ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var enhanceError = __webpack_require__(/*! ./enhanceError */ "./Areas/Portal/www/node_modules/axios/lib/core/enhanceError.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/core/dispatchRequest.js":
/*!*************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/core/dispatchRequest.js ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");
var transformData = __webpack_require__(/*! ./transformData */ "./Areas/Portal/www/node_modules/axios/lib/core/transformData.js");
var isCancel = __webpack_require__(/*! ../cancel/isCancel */ "./Areas/Portal/www/node_modules/axios/lib/cancel/isCancel.js");
var defaults = __webpack_require__(/*! ../defaults */ "./Areas/Portal/www/node_modules/axios/lib/defaults.js");
var isAbsoluteURL = __webpack_require__(/*! ./../helpers/isAbsoluteURL */ "./Areas/Portal/www/node_modules/axios/lib/helpers/isAbsoluteURL.js");
var combineURLs = __webpack_require__(/*! ./../helpers/combineURLs */ "./Areas/Portal/www/node_modules/axios/lib/helpers/combineURLs.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/core/enhanceError.js":
/*!**********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/core/enhanceError.js ***!
  \**********************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/axios/lib/core/settle.js":
/*!****************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/core/settle.js ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var createError = __webpack_require__(/*! ./createError */ "./Areas/Portal/www/node_modules/axios/lib/core/createError.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/core/transformData.js":
/*!***********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/core/transformData.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/defaults.js":
/*!*************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/defaults.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(process) {

var utils = __webpack_require__(/*! ./utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");
var normalizeHeaderName = __webpack_require__(/*! ./helpers/normalizeHeaderName */ "./Areas/Portal/www/node_modules/axios/lib/helpers/normalizeHeaderName.js");

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
    adapter = __webpack_require__(/*! ./adapters/xhr */ "./Areas/Portal/www/node_modules/axios/lib/adapters/xhr.js");
  } else if (typeof process !== 'undefined') {
    // For node use HTTP adapter
    adapter = __webpack_require__(/*! ./adapters/http */ "./Areas/Portal/www/node_modules/axios/lib/adapters/xhr.js");
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

/* WEBPACK VAR INJECTION */}.call(this, __webpack_require__(/*! ./../../../../../../node_modules/process/browser.js */ "./node_modules/process/browser.js")))

/***/ }),

/***/ "./Areas/Portal/www/node_modules/axios/lib/helpers/bind.js":
/*!*****************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/helpers/bind.js ***!
  \*****************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/axios/lib/helpers/btoa.js":
/*!*****************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/helpers/btoa.js ***!
  \*****************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/axios/lib/helpers/buildURL.js":
/*!*********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/helpers/buildURL.js ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/helpers/combineURLs.js":
/*!************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/helpers/combineURLs.js ***!
  \************************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/axios/lib/helpers/cookies.js":
/*!********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/helpers/cookies.js ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/helpers/isAbsoluteURL.js":
/*!**************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/helpers/isAbsoluteURL.js ***!
  \**************************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/axios/lib/helpers/isURLSameOrigin.js":
/*!****************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/helpers/isURLSameOrigin.js ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/helpers/normalizeHeaderName.js":
/*!********************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/helpers/normalizeHeaderName.js ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ../utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");

module.exports = function normalizeHeaderName(headers, normalizedName) {
  utils.forEach(headers, function processHeader(value, name) {
    if (name !== normalizedName && name.toUpperCase() === normalizedName.toUpperCase()) {
      headers[normalizedName] = value;
      delete headers[name];
    }
  });
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/axios/lib/helpers/parseHeaders.js":
/*!*************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/helpers/parseHeaders.js ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./Areas/Portal/www/node_modules/axios/lib/utils.js");

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

/***/ "./Areas/Portal/www/node_modules/axios/lib/helpers/spread.js":
/*!*******************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/helpers/spread.js ***!
  \*******************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/axios/lib/utils.js":
/*!**********************************************************!*\
  !*** ./Areas/Portal/www/node_modules/axios/lib/utils.js ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var bind = __webpack_require__(/*! ./helpers/bind */ "./Areas/Portal/www/node_modules/axios/lib/helpers/bind.js");
var isBuffer = __webpack_require__(/*! is-buffer */ "./Areas/Portal/www/node_modules/is-buffer/index.js");

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/array/from.js":
/*!***************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/array/from.js ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/array/from */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/array/from.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/json/stringify.js":
/*!*******************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/json/stringify.js ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/json/stringify */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/json/stringify.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/assign.js":
/*!******************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/object/assign.js ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/assign */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/assign.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/create.js":
/*!******************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/object/create.js ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/create */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/create.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/define-property.js":
/*!***************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/object/define-property.js ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/define-property */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/define-property.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/get-prototype-of.js":
/*!****************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/object/get-prototype-of.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/get-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/get-prototype-of.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/keys.js":
/*!****************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/object/keys.js ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/keys.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/set-prototype-of.js":
/*!****************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/object/set-prototype-of.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/object/set-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/set-prototype-of.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/promise.js":
/*!************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/promise.js ***!
  \************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/promise */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/promise.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/symbol.js":
/*!***********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/symbol.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/symbol */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/index.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/core-js/symbol/iterator.js":
/*!********************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/core-js/symbol/iterator.js ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(/*! core-js/library/fn/symbol/iterator */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/iterator.js"), __esModule: true };

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/helpers/asyncToGenerator.js":
/*!*********************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/helpers/asyncToGenerator.js ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _promise = __webpack_require__(/*! ../core-js/promise */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/promise.js");

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/helpers/classCallCheck.js":
/*!*******************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/helpers/classCallCheck.js ***!
  \*******************************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/helpers/createClass.js":
/*!****************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/helpers/createClass.js ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _defineProperty = __webpack_require__(/*! ../core-js/object/define-property */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/define-property.js");

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/helpers/defineProperty.js":
/*!*******************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/helpers/defineProperty.js ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _defineProperty = __webpack_require__(/*! ../core-js/object/define-property */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/define-property.js");

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/helpers/extends.js":
/*!************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/helpers/extends.js ***!
  \************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _assign = __webpack_require__(/*! ../core-js/object/assign */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/assign.js");

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/helpers/inherits.js":
/*!*************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/helpers/inherits.js ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _setPrototypeOf = __webpack_require__(/*! ../core-js/object/set-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/set-prototype-of.js");

var _setPrototypeOf2 = _interopRequireDefault(_setPrototypeOf);

var _create = __webpack_require__(/*! ../core-js/object/create */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/create.js");

var _create2 = _interopRequireDefault(_create);

var _typeof2 = __webpack_require__(/*! ../helpers/typeof */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/typeof.js");

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/helpers/possibleConstructorReturn.js":
/*!******************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/helpers/possibleConstructorReturn.js ***!
  \******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _typeof2 = __webpack_require__(/*! ../helpers/typeof */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/typeof.js");

var _typeof3 = _interopRequireDefault(_typeof2);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = function (self, call) {
  if (!self) {
    throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
  }

  return call && ((typeof call === "undefined" ? "undefined" : (0, _typeof3.default)(call)) === "object" || typeof call === "function") ? call : self;
};

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/helpers/toConsumableArray.js":
/*!**********************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/helpers/toConsumableArray.js ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _from = __webpack_require__(/*! ../core-js/array/from */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/array/from.js");

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/helpers/typeof.js":
/*!***********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/helpers/typeof.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _iterator = __webpack_require__(/*! ../core-js/symbol/iterator */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/symbol/iterator.js");

var _iterator2 = _interopRequireDefault(_iterator);

var _symbol = __webpack_require__(/*! ../core-js/symbol */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/symbol.js");

var _symbol2 = _interopRequireDefault(_symbol);

var _typeof = typeof _symbol2.default === "function" && typeof _iterator2.default === "symbol" ? function (obj) { return typeof obj; } : function (obj) { return obj && typeof _symbol2.default === "function" && obj.constructor === _symbol2.default && obj !== _symbol2.default.prototype ? "symbol" : typeof obj; };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = typeof _symbol2.default === "function" && _typeof(_iterator2.default) === "symbol" ? function (obj) {
  return typeof obj === "undefined" ? "undefined" : _typeof(obj);
} : function (obj) {
  return obj && typeof _symbol2.default === "function" && obj.constructor === _symbol2.default && obj !== _symbol2.default.prototype ? "symbol" : typeof obj === "undefined" ? "undefined" : _typeof(obj);
};

/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/array/from.js":
/*!***************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/array/from.js ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.string.iterator */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.string.iterator.js");
__webpack_require__(/*! ../../modules/es6.array.from */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.from.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Array.from;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/json/stringify.js":
/*!*******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/json/stringify.js ***!
  \*******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var core = __webpack_require__(/*! ../../modules/_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var $JSON = core.JSON || (core.JSON = { stringify: JSON.stringify });
module.exports = function stringify(it) { // eslint-disable-line no-unused-vars
  return $JSON.stringify.apply($JSON, arguments);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/assign.js":
/*!******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/assign.js ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.assign */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.assign.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object.assign;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/create.js":
/*!******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/create.js ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.create */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.create.js");
var $Object = __webpack_require__(/*! ../../modules/_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object;
module.exports = function create(P, D) {
  return $Object.create(P, D);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/define-property.js":
/*!***************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/define-property.js ***!
  \***************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.define-property */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.define-property.js");
var $Object = __webpack_require__(/*! ../../modules/_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object;
module.exports = function defineProperty(it, key, desc) {
  return $Object.defineProperty(it, key, desc);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/get-prototype-of.js":
/*!****************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/get-prototype-of.js ***!
  \****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.get-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.get-prototype-of.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object.getPrototypeOf;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/keys.js":
/*!****************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/keys.js ***!
  \****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.keys.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object.keys;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/set-prototype-of.js":
/*!****************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/object/set-prototype-of.js ***!
  \****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.object.set-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.set-prototype-of.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Object.setPrototypeOf;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/promise.js":
/*!************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/promise.js ***!
  \************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../modules/es6.object.to-string */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.to-string.js");
__webpack_require__(/*! ../modules/es6.string.iterator */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.string.iterator.js");
__webpack_require__(/*! ../modules/web.dom.iterable */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/web.dom.iterable.js");
__webpack_require__(/*! ../modules/es6.promise */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.promise.js");
__webpack_require__(/*! ../modules/es7.promise.finally */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.finally.js");
__webpack_require__(/*! ../modules/es7.promise.try */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.try.js");
module.exports = __webpack_require__(/*! ../modules/_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Promise;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/index.js":
/*!*****************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/index.js ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.symbol */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.symbol.js");
__webpack_require__(/*! ../../modules/es6.object.to-string */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.to-string.js");
__webpack_require__(/*! ../../modules/es7.symbol.async-iterator */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.async-iterator.js");
__webpack_require__(/*! ../../modules/es7.symbol.observable */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.observable.js");
module.exports = __webpack_require__(/*! ../../modules/_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").Symbol;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/iterator.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/fn/symbol/iterator.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es6.string.iterator */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.string.iterator.js");
__webpack_require__(/*! ../../modules/web.dom.iterable */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/web.dom.iterable.js");
module.exports = __webpack_require__(/*! ../../modules/_wks-ext */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-ext.js").f('iterator');


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (it) {
  if (typeof it != 'function') throw TypeError(it + ' is not a function!');
  return it;
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_add-to-unscopables.js":
/*!*****************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_add-to-unscopables.js ***!
  \*****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function () { /* empty */ };


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-instance.js":
/*!**********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-instance.js ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (it, Constructor, name, forbiddenField) {
  if (!(it instanceof Constructor) || (forbiddenField !== undefined && forbiddenField in it)) {
    throw TypeError(name + ': incorrect invocation!');
  } return it;
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var isObject = __webpack_require__(/*! ./_is-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
module.exports = function (it) {
  if (!isObject(it)) throw TypeError(it + ' is not an object!');
  return it;
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_array-includes.js":
/*!*************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_array-includes.js ***!
  \*************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// false -> Array#indexOf
// true  -> Array#includes
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");
var toLength = __webpack_require__(/*! ./_to-length */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-length.js");
var toAbsoluteIndex = __webpack_require__(/*! ./_to-absolute-index */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-absolute-index.js");
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_classof.js":
/*!******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_classof.js ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// getting tag from 19.1.3.6 Object.prototype.toString()
var cof = __webpack_require__(/*! ./_cof */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js");
var TAG = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('toStringTag');
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js":
/*!**************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

var toString = {}.toString;

module.exports = function (it) {
  return toString.call(it).slice(8, -1);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js":
/*!***************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

var core = module.exports = { version: '2.5.7' };
if (typeof __e == 'number') __e = core; // eslint-disable-line no-undef


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_create-property.js":
/*!**************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_create-property.js ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $defineProperty = __webpack_require__(/*! ./_object-dp */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js");
var createDesc = __webpack_require__(/*! ./_property-desc */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js");

module.exports = function (object, index, value) {
  if (index in object) $defineProperty.f(object, index, createDesc(0, value));
  else object[index] = value;
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js":
/*!**************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// optional / simple context binding
var aFunction = __webpack_require__(/*! ./_a-function */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js");
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_defined.js":
/*!******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_defined.js ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// 7.2.1 RequireObjectCoercible(argument)
module.exports = function (it) {
  if (it == undefined) throw TypeError("Can't call method on  " + it);
  return it;
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js":
/*!**********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// Thank's IE8 for his funny defineProperty
module.exports = !__webpack_require__(/*! ./_fails */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js")(function () {
  return Object.defineProperty({}, 'a', { get: function () { return 7; } }).a != 7;
});


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_dom-create.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_dom-create.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var isObject = __webpack_require__(/*! ./_is-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var document = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js").document;
// typeof document.createElement is 'object' in old IE
var is = isObject(document) && isObject(document.createElement);
module.exports = function (it) {
  return is ? document.createElement(it) : {};
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-bug-keys.js":
/*!************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-bug-keys.js ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// IE 8- don't enum bug keys
module.exports = (
  'constructor,hasOwnProperty,isPrototypeOf,propertyIsEnumerable,toLocaleString,toString,valueOf'
).split(',');


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-keys.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-keys.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// all enumerable object keys, includes symbols
var getKeys = __webpack_require__(/*! ./_object-keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js");
var gOPS = __webpack_require__(/*! ./_object-gops */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gops.js");
var pIE = __webpack_require__(/*! ./_object-pie */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js");
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js":
/*!*****************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var core = __webpack_require__(/*! ./_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var ctx = __webpack_require__(/*! ./_ctx */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js");
var hide = __webpack_require__(/*! ./_hide */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js");
var has = __webpack_require__(/*! ./_has */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js":
/*!****************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js ***!
  \****************************************************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_for-of.js":
/*!*****************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_for-of.js ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var ctx = __webpack_require__(/*! ./_ctx */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js");
var call = __webpack_require__(/*! ./_iter-call */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-call.js");
var isArrayIter = __webpack_require__(/*! ./_is-array-iter */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array-iter.js");
var anObject = __webpack_require__(/*! ./_an-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var toLength = __webpack_require__(/*! ./_to-length */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-length.js");
var getIterFn = __webpack_require__(/*! ./core.get-iterator-method */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/core.get-iterator-method.js");
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js":
/*!*****************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// https://github.com/zloirock/core-js/issues/86#issuecomment-115759028
var global = module.exports = typeof window != 'undefined' && window.Math == Math
  ? window : typeof self != 'undefined' && self.Math == Math ? self
  // eslint-disable-next-line no-new-func
  : Function('return this')();
if (typeof __g == 'number') __g = global; // eslint-disable-line no-undef


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js":
/*!**************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

var hasOwnProperty = {}.hasOwnProperty;
module.exports = function (it, key) {
  return hasOwnProperty.call(it, key);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js":
/*!***************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var dP = __webpack_require__(/*! ./_object-dp */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js");
var createDesc = __webpack_require__(/*! ./_property-desc */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js");
module.exports = __webpack_require__(/*! ./_descriptors */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js") ? function (object, key, value) {
  return dP.f(object, key, createDesc(1, value));
} : function (object, key, value) {
  object[key] = value;
  return object;
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_html.js":
/*!***************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_html.js ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var document = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js").document;
module.exports = document && document.documentElement;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ie8-dom-define.js":
/*!*************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ie8-dom-define.js ***!
  \*************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = !__webpack_require__(/*! ./_descriptors */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js") && !__webpack_require__(/*! ./_fails */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js")(function () {
  return Object.defineProperty(__webpack_require__(/*! ./_dom-create */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_dom-create.js")('div'), 'a', { get: function () { return 7; } }).a != 7;
});


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_invoke.js":
/*!*****************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_invoke.js ***!
  \*****************************************************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iobject.js":
/*!******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iobject.js ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// fallback for non-array-like ES3 and non-enumerable old V8 strings
var cof = __webpack_require__(/*! ./_cof */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js");
// eslint-disable-next-line no-prototype-builtins
module.exports = Object('z').propertyIsEnumerable(0) ? Object : function (it) {
  return cof(it) == 'String' ? it.split('') : Object(it);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array-iter.js":
/*!************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array-iter.js ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// check on default Array iterator
var Iterators = __webpack_require__(/*! ./_iterators */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js");
var ITERATOR = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('iterator');
var ArrayProto = Array.prototype;

module.exports = function (it) {
  return it !== undefined && (Iterators.Array === it || ArrayProto[ITERATOR] === it);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array.js":
/*!*******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array.js ***!
  \*******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 7.2.2 IsArray(argument)
var cof = __webpack_require__(/*! ./_cof */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js");
module.exports = Array.isArray || function isArray(arg) {
  return cof(arg) == 'Array';
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (it) {
  return typeof it === 'object' ? it !== null : typeof it === 'function';
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-call.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-call.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// call something on iterator step with safe closing on error
var anObject = __webpack_require__(/*! ./_an-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-create.js":
/*!**********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-create.js ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var create = __webpack_require__(/*! ./_object-create */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-create.js");
var descriptor = __webpack_require__(/*! ./_property-desc */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js");
var setToStringTag = __webpack_require__(/*! ./_set-to-string-tag */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js");
var IteratorPrototype = {};

// 25.1.2.1.1 %IteratorPrototype%[@@iterator]()
__webpack_require__(/*! ./_hide */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js")(IteratorPrototype, __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('iterator'), function () { return this; });

module.exports = function (Constructor, NAME, next) {
  Constructor.prototype = create(IteratorPrototype, { next: descriptor(1, next) });
  setToStringTag(Constructor, NAME + ' Iterator');
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-define.js":
/*!**********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-define.js ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var LIBRARY = __webpack_require__(/*! ./_library */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js");
var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var redefine = __webpack_require__(/*! ./_redefine */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine.js");
var hide = __webpack_require__(/*! ./_hide */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js");
var Iterators = __webpack_require__(/*! ./_iterators */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js");
var $iterCreate = __webpack_require__(/*! ./_iter-create */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-create.js");
var setToStringTag = __webpack_require__(/*! ./_set-to-string-tag */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js");
var getPrototypeOf = __webpack_require__(/*! ./_object-gpo */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gpo.js");
var ITERATOR = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('iterator');
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-detect.js":
/*!**********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-detect.js ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var ITERATOR = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('iterator');
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-step.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-step.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (done, value) {
  return { value: value, done: !!done };
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = {};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js":
/*!******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = true;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_meta.js":
/*!***************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_meta.js ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var META = __webpack_require__(/*! ./_uid */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js")('meta');
var isObject = __webpack_require__(/*! ./_is-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var has = __webpack_require__(/*! ./_has */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var setDesc = __webpack_require__(/*! ./_object-dp */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js").f;
var id = 0;
var isExtensible = Object.isExtensible || function () {
  return true;
};
var FREEZE = !__webpack_require__(/*! ./_fails */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js")(function () {
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_microtask.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_microtask.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var macrotask = __webpack_require__(/*! ./_task */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_task.js").set;
var Observer = global.MutationObserver || global.WebKitMutationObserver;
var process = global.process;
var Promise = global.Promise;
var isNode = __webpack_require__(/*! ./_cof */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js")(process) == 'process';

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_new-promise-capability.js":
/*!*********************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_new-promise-capability.js ***!
  \*********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// 25.4.1.5 NewPromiseCapability(C)
var aFunction = __webpack_require__(/*! ./_a-function */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js");

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-assign.js":
/*!************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-assign.js ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// 19.1.2.1 Object.assign(target, source, ...)
var getKeys = __webpack_require__(/*! ./_object-keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js");
var gOPS = __webpack_require__(/*! ./_object-gops */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gops.js");
var pIE = __webpack_require__(/*! ./_object-pie */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js");
var toObject = __webpack_require__(/*! ./_to-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js");
var IObject = __webpack_require__(/*! ./_iobject */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iobject.js");
var $assign = Object.assign;

// should work with symbols and should have deterministic property order (V8 bug)
module.exports = !$assign || __webpack_require__(/*! ./_fails */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js")(function () {
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-create.js":
/*!************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-create.js ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.2 / 15.2.3.5 Object.create(O [, Properties])
var anObject = __webpack_require__(/*! ./_an-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var dPs = __webpack_require__(/*! ./_object-dps */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dps.js");
var enumBugKeys = __webpack_require__(/*! ./_enum-bug-keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-bug-keys.js");
var IE_PROTO = __webpack_require__(/*! ./_shared-key */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_shared-key.js")('IE_PROTO');
var Empty = function () { /* empty */ };
var PROTOTYPE = 'prototype';

// Create object with fake `null` prototype: use iframe Object with cleared prototype
var createDict = function () {
  // Thrash, waste and sodomy: IE GC bug
  var iframe = __webpack_require__(/*! ./_dom-create */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_dom-create.js")('iframe');
  var i = enumBugKeys.length;
  var lt = '<';
  var gt = '>';
  var iframeDocument;
  iframe.style.display = 'none';
  __webpack_require__(/*! ./_html */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_html.js").appendChild(iframe);
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var anObject = __webpack_require__(/*! ./_an-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var IE8_DOM_DEFINE = __webpack_require__(/*! ./_ie8-dom-define */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ie8-dom-define.js");
var toPrimitive = __webpack_require__(/*! ./_to-primitive */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-primitive.js");
var dP = Object.defineProperty;

exports.f = __webpack_require__(/*! ./_descriptors */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js") ? Object.defineProperty : function defineProperty(O, P, Attributes) {
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dps.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dps.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var dP = __webpack_require__(/*! ./_object-dp */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js");
var anObject = __webpack_require__(/*! ./_an-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var getKeys = __webpack_require__(/*! ./_object-keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js");

module.exports = __webpack_require__(/*! ./_descriptors */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js") ? Object.defineProperties : function defineProperties(O, Properties) {
  anObject(O);
  var keys = getKeys(Properties);
  var length = keys.length;
  var i = 0;
  var P;
  while (length > i) dP.f(O, P = keys[i++], Properties[P]);
  return O;
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopd.js":
/*!**********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopd.js ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var pIE = __webpack_require__(/*! ./_object-pie */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js");
var createDesc = __webpack_require__(/*! ./_property-desc */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js");
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");
var toPrimitive = __webpack_require__(/*! ./_to-primitive */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-primitive.js");
var has = __webpack_require__(/*! ./_has */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var IE8_DOM_DEFINE = __webpack_require__(/*! ./_ie8-dom-define */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ie8-dom-define.js");
var gOPD = Object.getOwnPropertyDescriptor;

exports.f = __webpack_require__(/*! ./_descriptors */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js") ? gOPD : function getOwnPropertyDescriptor(O, P) {
  O = toIObject(O);
  P = toPrimitive(P, true);
  if (IE8_DOM_DEFINE) try {
    return gOPD(O, P);
  } catch (e) { /* empty */ }
  if (has(O, P)) return createDesc(!pIE.f.call(O, P), O[P]);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn-ext.js":
/*!**************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn-ext.js ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// fallback for IE11 buggy Object.getOwnPropertyNames with iframe and window
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");
var gOPN = __webpack_require__(/*! ./_object-gopn */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn.js").f;
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn.js":
/*!**********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn.js ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.7 / 15.2.3.4 Object.getOwnPropertyNames(O)
var $keys = __webpack_require__(/*! ./_object-keys-internal */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys-internal.js");
var hiddenKeys = __webpack_require__(/*! ./_enum-bug-keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-bug-keys.js").concat('length', 'prototype');

exports.f = Object.getOwnPropertyNames || function getOwnPropertyNames(O) {
  return $keys(O, hiddenKeys);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gops.js":
/*!**********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gops.js ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

exports.f = Object.getOwnPropertySymbols;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gpo.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gpo.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.9 / 15.2.3.2 Object.getPrototypeOf(O)
var has = __webpack_require__(/*! ./_has */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var toObject = __webpack_require__(/*! ./_to-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js");
var IE_PROTO = __webpack_require__(/*! ./_shared-key */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_shared-key.js")('IE_PROTO');
var ObjectProto = Object.prototype;

module.exports = Object.getPrototypeOf || function (O) {
  O = toObject(O);
  if (has(O, IE_PROTO)) return O[IE_PROTO];
  if (typeof O.constructor == 'function' && O instanceof O.constructor) {
    return O.constructor.prototype;
  } return O instanceof Object ? ObjectProto : null;
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys-internal.js":
/*!*******************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys-internal.js ***!
  \*******************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var has = __webpack_require__(/*! ./_has */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");
var arrayIndexOf = __webpack_require__(/*! ./_array-includes */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_array-includes.js")(false);
var IE_PROTO = __webpack_require__(/*! ./_shared-key */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_shared-key.js")('IE_PROTO');

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js":
/*!**********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.14 / 15.2.3.14 Object.keys(O)
var $keys = __webpack_require__(/*! ./_object-keys-internal */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys-internal.js");
var enumBugKeys = __webpack_require__(/*! ./_enum-bug-keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-bug-keys.js");

module.exports = Object.keys || function keys(O) {
  return $keys(O, enumBugKeys);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

exports.f = {}.propertyIsEnumerable;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-sap.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-sap.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// most Object methods by ES6 should accept primitives
var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var core = __webpack_require__(/*! ./_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var fails = __webpack_require__(/*! ./_fails */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js");
module.exports = function (KEY, exec) {
  var fn = (core.Object || {})[KEY] || Object[KEY];
  var exp = {};
  exp[KEY] = exec(fn);
  $export($export.S + $export.F * fails(function () { fn(1); }), 'Object', exp);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_perform.js":
/*!******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_perform.js ***!
  \******************************************************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_promise-resolve.js":
/*!**************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_promise-resolve.js ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var anObject = __webpack_require__(/*! ./_an-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var isObject = __webpack_require__(/*! ./_is-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var newPromiseCapability = __webpack_require__(/*! ./_new-promise-capability */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_new-promise-capability.js");

module.exports = function (C, x) {
  anObject(C);
  if (isObject(x) && x.constructor === C) return x;
  var promiseCapability = newPromiseCapability.f(C);
  var resolve = promiseCapability.resolve;
  resolve(x);
  return promiseCapability.promise;
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js":
/*!************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js ***!
  \************************************************************************************************************/
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine-all.js":
/*!***********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine-all.js ***!
  \***********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var hide = __webpack_require__(/*! ./_hide */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js");
module.exports = function (target, src, safe) {
  for (var key in src) {
    if (safe && target[key]) target[key] = src[key];
    else hide(target, key, src[key]);
  } return target;
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine.js":
/*!*******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine.js ***!
  \*******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! ./_hide */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js");


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-proto.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-proto.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// Works with __proto__ only. Old v8 can't work with null proto objects.
/* eslint-disable no-proto */
var isObject = __webpack_require__(/*! ./_is-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var anObject = __webpack_require__(/*! ./_an-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var check = function (O, proto) {
  anObject(O);
  if (!isObject(proto) && proto !== null) throw TypeError(proto + ": can't set as prototype!");
};
module.exports = {
  set: Object.setPrototypeOf || ('__proto__' in {} ? // eslint-disable-line
    function (test, buggy, set) {
      try {
        set = __webpack_require__(/*! ./_ctx */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js")(Function.call, __webpack_require__(/*! ./_object-gopd */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopd.js").f(Object.prototype, '__proto__').set, 2);
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-species.js":
/*!**********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-species.js ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var core = __webpack_require__(/*! ./_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var dP = __webpack_require__(/*! ./_object-dp */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js");
var DESCRIPTORS = __webpack_require__(/*! ./_descriptors */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js");
var SPECIES = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('species');

module.exports = function (KEY) {
  var C = typeof core[KEY] == 'function' ? core[KEY] : global[KEY];
  if (DESCRIPTORS && C && !C[SPECIES]) dP.f(C, SPECIES, {
    configurable: true,
    get: function () { return this; }
  });
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js":
/*!****************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js ***!
  \****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var def = __webpack_require__(/*! ./_object-dp */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js").f;
var has = __webpack_require__(/*! ./_has */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var TAG = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('toStringTag');

module.exports = function (it, tag, stat) {
  if (it && !has(it = stat ? it : it.prototype, TAG)) def(it, TAG, { configurable: true, value: tag });
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_shared-key.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_shared-key.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var shared = __webpack_require__(/*! ./_shared */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_shared.js")('keys');
var uid = __webpack_require__(/*! ./_uid */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js");
module.exports = function (key) {
  return shared[key] || (shared[key] = uid(key));
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_shared.js":
/*!*****************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_shared.js ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var core = __webpack_require__(/*! ./_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var SHARED = '__core-js_shared__';
var store = global[SHARED] || (global[SHARED] = {});

(module.exports = function (key, value) {
  return store[key] || (store[key] = value !== undefined ? value : {});
})('versions', []).push({
  version: core.version,
  mode: __webpack_require__(/*! ./_library */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js") ? 'pure' : 'global',
  copyright: '© 2018 Denis Pushkarev (zloirock.ru)'
});


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_species-constructor.js":
/*!******************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_species-constructor.js ***!
  \******************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 7.3.20 SpeciesConstructor(O, defaultConstructor)
var anObject = __webpack_require__(/*! ./_an-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var aFunction = __webpack_require__(/*! ./_a-function */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js");
var SPECIES = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('species');
module.exports = function (O, D) {
  var C = anObject(O).constructor;
  var S;
  return C === undefined || (S = anObject(C)[SPECIES]) == undefined ? D : aFunction(S);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_string-at.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_string-at.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var toInteger = __webpack_require__(/*! ./_to-integer */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-integer.js");
var defined = __webpack_require__(/*! ./_defined */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_defined.js");
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_task.js":
/*!***************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_task.js ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var ctx = __webpack_require__(/*! ./_ctx */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js");
var invoke = __webpack_require__(/*! ./_invoke */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_invoke.js");
var html = __webpack_require__(/*! ./_html */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_html.js");
var cel = __webpack_require__(/*! ./_dom-create */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_dom-create.js");
var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
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
  if (__webpack_require__(/*! ./_cof */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_cof.js")(process) == 'process') {
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-absolute-index.js":
/*!****************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-absolute-index.js ***!
  \****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var toInteger = __webpack_require__(/*! ./_to-integer */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-integer.js");
var max = Math.max;
var min = Math.min;
module.exports = function (index, length) {
  index = toInteger(index);
  return index < 0 ? max(index + length, 0) : min(index, length);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-integer.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-integer.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// 7.1.4 ToInteger
var ceil = Math.ceil;
var floor = Math.floor;
module.exports = function (it) {
  return isNaN(it = +it) ? 0 : (it > 0 ? floor : ceil)(it);
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// to indexed object, toObject with fallback for non-array-like ES3 strings
var IObject = __webpack_require__(/*! ./_iobject */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iobject.js");
var defined = __webpack_require__(/*! ./_defined */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_defined.js");
module.exports = function (it) {
  return IObject(defined(it));
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-length.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-length.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 7.1.15 ToLength
var toInteger = __webpack_require__(/*! ./_to-integer */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-integer.js");
var min = Math.min;
module.exports = function (it) {
  return it > 0 ? min(toInteger(it), 0x1fffffffffffff) : 0; // pow(2, 53) - 1 == 9007199254740991
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 7.1.13 ToObject(argument)
var defined = __webpack_require__(/*! ./_defined */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_defined.js");
module.exports = function (it) {
  return Object(defined(it));
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-primitive.js":
/*!***********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-primitive.js ***!
  \***********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 7.1.1 ToPrimitive(input [, PreferredType])
var isObject = __webpack_require__(/*! ./_is-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js":
/*!**************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

var id = 0;
var px = Math.random();
module.exports = function (key) {
  return 'Symbol('.concat(key === undefined ? '' : key, ')_', (++id + px).toString(36));
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_user-agent.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_user-agent.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var navigator = global.navigator;

module.exports = navigator && navigator.userAgent || '';


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-define.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-define.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var core = __webpack_require__(/*! ./_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var LIBRARY = __webpack_require__(/*! ./_library */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js");
var wksExt = __webpack_require__(/*! ./_wks-ext */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-ext.js");
var defineProperty = __webpack_require__(/*! ./_object-dp */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js").f;
module.exports = function (name) {
  var $Symbol = core.Symbol || (core.Symbol = LIBRARY ? {} : global.Symbol || {});
  if (name.charAt(0) != '_' && !(name in $Symbol)) defineProperty($Symbol, name, { value: wksExt.f(name) });
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-ext.js":
/*!******************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-ext.js ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

exports.f = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js");


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js":
/*!**************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var store = __webpack_require__(/*! ./_shared */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_shared.js")('wks');
var uid = __webpack_require__(/*! ./_uid */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js");
var Symbol = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js").Symbol;
var USE_SYMBOL = typeof Symbol == 'function';

var $exports = module.exports = function (name) {
  return store[name] || (store[name] =
    USE_SYMBOL && Symbol[name] || (USE_SYMBOL ? Symbol : uid)('Symbol.' + name));
};

$exports.store = store;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/core.get-iterator-method.js":
/*!**********************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/core.get-iterator-method.js ***!
  \**********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var classof = __webpack_require__(/*! ./_classof */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_classof.js");
var ITERATOR = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('iterator');
var Iterators = __webpack_require__(/*! ./_iterators */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js");
module.exports = __webpack_require__(/*! ./_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js").getIteratorMethod = function (it) {
  if (it != undefined) return it[ITERATOR]
    || it['@@iterator']
    || Iterators[classof(it)];
};


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.from.js":
/*!************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.from.js ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var ctx = __webpack_require__(/*! ./_ctx */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js");
var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var toObject = __webpack_require__(/*! ./_to-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js");
var call = __webpack_require__(/*! ./_iter-call */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-call.js");
var isArrayIter = __webpack_require__(/*! ./_is-array-iter */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array-iter.js");
var toLength = __webpack_require__(/*! ./_to-length */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-length.js");
var createProperty = __webpack_require__(/*! ./_create-property */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_create-property.js");
var getIterFn = __webpack_require__(/*! ./core.get-iterator-method */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/core.get-iterator-method.js");

$export($export.S + $export.F * !__webpack_require__(/*! ./_iter-detect */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-detect.js")(function (iter) { Array.from(iter); }), 'Array', {
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.iterator.js":
/*!****************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.iterator.js ***!
  \****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var addToUnscopables = __webpack_require__(/*! ./_add-to-unscopables */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_add-to-unscopables.js");
var step = __webpack_require__(/*! ./_iter-step */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-step.js");
var Iterators = __webpack_require__(/*! ./_iterators */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js");
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");

// 22.1.3.4 Array.prototype.entries()
// 22.1.3.13 Array.prototype.keys()
// 22.1.3.29 Array.prototype.values()
// 22.1.3.30 Array.prototype[@@iterator]()
module.exports = __webpack_require__(/*! ./_iter-define */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-define.js")(Array, 'Array', function (iterated, kind) {
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.assign.js":
/*!***************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.assign.js ***!
  \***************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.3.1 Object.assign(target, source)
var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");

$export($export.S + $export.F, 'Object', { assign: __webpack_require__(/*! ./_object-assign */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-assign.js") });


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.create.js":
/*!***************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.create.js ***!
  \***************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
// 19.1.2.2 / 15.2.3.5 Object.create(O [, Properties])
$export($export.S, 'Object', { create: __webpack_require__(/*! ./_object-create */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-create.js") });


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.define-property.js":
/*!************************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.define-property.js ***!
  \************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
// 19.1.2.4 / 15.2.3.6 Object.defineProperty(O, P, Attributes)
$export($export.S + $export.F * !__webpack_require__(/*! ./_descriptors */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js"), 'Object', { defineProperty: __webpack_require__(/*! ./_object-dp */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js").f });


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.get-prototype-of.js":
/*!*************************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.get-prototype-of.js ***!
  \*************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.9 Object.getPrototypeOf(O)
var toObject = __webpack_require__(/*! ./_to-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js");
var $getPrototypeOf = __webpack_require__(/*! ./_object-gpo */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gpo.js");

__webpack_require__(/*! ./_object-sap */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-sap.js")('getPrototypeOf', function () {
  return function getPrototypeOf(it) {
    return $getPrototypeOf(toObject(it));
  };
});


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.keys.js":
/*!*************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.keys.js ***!
  \*************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.14 Object.keys(O)
var toObject = __webpack_require__(/*! ./_to-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-object.js");
var $keys = __webpack_require__(/*! ./_object-keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js");

__webpack_require__(/*! ./_object-sap */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-sap.js")('keys', function () {
  return function keys(it) {
    return $keys(toObject(it));
  };
});


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.set-prototype-of.js":
/*!*************************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.set-prototype-of.js ***!
  \*************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// 19.1.3.19 Object.setPrototypeOf(O, proto)
var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
$export($export.S, 'Object', { setPrototypeOf: __webpack_require__(/*! ./_set-proto */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-proto.js").set });


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.to-string.js":
/*!******************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.object.to-string.js ***!
  \******************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {



/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.promise.js":
/*!*********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.promise.js ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var LIBRARY = __webpack_require__(/*! ./_library */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js");
var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var ctx = __webpack_require__(/*! ./_ctx */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_ctx.js");
var classof = __webpack_require__(/*! ./_classof */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_classof.js");
var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var isObject = __webpack_require__(/*! ./_is-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var aFunction = __webpack_require__(/*! ./_a-function */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_a-function.js");
var anInstance = __webpack_require__(/*! ./_an-instance */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-instance.js");
var forOf = __webpack_require__(/*! ./_for-of */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_for-of.js");
var speciesConstructor = __webpack_require__(/*! ./_species-constructor */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_species-constructor.js");
var task = __webpack_require__(/*! ./_task */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_task.js").set;
var microtask = __webpack_require__(/*! ./_microtask */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_microtask.js")();
var newPromiseCapabilityModule = __webpack_require__(/*! ./_new-promise-capability */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_new-promise-capability.js");
var perform = __webpack_require__(/*! ./_perform */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_perform.js");
var userAgent = __webpack_require__(/*! ./_user-agent */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_user-agent.js");
var promiseResolve = __webpack_require__(/*! ./_promise-resolve */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_promise-resolve.js");
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
    var FakePromise = (promise.constructor = {})[__webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('species')] = function (exec) {
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
  Internal.prototype = __webpack_require__(/*! ./_redefine-all */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine-all.js")($Promise.prototype, {
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
__webpack_require__(/*! ./_set-to-string-tag */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js")($Promise, PROMISE);
__webpack_require__(/*! ./_set-species */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-species.js")(PROMISE);
Wrapper = __webpack_require__(/*! ./_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js")[PROMISE];

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
$export($export.S + $export.F * !(USE_NATIVE && __webpack_require__(/*! ./_iter-detect */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-detect.js")(function (iter) {
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.string.iterator.js":
/*!*****************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.string.iterator.js ***!
  \*****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $at = __webpack_require__(/*! ./_string-at */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_string-at.js")(true);

// 21.1.3.27 String.prototype[@@iterator]()
__webpack_require__(/*! ./_iter-define */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iter-define.js")(String, 'String', function (iterated) {
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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.symbol.js":
/*!********************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.symbol.js ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// ECMAScript 6 symbols shim
var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var has = __webpack_require__(/*! ./_has */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_has.js");
var DESCRIPTORS = __webpack_require__(/*! ./_descriptors */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_descriptors.js");
var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var redefine = __webpack_require__(/*! ./_redefine */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_redefine.js");
var META = __webpack_require__(/*! ./_meta */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_meta.js").KEY;
var $fails = __webpack_require__(/*! ./_fails */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_fails.js");
var shared = __webpack_require__(/*! ./_shared */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_shared.js");
var setToStringTag = __webpack_require__(/*! ./_set-to-string-tag */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_set-to-string-tag.js");
var uid = __webpack_require__(/*! ./_uid */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_uid.js");
var wks = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js");
var wksExt = __webpack_require__(/*! ./_wks-ext */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-ext.js");
var wksDefine = __webpack_require__(/*! ./_wks-define */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-define.js");
var enumKeys = __webpack_require__(/*! ./_enum-keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_enum-keys.js");
var isArray = __webpack_require__(/*! ./_is-array */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-array.js");
var anObject = __webpack_require__(/*! ./_an-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_an-object.js");
var isObject = __webpack_require__(/*! ./_is-object */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_is-object.js");
var toIObject = __webpack_require__(/*! ./_to-iobject */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-iobject.js");
var toPrimitive = __webpack_require__(/*! ./_to-primitive */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_to-primitive.js");
var createDesc = __webpack_require__(/*! ./_property-desc */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_property-desc.js");
var _create = __webpack_require__(/*! ./_object-create */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-create.js");
var gOPNExt = __webpack_require__(/*! ./_object-gopn-ext */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn-ext.js");
var $GOPD = __webpack_require__(/*! ./_object-gopd */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopd.js");
var $DP = __webpack_require__(/*! ./_object-dp */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-dp.js");
var $keys = __webpack_require__(/*! ./_object-keys */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-keys.js");
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
  __webpack_require__(/*! ./_object-gopn */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gopn.js").f = gOPNExt.f = $getOwnPropertyNames;
  __webpack_require__(/*! ./_object-pie */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-pie.js").f = $propertyIsEnumerable;
  __webpack_require__(/*! ./_object-gops */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_object-gops.js").f = $getOwnPropertySymbols;

  if (DESCRIPTORS && !__webpack_require__(/*! ./_library */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_library.js")) {
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
$Symbol[PROTOTYPE][TO_PRIMITIVE] || __webpack_require__(/*! ./_hide */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js")($Symbol[PROTOTYPE], TO_PRIMITIVE, $Symbol[PROTOTYPE].valueOf);
// 19.4.3.5 Symbol.prototype[@@toStringTag]
setToStringTag($Symbol, 'Symbol');
// 20.2.1.9 Math[@@toStringTag]
setToStringTag(Math, 'Math', true);
// 24.3.3 JSON[@@toStringTag]
setToStringTag(global.JSON, 'JSON', true);


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.finally.js":
/*!*****************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.finally.js ***!
  \*****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
// https://github.com/tc39/proposal-promise-finally

var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var core = __webpack_require__(/*! ./_core */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_core.js");
var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var speciesConstructor = __webpack_require__(/*! ./_species-constructor */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_species-constructor.js");
var promiseResolve = __webpack_require__(/*! ./_promise-resolve */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_promise-resolve.js");

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.try.js":
/*!*************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.promise.try.js ***!
  \*************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// https://github.com/tc39/proposal-promise-try
var $export = __webpack_require__(/*! ./_export */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_export.js");
var newPromiseCapability = __webpack_require__(/*! ./_new-promise-capability */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_new-promise-capability.js");
var perform = __webpack_require__(/*! ./_perform */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_perform.js");

$export($export.S, 'Promise', { 'try': function (callbackfn) {
  var promiseCapability = newPromiseCapability.f(this);
  var result = perform(callbackfn);
  (result.e ? promiseCapability.reject : promiseCapability.resolve)(result.v);
  return promiseCapability.promise;
} });


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.async-iterator.js":
/*!***********************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.async-iterator.js ***!
  \***********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ./_wks-define */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-define.js")('asyncIterator');


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.observable.js":
/*!*******************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es7.symbol.observable.js ***!
  \*******************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ./_wks-define */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks-define.js")('observable');


/***/ }),

/***/ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/web.dom.iterable.js":
/*!**************************************************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/web.dom.iterable.js ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ./es6.array.iterator */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/es6.array.iterator.js");
var global = __webpack_require__(/*! ./_global */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_global.js");
var hide = __webpack_require__(/*! ./_hide */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_hide.js");
var Iterators = __webpack_require__(/*! ./_iterators */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_iterators.js");
var TO_STRING_TAG = __webpack_require__(/*! ./_wks */ "./Areas/Portal/www/node_modules/babel-runtime/node_modules/core-js/library/modules/_wks.js")('toStringTag');

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

/***/ "./Areas/Portal/www/node_modules/babel-runtime/regenerator/index.js":
/*!**************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/babel-runtime/regenerator/index.js ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! regenerator-runtime */ "./Areas/Portal/www/node_modules/regenerator-runtime/runtime-module.js");


/***/ }),

/***/ "./Areas/Portal/www/node_modules/fbjs/lib/emptyFunction.js":
/*!********************************************************************************************************!*\
  !*** delegated ./node_modules/fbjs/lib/emptyFunction.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(23);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/fbjs/lib/emptyObject.js":
/*!******************************************************************************************************!*\
  !*** delegated ./node_modules/fbjs/lib/emptyObject.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(22);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/fbjs/lib/invariant.js":
/*!****************************************************************************************************!*\
  !*** delegated ./node_modules/fbjs/lib/invariant.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(21);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/fbjs/lib/warning.js":
/*!***********************************************************!*\
  !*** ./Areas/Portal/www/node_modules/fbjs/lib/warning.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/**
 * Copyright (c) 2014-present, Facebook, Inc.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 *
 */



var emptyFunction = __webpack_require__(/*! ./emptyFunction */ "./Areas/Portal/www/node_modules/fbjs/lib/emptyFunction.js");

/**
 * Similar to invariant but only logs a warning if the condition is not met.
 * This can be used to log issues in development environments in critical
 * paths. Removing the logging code for production environments will keep the
 * same logic and follow the same code paths.
 */

var warning = emptyFunction;

if (true) {
  var printWarning = function printWarning(format) {
    for (var _len = arguments.length, args = Array(_len > 1 ? _len - 1 : 0), _key = 1; _key < _len; _key++) {
      args[_key - 1] = arguments[_key];
    }

    var argIndex = 0;
    var message = 'Warning: ' + format.replace(/%s/g, function () {
      return args[argIndex++];
    });
    if (typeof console !== 'undefined') {
      console.error(message);
    }
    try {
      // --- Welcome to debugging React ---
      // This error was thrown as a convenience so that you can use this stack
      // to find the callsite that caused this warning to fire.
      throw new Error(message);
    } catch (x) {}
  };

  warning = function warning(condition, format) {
    if (format === undefined) {
      throw new Error('`warning(condition, format, ...args)` requires a warning ' + 'message argument');
    }

    if (format.indexOf('Failed Composite propType: ') === 0) {
      return; // Ignore CompositeComponent proptype check.
    }

    if (!condition) {
      for (var _len2 = arguments.length, args = Array(_len2 > 2 ? _len2 - 2 : 0), _key2 = 2; _key2 < _len2; _key2++) {
        args[_key2 - 2] = arguments[_key2];
      }

      printWarning.apply(undefined, [format].concat(args));
    }
  };
}

module.exports = warning;

/***/ }),

/***/ "./Areas/Portal/www/node_modules/is-buffer/index.js":
/*!**********************************************************!*\
  !*** ./Areas/Portal/www/node_modules/is-buffer/index.js ***!
  \**********************************************************/
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

/***/ "./Areas/Portal/www/node_modules/object-assign/index.js":
/*!*****************************************************************************************************!*\
  !*** delegated ./node_modules/object-assign/index.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(20);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/prop-types/checkPropTypes.js":
/*!********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/prop-types/checkPropTypes.js ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/**
 * Copyright (c) 2013-present, Facebook, Inc.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */



var printWarning = function() {};

if (true) {
  var ReactPropTypesSecret = __webpack_require__(/*! ./lib/ReactPropTypesSecret */ "./Areas/Portal/www/node_modules/prop-types/lib/ReactPropTypesSecret.js");
  var loggedTypeFailures = {};

  printWarning = function(text) {
    var message = 'Warning: ' + text;
    if (typeof console !== 'undefined') {
      console.error(message);
    }
    try {
      // --- Welcome to debugging React ---
      // This error was thrown as a convenience so that you can use this stack
      // to find the callsite that caused this warning to fire.
      throw new Error(message);
    } catch (x) {}
  };
}

/**
 * Assert that the values match with the type specs.
 * Error messages are memorized and will only be shown once.
 *
 * @param {object} typeSpecs Map of name to a ReactPropType
 * @param {object} values Runtime values that need to be type-checked
 * @param {string} location e.g. "prop", "context", "child context"
 * @param {string} componentName Name of the component for error messages.
 * @param {?Function} getStack Returns the component stack.
 * @private
 */
function checkPropTypes(typeSpecs, values, location, componentName, getStack) {
  if (true) {
    for (var typeSpecName in typeSpecs) {
      if (typeSpecs.hasOwnProperty(typeSpecName)) {
        var error;
        // Prop type validation may throw. In case they do, we don't want to
        // fail the render phase where it didn't fail before. So we log it.
        // After these have been cleaned up, we'll let them throw.
        try {
          // This is intentionally an invariant that gets caught. It's the same
          // behavior as without this statement except with a better message.
          if (typeof typeSpecs[typeSpecName] !== 'function') {
            var err = Error(
              (componentName || 'React class') + ': ' + location + ' type `' + typeSpecName + '` is invalid; ' +
              'it must be a function, usually from the `prop-types` package, but received `' + typeof typeSpecs[typeSpecName] + '`.'
            );
            err.name = 'Invariant Violation';
            throw err;
          }
          error = typeSpecs[typeSpecName](values, typeSpecName, componentName, location, null, ReactPropTypesSecret);
        } catch (ex) {
          error = ex;
        }
        if (error && !(error instanceof Error)) {
          printWarning(
            (componentName || 'React class') + ': type specification of ' +
            location + ' `' + typeSpecName + '` is invalid; the type checker ' +
            'function must return `null` or an `Error` but returned a ' + typeof error + '. ' +
            'You may have forgotten to pass an argument to the type checker ' +
            'creator (arrayOf, instanceOf, objectOf, oneOf, oneOfType, and ' +
            'shape all require an argument).'
          )

        }
        if (error instanceof Error && !(error.message in loggedTypeFailures)) {
          // Only monitor this failure once because there tends to be a lot of the
          // same error.
          loggedTypeFailures[error.message] = true;

          var stack = getStack ? getStack() : '';

          printWarning(
            'Failed ' + location + ' type: ' + error.message + (stack != null ? stack : '')
          );
        }
      }
    }
  }
}

module.exports = checkPropTypes;


/***/ }),

/***/ "./Areas/Portal/www/node_modules/prop-types/lib/ReactPropTypesSecret.js":
/*!*********************************************************************************************************************!*\
  !*** delegated ./node_modules/prop-types/lib/ReactPropTypesSecret.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \*********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(58);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/react-dom/index.js":
/*!*************************************************************************************************!*\
  !*** delegated ./node_modules/react-dom/index.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \*************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(4);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/react-redux/es/index.js":
/*!******************************************************************************************************!*\
  !*** delegated ./node_modules/react-redux/es/index.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \******************************************************************************************************/
/*! exports provided: Provider, createProvider, connectAdvanced, connect */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(56);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/react/index.js":
/*!*********************************************************************************************!*\
  !*** delegated ./node_modules/react/index.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \*********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(1);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/reactstrap/dist/reactstrap.es.js":
/*!***************************************************************************************************************!*\
  !*** delegated ./node_modules/reactstrap/dist/reactstrap.es.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \***************************************************************************************************************/
/*! exports provided: Alert, Container, Row, Col, Navbar, NavbarBrand, NavbarToggler, Nav, NavItem, NavDropdown, NavLink, Breadcrumb, BreadcrumbItem, Button, ButtonDropdown, ButtonGroup, ButtonToolbar, Dropdown, DropdownItem, DropdownMenu, DropdownToggle, Fade, Badge, Card, CardLink, CardGroup, CardDeck, CardColumns, CardBody, CardBlock, CardFooter, CardHeader, CardImg, CardImgOverlay, Carousel, UncontrolledCarousel, CarouselControl, CarouselItem, CarouselIndicators, CarouselCaption, CardSubtitle, CardText, CardTitle, Popover, PopoverContent, PopoverBody, PopoverTitle, PopoverHeader, Progress, Modal, ModalHeader, ModalBody, ModalFooter, PopperContent, PopperTargetHelper, Tooltip, Table, ListGroup, Form, FormFeedback, FormGroup, FormText, Input, InputGroup, InputGroupAddon, InputGroupButton, InputGroupButtonDropdown, InputGroupText, Label, CustomInput, Media, Pagination, PaginationItem, PaginationLink, TabContent, TabPane, Jumbotron, Collapse, ListGroupItem, ListGroupItemText, ListGroupItemHeading, UncontrolledAlert, UncontrolledButtonDropdown, UncontrolledCollapse, UncontrolledDropdown, UncontrolledNavDropdown, UncontrolledTooltip, Util */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(62);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/redux-thunk/es/index.js":
/*!******************************************************************************************************!*\
  !*** delegated ./node_modules/redux-thunk/es/index.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \******************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(61);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/redux/es/redux.js":
/*!************************************************************************************************!*\
  !*** delegated ./node_modules/redux/es/redux.js from dll-reference react_bb523aa7e14eeaff8f10 ***!
  \************************************************************************************************/
/*! exports provided: createStore, combineReducers, bindActionCreators, applyMiddleware, compose, __DO_NOT_USE__ActionTypes */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_bb523aa7e14eeaff8f10 */ "dll-reference react_bb523aa7e14eeaff8f10"))(15);

/***/ }),

/***/ "./Areas/Portal/www/node_modules/regenerator-runtime/runtime-module.js":
/*!*****************************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/regenerator-runtime/runtime-module.js ***!
  \*****************************************************************************/
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

module.exports = __webpack_require__(/*! ./runtime */ "./Areas/Portal/www/node_modules/regenerator-runtime/runtime.js");

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

/***/ "./Areas/Portal/www/node_modules/regenerator-runtime/runtime.js":
/*!**********************************************************************!*\
  !*** ./Areas/Portal/www/node_modules/regenerator-runtime/runtime.js ***!
  \**********************************************************************/
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

/***/ "./Areas/Portal/www/source/js/actions/actionTypes.js":
/*!***********************************************************!*\
  !*** ./Areas/Portal/www/source/js/actions/actionTypes.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
var SET_PAGING_OPTION = exports.SET_PAGING_OPTION = 'SET_PAGING_OPTION';
var TOGGLE_LOADER = exports.TOGGLE_LOADER = 'TOGGLE_LOADER';

/***/ }),

/***/ "./Areas/Portal/www/source/js/actions/loaderAction.js":
/*!************************************************************!*\
  !*** ./Areas/Portal/www/source/js/actions/loaderAction.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.toggleLoader = undefined;

var _actionTypes = __webpack_require__(/*! ./actionTypes */ "./Areas/Portal/www/source/js/actions/actionTypes.js");

var toggleLoader = exports.toggleLoader = function toggleLoader(data) {
    return {
        type: _actionTypes.TOGGLE_LOADER,
        data: data
    };
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/actions/pagingAction.js":
/*!************************************************************!*\
  !*** ./Areas/Portal/www/source/js/actions/pagingAction.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.setPagingOption = undefined;

var _actionTypes = __webpack_require__(/*! ./actionTypes */ "./Areas/Portal/www/source/js/actions/actionTypes.js");

var setPagingOption = exports.setPagingOption = function setPagingOption(data) {
    return {
        type: _actionTypes.SET_PAGING_OPTION,
        data: data
    };
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/components/Loader.jsx":
/*!**********************************************************!*\
  !*** ./Areas/Portal/www/source/js/components/Loader.jsx ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _react = __webpack_require__(/*! react */ "./Areas/Portal/www/node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = function (props) {
    var backdrop = props.backdrop === undefined ? true : props.backdrop;

    return _react2.default.createElement(
        'div',
        { className: 'loader', style: { display: props.show ? 'block' : 'none' } },
        _react2.default.createElement('div', { className: 'backdrop', style: { display: backdrop ? 'block' : 'none' } }),
        _react2.default.createElement(
            'div',
            { className: 'spinner' },
            _react2.default.createElement('div', null),
            _react2.default.createElement('div', null),
            _react2.default.createElement('div', null),
            _react2.default.createElement('div', null)
        )
    );
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/components/ReportTitle.jsx":
/*!***************************************************************!*\
  !*** ./Areas/Portal/www/source/js/components/ReportTitle.jsx ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _react = __webpack_require__(/*! react */ "./Areas/Portal/www/node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var ReportTitle = function ReportTitle(props) {
    return _react2.default.createElement(
        "div",
        { className: "report-title-container" },
        _react2.default.createElement(
            "div",
            { className: "report-title" },
            props.content
        )
    );
};

exports.default = ReportTitle;

/***/ }),

/***/ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx":
/*!**********************************************************!*\
  !*** ./Areas/Portal/www/source/js/layouts/AppLayout.jsx ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./Areas/Portal/www/node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactDom = __webpack_require__(/*! react-dom */ "./Areas/Portal/www/node_modules/react-dom/index.js");

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
        { id: 'container', className: 'app-container' },
        this.props.children
      );
    }
  }]);
  return AppLayout;
}(_react.Component);

exports.default = AppLayout;

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/ajaxRequest.js":
/*!***********************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/ajaxRequest.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.post = undefined;

var _promise = __webpack_require__(/*! babel-runtime/core-js/promise */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/promise.js");

var _promise2 = _interopRequireDefault(_promise);

var _axios = __webpack_require__(/*! axios */ "./Areas/Portal/www/node_modules/axios/index.js");

var _axios2 = _interopRequireDefault(_axios);

var _utils = __webpack_require__(/*! ./utils */ "./Areas/Portal/www/source/js/modules/utils.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var _xsrfToken = null;

var getXsrfToken = function getXsrfToken() {
    if (!_xsrfToken) {
        _xsrfToken = document.querySelector("input[name=__RequestVerificationToken]").value;
    }

    return _xsrfToken;
};

_axios2.default.interceptors.response.use(function (response) {
    return _promise2.default.resolve(response);
}, function (error) {
    var response = error.response;

    if (response) {
        var data = response.data;

        var status = data && data.ErrCode || response.status;

        switch (status) {
            case 401:
            case 403:
                alert("You are not authorized on this function.");
                location.reload(true);
                break;
            case 500:
                alert("An error occurred while processing your request");
                break;
            default:
                break;
        }
    }

    return _promise2.default.reject(error);
});

var post = exports.post = function post(relativeUrl, params, options) {
    var url = (0, _utils.resolveClientUrl)(relativeUrl);
    return _axios2.default.post(url, params, {
        headers: {
            'X-Requested-With': 'XMLHttpRequest',
            '__RequestVerificationToken': getXsrfToken()
        }
    }).then(function (response) {
        return response;
    }).catch(function (err) {
        options && options.onError && options.onError(err);
    });
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/constants.js":
/*!*********************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/constants.js ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
var defaultPagingOption = exports.defaultPagingOption = {
    PageIndex: 1,
    PageSize: 20,
    TotalItem: 0,
    PageList: [10, 20, 50, 100, 500]
};

var minMonthDate = exports.minMonthDate = { month: 12, year: 2009 };

var shortedMonthNames = exports.shortedMonthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/createReducer.js":
/*!*************************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/createReducer.js ***!
  \*************************************************************/
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

/***/ "./Areas/Portal/www/source/js/modules/dataHandler.js":
/*!***********************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/dataHandler.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.updateObjectInArray = exports.deepClone = exports.removeItem = undefined;

var _extends2 = __webpack_require__(/*! babel-runtime/helpers/extends */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/extends.js");

var _extends3 = _interopRequireDefault(_extends2);

var _stringify = __webpack_require__(/*! babel-runtime/core-js/json/stringify */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/json/stringify.js");

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

/***/ "./Areas/Portal/www/source/js/modules/excel.js":
/*!*****************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/excel.js ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _typeof2 = __webpack_require__(/*! babel-runtime/helpers/typeof */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/typeof.js");

var _typeof3 = _interopRequireDefault(_typeof2);

var _regenerator = __webpack_require__(/*! babel-runtime/regenerator */ "./Areas/Portal/www/node_modules/babel-runtime/regenerator/index.js");

var _regenerator2 = _interopRequireDefault(_regenerator);

var _asyncToGenerator2 = __webpack_require__(/*! babel-runtime/helpers/asyncToGenerator */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/asyncToGenerator.js");

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var EXCEL_TYPE = "xlsx";
var MapExcelType = {
    "number": "n",
    "string": "s",
    "boolean": "b",
    "object": "d"
};

var _XLSX = null;

var Excel = function () {
    function Excel() {
        (0, _classCallCheck3.default)(this, Excel);
    }

    (0, _createClass3.default)(Excel, null, [{
        key: "process",
        value: function () {
            var _ref = (0, _asyncToGenerator3.default)( /*#__PURE__*/_regenerator2.default.mark(function _callee(callback) {
                return _regenerator2.default.wrap(function _callee$(_context) {
                    while (1) {
                        switch (_context.prev = _context.next) {
                            case 0:
                                _context.next = 2;
                                return Promise.all(/*! import() | xlsx-style */[__webpack_require__.e("vendors~xlsx-style"), __webpack_require__.e("xlsx-style")]).then(__webpack_require__.t.bind(null, /*! xlsx-style-sparta */ "./node_modules/xlsx-style-sparta/xlsx.js", 7));

                            case 2:
                                _XLSX = _context.sent;


                                if (_XLSX) {
                                    callback && callback();
                                }

                            case 4:
                            case "end":
                                return _context.stop();
                        }
                    }
                }, _callee, this);
            }));

            function process(_x) {
                return _ref.apply(this, arguments);
            }

            return process;
        }()
    }, {
        key: "export",
        value: function _export(worksheet, fileName) {
            var sheetName = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : 'Sheet 1';

            var option = { bookType: EXCEL_TYPE, bookSST: false, type: 'binary' };
            var workbook = { SheetNames: [sheetName], Sheets: {}, Props: {} };
            workbook.Sheets[sheetName] = worksheet;

            var workbookOutput = _XLSX.write(workbook, option);
            this.download(workbookOutput, fileName);
        }
    }, {
        key: "getCellType",
        value: function getCellType(value) {
            return MapExcelType[typeof value === "undefined" ? "undefined" : (0, _typeof3.default)(value)];
        }
    }, {
        key: "getCellAddress",
        value: function getCellAddress(cellObject) {
            return _XLSX.utils.encode_cell(cellObject);
        }
    }, {
        key: "download",
        value: function download(workbookOut, fileName) {
            var blob = new Blob([this.sheetToArrayBuffer(workbookOut)], {
                type: "application/octet-stream"
            });

            this.downloadFromBolb(blob, fileName);
        }
    }, {
        key: "downloadFromBolb",
        value: function downloadFromBolb(blob, fileName) {
            var fileNameDownload = fileName + "." + EXCEL_TYPE;

            // IE 10+
            if (typeof navigator !== "undefined" && navigator.msSaveOrOpenBlob) {
                navigator.msSaveOrOpenBlob(blob, fileNameDownload);
            } else {
                var element = window.document.createElement('a');
                element.href = window.URL.createObjectURL(blob);
                element.download = fileNameDownload;
                document.body.appendChild(element);
                element.click();
                document.body.removeChild(element);
            }
        }
    }, {
        key: "sheetToArrayBuffer",
        value: function sheetToArrayBuffer(s) {
            var buffer = new ArrayBuffer(s.length);
            var view = new Uint8Array(buffer);
            for (var i = 0; i != s.length; ++i) {
                view[i] = s.charCodeAt(i) & 0xFF;
            }return buffer;
        }
    }]);
    return Excel;
}();

exports.default = Excel;

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/excelStyleConstant.js":
/*!******************************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/excelStyleConstant.js ***!
  \******************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.totalCellStyle = exports.bodyCellStyle = exports.headerCellStyle = exports.titleCellStyle = undefined;

var _extends2 = __webpack_require__(/*! babel-runtime/helpers/extends */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/extends.js");

var _extends3 = _interopRequireDefault(_extends2);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var titleCellStyle = exports.titleCellStyle = {
    font: {
        bold: true,
        color: {
            rgb: "333333"
        }
    },
    fill: {
        patternType: "solid",
        fgColor: {
            rgb: "cdd3d9"
        }
    }
};

var headerCellStyle = exports.headerCellStyle = {
    font: {
        bold: true,
        color: {
            rgb: "333333"
        },
        sz: "12"
    },
    alignment: {
        horizontal: "center",
        vertical: "center"
    },
    fill: {
        patternType: "solid",
        fgColor: {
            rgb: "e0e0e0"
        }
    },
    border: {
        left: {
            style: "thin",
            color: {
                rgb: "cccccc"
            }
        },
        right: {
            style: "thin",
            color: {
                rgb: "cccccc"
            }
        },
        top: {
            style: "thin",
            color: {
                rgb: "cccccc"
            }
        },
        bottom: {
            style: "thin",
            color: {
                rgb: "cccccc"
            }
        }
    }
};

var bodyTextStyle = {
    alignment: {
        vertical: "center"
    }
};
var bodyDecimalStyle = {
    numFmt: "#,##0.00"
};
var bodyCellStyle = exports.bodyCellStyle = {
    rowspan: bodyTextStyle,
    center: {
        alignment: (0, _extends3.default)({}, bodyTextStyle.alignment, {
            horizontal: "center"
        })
    },
    left: {
        alignment: (0, _extends3.default)({}, bodyTextStyle.alignment, {
            horizontal: "left"
        })
    },
    right: {
        alignment: (0, _extends3.default)({}, bodyTextStyle.alignment, {
            horizontal: "right"
        })
    },
    number: {
        numFmt: "#,##0"
    },
    decimal: bodyDecimalStyle,
    negativeDecimal: (0, _extends3.default)({}, bodyDecimalStyle, {
        font: {
            color: {
                rgb: "FF0000"
            }
        }
    }),
    percent: {
        numFmt: '#,##0.00%'
    }
};

var totalTextStyle = {
    font: {
        bold: true
    },
    fill: {
        patternType: "solid",
        fgColor: {
            rgb: "ffc4a8"
        }
    }
};
var totalDecimalStyle = (0, _extends3.default)({}, totalTextStyle, {
    numFmt: "#,##0.00"
});
var totalCellStyle = exports.totalCellStyle = {
    default: totalTextStyle,
    rowspan: (0, _extends3.default)({}, totalTextStyle, {
        alignment: {
            horizontal: "center",
            vertical: "center"
        }
    }),
    center: (0, _extends3.default)({}, totalTextStyle, {
        alignment: {
            vertical: "center",
            horizontal: "center"
        }
    }),
    right: (0, _extends3.default)({}, totalTextStyle, {
        alignment: {
            vertical: "center",
            horizontal: "right"
        }
    }),
    number: (0, _extends3.default)({}, totalTextStyle, {
        numFmt: "#,##0"
    }),
    decimal: totalDecimalStyle,
    negativeDecimal: (0, _extends3.default)({}, totalDecimalStyle, {
        font: {
            bold: true,
            color: {
                rgb: "FF0000"
            }
        }
    })
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/globalResource.js":
/*!**************************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/globalResource.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
var globalResource = exports.globalResource = {
    From: 'From',
    To: 'To',
    GetAll: 'Get All',
    Search: 'Search',
    All: 'All',
    Product: 'Product'
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/numberFormat.js":
/*!************************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/numberFormat.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.formatDecimal = formatDecimal;
exports.format = format;
exports.formatDecimalWithCurrencySymbol = formatDecimalWithCurrencySymbol;
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

function formatDecimalWithCurrencySymbol(value) {
    var currencySymbol = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : "$";

    return value < 0 ? "-" + currencySymbol + formatDecimal(Math.abs(value)) : "" + currencySymbol + formatDecimal(value);
}

function round(value, digits) {
    var sign = value < 0 ? -1 : 1;
    var absoluteValue = Math.abs(value);
    var rounded = Number(Math.round(absoluteValue + 'e' + digits) + 'e-' + digits);

    return sign * rounded;
}

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/store.js":
/*!*****************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/store.js ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.configureStore = undefined;

var _redux = __webpack_require__(/*! redux */ "./Areas/Portal/www/node_modules/redux/es/redux.js");

var _reduxThunk = __webpack_require__(/*! redux-thunk */ "./Areas/Portal/www/node_modules/redux-thunk/es/index.js");

var _reduxThunk2 = _interopRequireDefault(_reduxThunk);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var configureStore = exports.configureStore = function configureStore(initialState, reducer) {
    var enhancer = (0, _redux.compose)((0, _redux.applyMiddleware)(_reduxThunk2.default // lets us dispatch() functions
    ));

    return (0, _redux.createStore)(reducer, initialState, enhancer);
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/utils.js":
/*!*****************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/utils.js ***!
  \*****************************************************/
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

var formatUnit = exports.formatUnit = function formatUnit(value) {
    return value < 10 ? '0' + value : value;
};

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

/***/ "./Areas/Portal/www/source/js/pages/affiliate-info/actions/index.js":
/*!**************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate-info/actions/index.js ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.setParam = exports.setReportTitle = exports.setPagingOption = undefined;

var _types = __webpack_require__(/*! ./types */ "./Areas/Portal/www/source/js/pages/affiliate-info/actions/types.js");

var _pagingAction = __webpack_require__(/*! ../../../actions/pagingAction */ "./Areas/Portal/www/source/js/actions/pagingAction.js");

exports.setPagingOption = _pagingAction.setPagingOption;
var setReportTitle = exports.setReportTitle = function setReportTitle(data) {
    return {
        type: _types.SET_REPORT_TITLE,
        data: data
    };
};

var setParam = exports.setParam = function setParam(data) {
    return {
        type: _types.SET_PARAM,
        data: data
    };
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate-info/actions/types.js":
/*!**************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate-info/actions/types.js ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
var SET_REPORT_TITLE = exports.SET_REPORT_TITLE = "SET_REPORT_TITLE";
var SET_PARAM = exports.SET_PARAM = "SET_PARAM";

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate-info/components/AppContainer.jsx":
/*!*************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate-info/components/AppContainer.jsx ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.AppContainer = undefined;

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./Areas/Portal/www/node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./Areas/Portal/www/node_modules/react-redux/es/index.js");

var _AppLayout = __webpack_require__(/*! ../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");

var _AppLayout2 = _interopRequireDefault(_AppLayout);

var _Filter = __webpack_require__(/*! ./Filter */ "./Areas/Portal/www/source/js/pages/affiliate-info/components/Filter.jsx");

var _Filter2 = _interopRequireDefault(_Filter);

var _Content = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/affiliate-info/components/Content.jsx");

var _Content2 = _interopRequireDefault(_Content);

var _Loader = __webpack_require__(/*! ../../../components/Loader */ "./Areas/Portal/www/source/js/components/Loader.jsx");

var _Loader2 = _interopRequireDefault(_Loader);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var AppContainer = exports.AppContainer = function (_Component) {
    (0, _inherits3.default)(AppContainer, _Component);

    function AppContainer(props) {
        (0, _classCallCheck3.default)(this, AppContainer);
        return (0, _possibleConstructorReturn3.default)(this, (AppContainer.__proto__ || (0, _getPrototypeOf2.default)(AppContainer)).call(this, props));
    }

    (0, _createClass3.default)(AppContainer, [{
        key: 'render',
        value: function render() {
            return _react2.default.createElement(
                _AppLayout2.default,
                null,
                _react2.default.createElement(_Loader2.default, { show: this.props.isShowLoader }),
                _react2.default.createElement(_Filter2.default, null),
                _react2.default.createElement(_Content2.default, null)
            );
        }
    }]);
    return AppContainer;
}(_react.Component);

function mapStateToProps(_ref) {
    var isShowLoader = _ref.isShowLoader;

    return (0, _assign2.default)({}, { isShowLoader: isShowLoader });
}

exports.default = (0, _reactRedux.connect)(mapStateToProps)(AppContainer);

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate-info/components/Content.jsx":
/*!********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate-info/components/Content.jsx ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.Content = undefined;

var _regenerator = __webpack_require__(/*! babel-runtime/regenerator */ "./Areas/Portal/www/node_modules/babel-runtime/regenerator/index.js");

var _regenerator2 = _interopRequireDefault(_regenerator);

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _asyncToGenerator2 = __webpack_require__(/*! babel-runtime/helpers/asyncToGenerator */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/asyncToGenerator.js");

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./Areas/Portal/www/node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./Areas/Portal/www/node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./Areas/Portal/www/node_modules/redux/es/redux.js");

var _loaderAction = __webpack_require__(/*! ../../../actions/loaderAction */ "./Areas/Portal/www/source/js/actions/loaderAction.js");

var _ReportTitle = __webpack_require__(/*! ../../../components/ReportTitle */ "./Areas/Portal/www/source/js/components/ReportTitle.jsx");

var _ReportTitle2 = _interopRequireDefault(_ReportTitle);

var _GridData = __webpack_require__(/*! ./GridData */ "./Areas/Portal/www/source/js/pages/affiliate-info/components/GridData.jsx");

var _GridData2 = _interopRequireDefault(_GridData);

var _reportExcel = __webpack_require__(/*! ../modules/reportExcel */ "./Areas/Portal/www/source/js/pages/affiliate-info/modules/reportExcel.js");

var _reportExcel2 = _interopRequireDefault(_reportExcel);

var _ajaxRequest = __webpack_require__(/*! ../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var Content = exports.Content = function (_Component) {
    (0, _inherits3.default)(Content, _Component);

    function Content(props) {
        var _this2 = this;

        (0, _classCallCheck3.default)(this, Content);

        var _this = (0, _possibleConstructorReturn3.default)(this, (Content.__proto__ || (0, _getPrototypeOf2.default)(Content)).call(this, props));

        _this.handleExportExcel = (0, _asyncToGenerator3.default)( /*#__PURE__*/_regenerator2.default.mark(function _callee() {
            var _this$props, param, reportTitle, languageList, url, postData, response, reportExcel;

            return _regenerator2.default.wrap(function _callee$(_context) {
                while (1) {
                    switch (_context.prev = _context.next) {
                        case 0:
                            _this.props.toggleLoader(true);
                            _this$props = _this.props, param = _this$props.param, reportTitle = _this$props.reportTitle, languageList = _this$props.languageList;
                            url = 'Portal/AffiliateInfo/GetAffiliateList';
                            postData = (0, _assign2.default)({}, param, { IsExportExcel: true });
                            _context.next = 6;
                            return (0, _ajaxRequest.post)(url, postData);

                        case 6:
                            response = _context.sent;


                            if (response && response.status === 200) {
                                reportExcel = new _reportExcel2.default(response.data, reportTitle, languageList);

                                reportExcel.export();
                            }
                            _this.props.toggleLoader(false);

                        case 9:
                        case 'end':
                            return _context.stop();
                    }
                }
            }, _callee, _this2);
        }));
        return _this;
    }

    (0, _createClass3.default)(Content, [{
        key: 'shouldComponentUpdate',
        value: function shouldComponentUpdate(nextprops) {
            var reportTitle = this.props.reportTitle;


            return nextprops.reportTitle !== reportTitle;
        }
    }, {
        key: '_buildReportTitle',
        value: function _buildReportTitle(reportTitle) {
            return _react2.default.createElement(
                'div',
                { className: 'd-flex align-items-center' },
                reportTitle,
                _react2.default.createElement(
                    'button',
                    { type: 'button', className: 'ml-auto btn btn-link btn-excel', onClick: this.handleExportExcel, title: 'Export Excel' },
                    _react2.default.createElement('span', { className: 'oi icon-excel' })
                )
            );
        }
    }, {
        key: 'render',
        value: function render() {
            var reportTitle = this.props.reportTitle;


            return _react2.default.createElement(
                'div',
                { className: 'data-content my-2' },
                reportTitle && _react2.default.createElement(_ReportTitle2.default, { content: this._buildReportTitle(reportTitle) }),
                _react2.default.createElement(_GridData2.default, null)
            );
        }
    }]);
    return Content;
}(_react.Component);

function mapStateToProps(_ref2) {
    var reportTitle = _ref2.reportTitle,
        param = _ref2.param,
        languageList = _ref2.languageList;

    return (0, _assign2.default)({}, { reportTitle: reportTitle, param: param, languageList: languageList });
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ toggleLoader: _loaderAction.toggleLoader }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProps, mapDispatchToProps)(Content);

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate-info/components/Filter.jsx":
/*!*******************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate-info/components/Filter.jsx ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.Filter = undefined;

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _keys = __webpack_require__(/*! babel-runtime/core-js/object/keys */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/keys.js");

var _keys2 = _interopRequireDefault(_keys);

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./Areas/Portal/www/node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./Areas/Portal/www/node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./Areas/Portal/www/node_modules/redux/es/redux.js");

var _reactstrap = __webpack_require__(/*! reactstrap */ "./Areas/Portal/www/node_modules/reactstrap/dist/reactstrap.es.js");

var _constants = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/affiliate-info/modules/constants.js");

var _actions = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/affiliate-info/actions/index.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var Filter = exports.Filter = function (_Component) {
    (0, _inherits3.default)(Filter, _Component);

    function Filter(props) {
        (0, _classCallCheck3.default)(this, Filter);

        var _this = (0, _possibleConstructorReturn3.default)(this, (Filter.__proto__ || (0, _getPrototypeOf2.default)(Filter)).call(this, props));

        _this.handleChange = function (event) {
            _this.setState((0, _defineProperty3.default)({}, event.target.name, event.target.value));
        };

        _this.handleChangeSortAscending = function () {
            _this.setState(function (prevState) {
                return { isSortAscending: !prevState.isSortAscending };
            });
        };

        _this.handleClickSearch = function () {
            var _this$state = _this.state,
                selectedCommGroup = _this$state.selectedCommGroup,
                selectedLanguage = _this$state.selectedLanguage,
                selectedStatus = _this$state.selectedStatus,
                userName = _this$state.userName,
                email = _this$state.email,
                selectedSortBy = _this$state.selectedSortBy,
                isSortAscending = _this$state.isSortAscending;


            _this.props.setReportTitle(_this._buildReportTitle(selectedCommGroup, selectedLanguage, selectedStatus, userName, email));
            var param = {
                GroupId: selectedCommGroup,
                Language: selectedLanguage,
                Status: selectedStatus,
                UserName: userName,
                Email: email,
                SortBy: selectedSortBy,
                SortDirectionType: !isSortAscending
            };
            _this.props.setParam(param);
        };

        _this.state = {
            selectedCommGroup: _constants.defaultSearchBy,
            selectedLanguage: '',
            selectedStatus: '',
            userName: '',
            email: '',
            selectedSortBy: _constants.defaultSortBy,
            isSortAscending: true
        };
        return _this;
    }

    (0, _createClass3.default)(Filter, [{
        key: '_buildReportTitle',
        value: function _buildReportTitle(selectedCommGroup, selectedLanguage, selectedStatus, userName, email) {
            var _props = this.props,
                commGroupList = _props.commGroupList,
                languageList = _props.languageList;


            var space = _constants.resource.Space;
            var apostrophe = _constants.resource.Apostrophe;
            var commGroupTitle = commGroupList[selectedCommGroup] || _constants.resource.AllCommGroups;
            var languageTitle = languageList[selectedLanguage] || _constants.resource.AllLanguages;
            var statusTitle = _constants.statusList[selectedStatus] || _constants.resource.AllStatuses;

            var userNameTitle = userName && space + _constants.resource.UsernameContains + apostrophe + userName + apostrophe;
            var emailTitle = email && space + _constants.resource.EmailContains + apostrophe + email + apostrophe;
            return '' + _constants.resource.ReportName + space + commGroupTitle.toUpperCase() + space + languageTitle.toUpperCase() + space + statusTitle.toUpperCase() + userNameTitle + ' ' + emailTitle;
        }
    }, {
        key: 'render',
        value: function render() {
            var _state = this.state,
                selectedCommGroup = _state.selectedCommGroup,
                selectedLanguage = _state.selectedLanguage,
                selectedSortBy = _state.selectedSortBy,
                selectedStatus = _state.selectedStatus,
                isSortAscending = _state.isSortAscending;
            var _props2 = this.props,
                commGroupList = _props2.commGroupList,
                languageList = _props2.languageList;


            return _react2.default.createElement(
                'div',
                { className: 'filter-container' },
                _react2.default.createElement(
                    _reactstrap.Form,
                    { inline: true, tag: 'div' },
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(
                            _reactstrap.Label,
                            { 'for': 'SearchBy', className: 'mr-sm-2' },
                            _constants.resource.SearchBy
                        ),
                        _react2.default.createElement(
                            _reactstrap.Input,
                            { type: 'select', name: 'selectedCommGroup', id: 'SearchBy', value: selectedCommGroup, onChange: this.handleChange },
                            _react2.default.createElement(
                                'option',
                                { key: 'commGroup-all', value: '' },
                                _constants.resource.AllCommGroups
                            ),
                            (0, _keys2.default)(commGroupList).map(function (commGroup) {
                                return _react2.default.createElement(
                                    'option',
                                    { key: 'commGroup-' + commGroup, value: commGroup },
                                    commGroupList[commGroup]
                                );
                            })
                        )
                    ),
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(
                            _reactstrap.Input,
                            { type: 'select', name: 'selectedLanguage', id: 'Language', value: selectedLanguage, onChange: this.handleChange },
                            _react2.default.createElement(
                                'option',
                                { key: 'lang-all', value: '' },
                                _constants.resource.AllLanguages
                            ),
                            (0, _keys2.default)(languageList).map(function (lang) {
                                return _react2.default.createElement(
                                    'option',
                                    { key: 'lang-' + lang, value: lang },
                                    languageList[lang]
                                );
                            })
                        )
                    ),
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(
                            _reactstrap.Input,
                            { type: 'select', name: 'selectedStatus', id: 'Status', value: selectedStatus, onChange: this.handleChange },
                            _react2.default.createElement(
                                'option',
                                { key: 'status-all', value: '' },
                                _constants.resource.AllStatuses
                            ),
                            (0, _keys2.default)(_constants.statusList).map(function (status) {
                                return _react2.default.createElement(
                                    'option',
                                    { key: 'status-' + status, value: status },
                                    _constants.statusList[status]
                                );
                            })
                        )
                    ),
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(_reactstrap.Input, { type: 'text', name: 'userName', id: 'Username', placeholder: 'Username', onChange: this.handleChange })
                    ),
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(_reactstrap.Input, { type: 'email', name: 'email', id: 'Email', placeholder: 'Email', onChange: this.handleChange })
                    ),
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(
                            _reactstrap.Label,
                            { 'for': 'SortBy', className: 'mr-sm-2' },
                            _constants.resource.SortBy
                        ),
                        _react2.default.createElement(
                            _reactstrap.Input,
                            { type: 'select', name: 'selectedSortBy', id: 'SortBy', value: selectedSortBy, onChange: this.handleChange },
                            (0, _keys2.default)(_constants.sortByList).map(function (sortBy) {
                                return _react2.default.createElement(
                                    'option',
                                    { key: 'sortBy-' + sortBy, value: _constants.sortByList[sortBy] },
                                    sortBy
                                );
                            })
                        )
                    ),
                    _react2.default.createElement(
                        _reactstrap.FormGroup,
                        { className: 'mb-2 mr-sm-2 mb-sm-0' },
                        _react2.default.createElement(
                            _reactstrap.Label,
                            { check: true },
                            _constants.resource.AZ,
                            _react2.default.createElement(_reactstrap.Input, { className: 'ml-1', type: 'checkbox', checked: isSortAscending, onChange: this.handleChangeSortAscending })
                        )
                    ),
                    _react2.default.createElement(
                        _reactstrap.Button,
                        { outline: true, color: 'primary', type: 'button', onClick: this.handleClickSearch },
                        _react2.default.createElement('span', { className: 'oi oi-magnifying-glass' }),
                        ' ',
                        _constants.resource.Search
                    )
                )
            );
        }
    }]);
    return Filter;
}(_react.Component);

function mapStateToProps(_ref) {
    var languageList = _ref.languageList,
        commGroupList = _ref.commGroupList,
        param = _ref.param;

    return (0, _assign2.default)({}, { languageList: languageList, commGroupList: commGroupList, param: param });
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ setReportTitle: _actions.setReportTitle, setParam: _actions.setParam }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProps, mapDispatchToProps)(Filter);

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate-info/components/GridData.jsx":
/*!*********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate-info/components/GridData.jsx ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.GridData = undefined;

var _regenerator = __webpack_require__(/*! babel-runtime/regenerator */ "./Areas/Portal/www/node_modules/babel-runtime/regenerator/index.js");

var _regenerator2 = _interopRequireDefault(_regenerator);

var _asyncToGenerator2 = __webpack_require__(/*! babel-runtime/helpers/asyncToGenerator */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/asyncToGenerator.js");

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _react = __webpack_require__(/*! react */ "./Areas/Portal/www/node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./Areas/Portal/www/node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./Areas/Portal/www/node_modules/redux/es/redux.js");

var _reactFixedTableHeader = __webpack_require__(/*! @9softstudio/react-fixed-table-header */ "./Areas/Portal/www/node_modules/@9softstudio/react-fixed-table-header/dist/index.js");

var _constants = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/affiliate-info/modules/constants.js");

var _actions = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/affiliate-info/actions/index.js");

var _loaderAction = __webpack_require__(/*! ../../../actions/loaderAction */ "./Areas/Portal/www/source/js/actions/loaderAction.js");

var _ajaxRequest = __webpack_require__(/*! ../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");

var _numberFormat = __webpack_require__(/*! ../../../modules/numberFormat */ "./Areas/Portal/www/source/js/modules/numberFormat.js");

var _dataHandler = __webpack_require__(/*! ../../../modules/dataHandler */ "./Areas/Portal/www/source/js/modules/dataHandler.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var GridData = exports.GridData = function (_Component) {
    (0, _inherits3.default)(GridData, _Component);

    function GridData(props) {
        (0, _classCallCheck3.default)(this, GridData);

        var _this = (0, _possibleConstructorReturn3.default)(this, (GridData.__proto__ || (0, _getPrototypeOf2.default)(GridData)).call(this, props));

        _this.handleToggleFavorite = function (affId, isFavorite) {
            var param = {
                AffId: affId,
                IsFavorite: !isFavorite
            };

            _this.saveToggleFavorite(param);
        };

        _this.handlePaging = function (index, size) {
            var newPaging = {
                PageIndex: index,
                PageSize: size
            };
            _this.props.setPagingOption(newPaging);

            var newParam = (0, _assign2.default)({}, _this.props.param, newPaging);
            _this.fetchData(newParam);
        };

        _this.state = {
            gridData: null
        };
        return _this;
    }

    (0, _createClass3.default)(GridData, [{
        key: 'componentDidUpdate',
        value: function componentDidUpdate(prevProps) {
            var param = this.props.param;


            if (param !== prevProps.param) {
                var pagingOption = this.props.pagingOption;

                var updateParam = (0, _assign2.default)({}, param, { PageIndex: 1, PageSize: pagingOption.PageSize });

                this.fetchData(updateParam);
            }
        }
    }, {
        key: 'fetchData',
        value: function () {
            var _ref = (0, _asyncToGenerator3.default)( /*#__PURE__*/_regenerator2.default.mark(function _callee(param) {
                var url, response, _response$data, MainData, TotalItem;

                return _regenerator2.default.wrap(function _callee$(_context) {
                    while (1) {
                        switch (_context.prev = _context.next) {
                            case 0:
                                this.props.toggleLoader(true);
                                url = 'Portal/AffiliateInfo/GetAffiliateList';
                                _context.next = 4;
                                return (0, _ajaxRequest.post)(url, param);

                            case 4:
                                response = _context.sent;


                                if (response && response.status === 200) {
                                    _response$data = response.data, MainData = _response$data.MainData, TotalItem = _response$data.TotalItem;


                                    this.setState({ gridData: MainData });

                                    this.props.setPagingOption({ TotalItem: TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
                                }
                                this.props.toggleLoader(false);

                            case 7:
                            case 'end':
                                return _context.stop();
                        }
                    }
                }, _callee, this);
            }));

            function fetchData(_x) {
                return _ref.apply(this, arguments);
            }

            return fetchData;
        }()
    }, {
        key: '_renderHeader',
        value: function _renderHeader() {
            return _react2.default.createElement(
                _reactFixedTableHeader.Row,
                { key: 'theard' },
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 80, title: _constants.headerText.Index },
                    _constants.headerText.Index
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 300, title: _constants.headerText.UserName },
                    _constants.headerText.UserName
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 200, title: _constants.headerText.Status },
                    _constants.headerText.Status
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 220, title: _constants.headerText.FirstName },
                    _constants.headerText.FirstName
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 300, title: _constants.headerText.LastName },
                    _constants.headerText.LastName
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 300, title: _constants.headerText.Language },
                    _constants.headerText.Language
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 450, title: _constants.headerText.Email },
                    _constants.headerText.Email
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 300, title: _constants.headerText.RegistrationDateText },
                    _constants.headerText.RegistrationDateText
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 300, title: _constants.headerText.ApprovalDateText },
                    _constants.headerText.ApprovalDateText
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 300, title: _constants.headerText.CasinoDateText },
                    _constants.headerText.CasinoDateText
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 300, title: _constants.headerText.CommissionGroup },
                    _constants.headerText.CommissionGroup
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 200, title: _constants.headerText.BalanceBf },
                    _constants.headerText.BalanceBf
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 150, title: _constants.headerText.Member },
                    _constants.headerText.Member
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { header: true, colWidth: 130, title: _constants.headerText.Password },
                    _constants.headerText.Password
                )
            );
        }
    }, {
        key: '_renderDataSection',
        value: function _renderDataSection(dataList) {
            var dataRows = [];
            var pagingOption = this.props.pagingOption;

            var startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (var i = 0; i < dataList.length; i++) {
                var dataItem = dataList[i];

                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
            }

            return dataRows;
        }
    }, {
        key: '_buildDataRow',
        value: function _buildDataRow(dataItem, index) {
            var _this2 = this;

            var languageList = this.props.languageList;

            var statusToLower = dataItem.Status && dataItem.Status.toLowerCase();
            var isApproved = statusToLower == "approved";

            var balanceBf = dataItem.BalanceBf ? (0, _numberFormat.formatDecimal)(dataItem.BalanceBf) : _constants.resource.NA;
            var member = dataItem.Member ? (0, _numberFormat.format)(dataItem.Member) : _constants.resource.NA;
            var language = languageList[dataItem.Language];
            var commissionGroup = dataItem.CommissionGroup ? dataItem.CommissionGroup : _constants.resource.NA;

            var isFavorite = dataItem.IsFavorite;
            var checkFavorite = isFavorite ? "favor-check" : "favor-uncheck";
            var classNameFavorite = isApproved || statusToLower == "closed" ? checkFavorite : "";

            return _react2.default.createElement(
                _reactFixedTableHeader.Row,
                { key: 'dataRow-' + index },
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { className: 'text-center', title: index },
                    index
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { title: dataItem.UserName },
                    dataItem.UserName,
                    ' ',
                    _react2.default.createElement('span', { className: classNameFavorite, onClick: function onClick() {
                            return _this2.handleToggleFavorite(dataItem.AffId, isFavorite);
                        } })
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { className: 'text-center', title: dataItem.Status },
                    dataItem.Status
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { title: dataItem.FirstName },
                    dataItem.FirstName
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { title: dataItem.LastName },
                    dataItem.LastName
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { title: language },
                    language
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { title: dataItem.Email },
                    dataItem.Email
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { className: 'text-center', title: dataItem.RegistrationDateText },
                    dataItem.RegistrationDateText
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { className: 'text-center', title: dataItem.ApprovalDateText },
                    dataItem.ApprovalDateText
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { className: 'text-center', title: dataItem.CasinoDateText },
                    dataItem.CasinoDateText
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { className: 'text-center', title: commissionGroup },
                    commissionGroup
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { className: 'text-right', title: balanceBf },
                    balanceBf
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { className: 'text-right', title: member },
                    member
                ),
                _react2.default.createElement(
                    _reactFixedTableHeader.Cell,
                    { className: 'text-center', title: 'Change Password' },
                    isApproved && _react2.default.createElement(
                        'button',
                        { className: 'btn btn-link' },
                        _react2.default.createElement('span', { className: 'oi oi-pencil text-secondary' })
                    )
                )
            );
        }
    }, {
        key: 'saveToggleFavorite',
        value: function () {
            var _ref2 = (0, _asyncToGenerator3.default)( /*#__PURE__*/_regenerator2.default.mark(function _callee2(param) {
                var url, response, gridData, indexItem, updateItem, gridDataUpdate;
                return _regenerator2.default.wrap(function _callee2$(_context2) {
                    while (1) {
                        switch (_context2.prev = _context2.next) {
                            case 0:
                                url = 'Portal/AffiliateInfo/ToggleFavorite';
                                _context2.next = 3;
                                return (0, _ajaxRequest.post)(url, param);

                            case 3:
                                response = _context2.sent;


                                if (response) {
                                    if (response.data.IsSuccessful) {
                                        gridData = this.state.gridData;
                                        indexItem = gridData.findIndex(function (item) {
                                            return item.AffId === param.AffId;
                                        });
                                        updateItem = (0, _assign2.default)({}, gridData[indexItem], { IsFavorite: param.IsFavorite });
                                        gridDataUpdate = (0, _dataHandler.updateObjectInArray)(gridData, indexItem, updateItem);


                                        this.setState({ gridData: gridDataUpdate });
                                    } else {
                                        alert(response.data.Message);
                                    }
                                }

                            case 5:
                            case 'end':
                                return _context2.stop();
                        }
                    }
                }, _callee2, this);
            }));

            function saveToggleFavorite(_x2) {
                return _ref2.apply(this, arguments);
            }

            return saveToggleFavorite;
        }()
    }, {
        key: 'render',
        value: function render() {
            var gridData = this.state.gridData;


            return gridData && _react2.default.createElement(
                'div',
                { className: 'table-responsive table-striped' },
                _react2.default.createElement(_reactFixedTableHeader.Table, {
                    minWidth: 1000,
                    autoWidth: true,
                    bodyHeight: 300,
                    header: this._renderHeader(),
                    body: this._renderDataSection(gridData),
                    containerPadding: 20,
                    isPaging: true,
                    pageOption: this.props.pagingOption,
                    onPaging: this.handlePaging
                })
            );
        }
    }]);
    return GridData;
}(_react.Component);

function mapStateToProps(_ref3) {
    var pagingOption = _ref3.pagingOption,
        languageList = _ref3.languageList,
        param = _ref3.param;

    return (0, _assign2.default)({}, { pagingOption: pagingOption, languageList: languageList, param: param });
}

function mapDispatchToProps(dispatch) {
    return (0, _redux.bindActionCreators)({ setPagingOption: _actions.setPagingOption, toggleLoader: _loaderAction.toggleLoader }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProps, mapDispatchToProps)(GridData);

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate-info/index.jsx":
/*!*******************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate-info/index.jsx ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _getPrototypeOf = __webpack_require__(/*! babel-runtime/core-js/object/get-prototype-of */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/get-prototype-of.js");

var _getPrototypeOf2 = _interopRequireDefault(_getPrototypeOf);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _possibleConstructorReturn2 = __webpack_require__(/*! babel-runtime/helpers/possibleConstructorReturn */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/possibleConstructorReturn.js");

var _possibleConstructorReturn3 = _interopRequireDefault(_possibleConstructorReturn2);

var _inherits2 = __webpack_require__(/*! babel-runtime/helpers/inherits */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/inherits.js");

var _inherits3 = _interopRequireDefault(_inherits2);

var _toConsumableArray2 = __webpack_require__(/*! babel-runtime/helpers/toConsumableArray */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/toConsumableArray.js");

var _toConsumableArray3 = _interopRequireDefault(_toConsumableArray2);

var _react = __webpack_require__(/*! react */ "./Areas/Portal/www/node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactDom = __webpack_require__(/*! react-dom */ "./Areas/Portal/www/node_modules/react-dom/index.js");

var _reactDom2 = _interopRequireDefault(_reactDom);

var _reactRedux = __webpack_require__(/*! react-redux */ "./Areas/Portal/www/node_modules/react-redux/es/index.js");

var _reducers = __webpack_require__(/*! ./reducers */ "./Areas/Portal/www/source/js/pages/affiliate-info/reducers/index.js");

var _reducers2 = _interopRequireDefault(_reducers);

var _AppContainer = __webpack_require__(/*! ./components/AppContainer */ "./Areas/Portal/www/source/js/pages/affiliate-info/components/AppContainer.jsx");

var _AppContainer2 = _interopRequireDefault(_AppContainer);

var _store = __webpack_require__(/*! ../../modules/store */ "./Areas/Portal/www/source/js/modules/store.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var commGroupList = [].concat((0, _toConsumableArray3.default)(window.initialState.commGroupList));
var commGroup = {};
for (var i = 0; i < commGroupList.length; i++) {
    var commGroupItem = commGroupList[i];
    commGroup[commGroupItem.Id] = commGroupItem.Name;
}

var languageList = [].concat((0, _toConsumableArray3.default)(window.initialState.languageList));
var language = {};
for (var _i = 0; _i < languageList.length; _i++) {
    var languageItem = languageList[_i];
    language[languageItem.LanguageCode] = languageItem.LanguageName;
}

var store = (0, _store.configureStore)({ languageList: language, commGroupList: commGroup }, _reducers2.default);

var App = function (_Component) {
    (0, _inherits3.default)(App, _Component);

    function App(props) {
        (0, _classCallCheck3.default)(this, App);
        return (0, _possibleConstructorReturn3.default)(this, (App.__proto__ || (0, _getPrototypeOf2.default)(App)).call(this, props));
    }

    (0, _createClass3.default)(App, [{
        key: 'render',
        value: function render() {
            return _react2.default.createElement(
                _reactRedux.Provider,
                { store: store },
                _react2.default.createElement(_AppContainer2.default, null)
            );
        }
    }]);
    return App;
}(_react.Component);

exports.default = App;


_reactDom2.default.render(_react2.default.createElement(App, null), document.getElementById("root"));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate-info/modules/constants.js":
/*!******************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate-info/modules/constants.js ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.resource = exports.headerText = exports.sortByList = exports.defaultSortBy = exports.defaultSearchBy = exports.statusList = undefined;

var _globalResource = __webpack_require__(/*! ../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");

var statusList = exports.statusList = {
    Favorite: "Favorite",
    Pending: "Pending",
    Approved: "Approved",
    Disapproved: "Disapproved",
    Closed: "Closed"
};

var defaultSearchBy = exports.defaultSearchBy = 0;
var defaultSortBy = exports.defaultSortBy = 1;

var sortByList = exports.sortByList = {
    Username: 1,
    RegistrationDate: 2,
    ApprovalDate: 3,
    BalanceBF: 4,
    Member: 5
};

var headerText = exports.headerText = {
    Index: '#',
    UserName: 'Username',
    Status: 'Status',
    FirstName: 'First Name',
    LastName: 'Last Name',
    Language: 'Language',
    Email: 'Email',
    RegistrationDateText: 'Registration Date',
    ApprovalDateText: 'Approval Date',
    CasinoDateText: 'Casino Date',
    CommissionGroup: 'Commission Group',
    BalanceBf: 'Balance B/F',
    Member: 'Member',
    Password: 'Password'
};

var resource = exports.resource = {
    Search: _globalResource.globalResource.Search,
    Space: ' - ',
    ReportName: 'AFFILIATE REPORT',
    AllCommGroups: 'All Comm Groups',
    AllLanguages: 'All Languages',
    AllStatuses: 'All Statuses',
    UsernameContains: 'USERNAME CONTAINS ',
    EmailContains: 'EMAIL CONTAINS ',
    SearchBy: 'Search By',
    SortBy: 'Sort By',
    AZ: 'A-Z',
    Apostrophe: "'",
    NA: 'N/A'
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate-info/modules/reportExcel.js":
/*!********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate-info/modules/reportExcel.js ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _classCallCheck2 = __webpack_require__(/*! babel-runtime/helpers/classCallCheck */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/classCallCheck.js");

var _classCallCheck3 = _interopRequireDefault(_classCallCheck2);

var _createClass2 = __webpack_require__(/*! babel-runtime/helpers/createClass */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/createClass.js");

var _createClass3 = _interopRequireDefault(_createClass2);

var _excel = __webpack_require__(/*! ../../../modules/excel */ "./Areas/Portal/www/source/js/modules/excel.js");

var _excel2 = _interopRequireDefault(_excel);

var _excelStyleConstant = __webpack_require__(/*! ../../../modules/excelStyleConstant */ "./Areas/Portal/www/source/js/modules/excelStyleConstant.js");

var _constants = __webpack_require__(/*! ./constants */ "./Areas/Portal/www/source/js/pages/affiliate-info/modules/constants.js");

var _numberFormat = __webpack_require__(/*! ../../../modules/numberFormat */ "./Areas/Portal/www/source/js/modules/numberFormat.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var REPORT_TITLE_ROW_INDEX = 0;
var HEADER_ROW_INDEX = 1;
var DATA_ROW_INDEX = 2;

var TOTAL_COLUMN = 13;

var MAIN_SECTION_STYLE = 'mainSectionStyle';

var fields = {
    Index: 'Index',
    UserName: 'UserName',
    Status: 'Status',
    FirstName: 'FirstName',
    LastName: 'LastName',
    Language: 'Language',
    Email: 'Email',
    CommissionGroup: 'CommissionGroup',
    BalanceBf: 'BalanceBf',
    Member: 'Member',
    RegistrationDateText: 'RegistrationDateText',
    ApprovalDateText: 'ApprovalDateText',
    CasinoDateText: 'CasinoDateText'
};

var ReportExcel = function () {
    function ReportExcel(data, title, languageList) {
        (0, _classCallCheck3.default)(this, ReportExcel);

        this.title = title;
        this.data = data;
        this.languageList = languageList;

        this.worksheet = {};
        this.currentRowIndex = DATA_ROW_INDEX;
        this.mergeConfig = [];
        this.columnConfigs = this._getcolumnConfigs();
    }

    (0, _createClass3.default)(ReportExcel, [{
        key: 'export',
        value: function _export() {
            var _this = this;

            _excel2.default.process(function () {
                _this._createReportTitle();
                _this._createHeader();
                _this._createBodyData();
                _this._createExcelConfig();

                _excel2.default.export(_this.worksheet, _this.title);
            });
        }
    }, {
        key: '_createReportTitle',
        value: function _createReportTitle() {
            this.mergeConfig.push({
                s: {
                    r: REPORT_TITLE_ROW_INDEX,
                    c: 0
                },
                e: {
                    r: REPORT_TITLE_ROW_INDEX,
                    c: TOTAL_COLUMN - 1
                }
            });
            var address = _excel2.default.getCellAddress({
                c: 0,
                r: REPORT_TITLE_ROW_INDEX
            });
            this.worksheet[address] = {
                t: "s",
                v: this.title,
                s: _excelStyleConstant.titleCellStyle
            };
        }
    }, {
        key: '_createHeader',
        value: function _createHeader() {
            for (var i = 0; i < TOTAL_COLUMN; i++) {
                var fieldName = this.columnConfigs[i].field;
                var address = _excel2.default.getCellAddress({
                    c: i,
                    r: HEADER_ROW_INDEX
                });
                this.worksheet[address] = {
                    t: "s",
                    v: _constants.headerText[fieldName],
                    s: _excelStyleConstant.headerCellStyle
                };
            }
        }
    }, {
        key: '_createBodyData',
        value: function _createBodyData() {
            var mainData = this.data.MainData;
            for (var i = 0; i < mainData.length; i++) {
                var item = mainData[i];

                for (var j = 0; j < TOTAL_COLUMN; j++) {
                    var address = _excel2.default.getCellAddress({
                        c: j,
                        r: this.currentRowIndex
                    });

                    this.worksheet[address] = this._buildDataRow(item, MAIN_SECTION_STYLE, i, j);
                }

                this.currentRowIndex++;
            }
        }
    }, {
        key: '_buildDataRow',
        value: function _buildDataRow(item, sectionStyleName, rowIndex, colIndex) {
            var columnConfig = this.columnConfigs[colIndex];
            var fieldName = columnConfig.field;
            var value = fieldName === fields.Index ? rowIndex + 1 : item[fieldName];

            if (fieldName === fields.BalanceBf) {
                value = value ? (0, _numberFormat.formatDecimal)(value) : _constants.resource.NA;
            } else if (fieldName === fields.Member) {
                value = value ? (0, _numberFormat.format)(value) : _constants.resource.NA;
            } else if (fieldName === fields.Language) {
                value = this.languageList[value] || '';
            }

            return {
                t: _excel2.default.getCellType(value),
                v: value,
                s: columnConfig[sectionStyleName]
            };
        }
    }, {
        key: '_createExcelConfig',
        value: function _createExcelConfig() {
            var cellStart = _excel2.default.getCellAddress({
                r: 0,
                c: 0
            });
            var cellEnd = _excel2.default.getCellAddress({
                r: this.currentRowIndex,
                c: TOTAL_COLUMN
            });

            this.worksheet["!merges"] = this.mergeConfig;
            this.worksheet["!ref"] = cellStart + ':' + cellEnd;
            this.worksheet['!cols'] = [{
                wpx: 70
            }, {
                wpx: 200
            }, {
                wpx: 150
            }, {
                wpx: 170
            }, {
                wpx: 170
            }, {
                wpx: 150
            }, {
                wpx: 250
            }, {
                wpx: 200
            }, {
                wpx: 200
            }, {
                wpx: 200
            }, {
                wpx: 250
            }, {
                wpx: 200
            }, {
                wpx: 200
            }];
        }
    }, {
        key: '_getcolumnConfigs',
        value: function _getcolumnConfigs() {
            return [(0, _defineProperty3.default)({
                field: fields.Index
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.center), (0, _defineProperty3.default)({
                field: fields.UserName
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.left), (0, _defineProperty3.default)({
                field: fields.Status
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.center), (0, _defineProperty3.default)({
                field: fields.FirstName
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.left), (0, _defineProperty3.default)({
                field: fields.LastName
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.left), (0, _defineProperty3.default)({
                field: fields.Language
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.left), (0, _defineProperty3.default)({
                field: fields.Email
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.left), (0, _defineProperty3.default)({
                field: fields.RegistrationDateText
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.center), (0, _defineProperty3.default)({
                field: fields.ApprovalDateText
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.center), (0, _defineProperty3.default)({
                field: fields.CasinoDateText
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.center), (0, _defineProperty3.default)({
                field: fields.CommissionGroup
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.center), (0, _defineProperty3.default)({
                field: fields.BalanceBf
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.right), (0, _defineProperty3.default)({
                field: fields.Member
            }, MAIN_SECTION_STYLE, _excelStyleConstant.bodyCellStyle.right)];
        }
    }]);
    return ReportExcel;
}();

exports.default = ReportExcel;

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate-info/reducers/index.js":
/*!***************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate-info/reducers/index.js ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _redux = __webpack_require__(/*! redux */ "./Areas/Portal/www/node_modules/redux/es/redux.js");

var _createReducer3 = __webpack_require__(/*! ../../../modules/createReducer */ "./Areas/Portal/www/source/js/modules/createReducer.js");

var _createReducer4 = _interopRequireDefault(_createReducer3);

var _types = __webpack_require__(/*! ../actions/types */ "./Areas/Portal/www/source/js/pages/affiliate-info/actions/types.js");

var _pagingReducer = __webpack_require__(/*! ../../../reducers/pagingReducer */ "./Areas/Portal/www/source/js/reducers/pagingReducer.js");

var _loaderReducer = __webpack_require__(/*! ../../../reducers/loaderReducer */ "./Areas/Portal/www/source/js/reducers/loaderReducer.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var commGroupList = (0, _createReducer4.default)({}, {});

var languageList = (0, _createReducer4.default)({}, {});

var reportTitle = (0, _createReducer4.default)('', (0, _defineProperty3.default)({}, _types.SET_REPORT_TITLE, function (prevState, action) {
    return action.data;
}));

var param = (0, _createReducer4.default)(null, (0, _defineProperty3.default)({}, _types.SET_PARAM, function (prevState, action) {
    return (0, _assign2.default)({}, prevState, action.data);
}));

exports.default = (0, _redux.combineReducers)({
    pagingOption: _pagingReducer.pagingOption,
    reportTitle: reportTitle,
    param: param,
    commGroupList: commGroupList,
    languageList: languageList,
    isShowLoader: _loaderReducer.isShowLoader
});

/***/ }),

/***/ "./Areas/Portal/www/source/js/reducers/loaderReducer.js":
/*!**************************************************************!*\
  !*** ./Areas/Portal/www/source/js/reducers/loaderReducer.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.isShowLoader = undefined;

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _createReducer2 = __webpack_require__(/*! ../modules/createReducer */ "./Areas/Portal/www/source/js/modules/createReducer.js");

var _createReducer3 = _interopRequireDefault(_createReducer2);

var _actionTypes = __webpack_require__(/*! ../actions/actionTypes */ "./Areas/Portal/www/source/js/actions/actionTypes.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var isShowLoader = exports.isShowLoader = (0, _createReducer3.default)(false, (0, _defineProperty3.default)({}, _actionTypes.TOGGLE_LOADER, function (prevState, action) {
    return action.data;
}));

/***/ }),

/***/ "./Areas/Portal/www/source/js/reducers/pagingReducer.js":
/*!**************************************************************!*\
  !*** ./Areas/Portal/www/source/js/reducers/pagingReducer.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.pagingOption = undefined;

var _defineProperty2 = __webpack_require__(/*! babel-runtime/helpers/defineProperty */ "./Areas/Portal/www/node_modules/babel-runtime/helpers/defineProperty.js");

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _assign = __webpack_require__(/*! babel-runtime/core-js/object/assign */ "./Areas/Portal/www/node_modules/babel-runtime/core-js/object/assign.js");

var _assign2 = _interopRequireDefault(_assign);

var _createReducer2 = __webpack_require__(/*! ../modules/createReducer */ "./Areas/Portal/www/source/js/modules/createReducer.js");

var _createReducer3 = _interopRequireDefault(_createReducer2);

var _actionTypes = __webpack_require__(/*! ../actions/actionTypes */ "./Areas/Portal/www/source/js/actions/actionTypes.js");

var _constants = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var pagingOption = exports.pagingOption = (0, _createReducer3.default)((0, _assign2.default)({}, _constants.defaultPagingOption), (0, _defineProperty3.default)({}, _actionTypes.SET_PAGING_OPTION, function (prevState, action) {
    return (0, _assign2.default)({}, prevState, action.data);
}));

/***/ }),

/***/ "./cptable":
/*!**************************!*\
  !*** external "cptable" ***!
  \**************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = cptable;

/***/ }),

/***/ "./jszip":
/*!************************!*\
  !*** external "jszip" ***!
  \************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = jszip;

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

/***/ "./ods":
/*!**********************!*\
  !*** external "ods" ***!
  \**********************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ods;

/***/ }),

/***/ "dll-reference moment_bb523aa7e14eeaff8f10":
/*!**********************************************!*\
  !*** external "moment_bb523aa7e14eeaff8f10" ***!
  \**********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = moment_bb523aa7e14eeaff8f10;

/***/ }),

/***/ "dll-reference react_bb523aa7e14eeaff8f10":
/*!*********************************************!*\
  !*** external "react_bb523aa7e14eeaff8f10" ***!
  \*********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = react_bb523aa7e14eeaff8f10;

/***/ })

/******/ });
//# sourceMappingURL=affiliate-info.js.map