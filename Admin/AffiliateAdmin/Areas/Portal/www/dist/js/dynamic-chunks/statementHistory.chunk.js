(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["statementHistory"],{

/***/ "./Areas/Portal/www/source/js/modules/numberFormat.js":
/*!************************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/numberFormat.js ***!
  \************************************************************/
/*! exports provided: formatDecimal, format, formatDecimalWithCurrencySymbol, round */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "formatDecimal", function() { return formatDecimal; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "format", function() { return format; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "formatDecimalWithCurrencySymbol", function() { return formatDecimalWithCurrencySymbol; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "round", function() { return round; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0__);

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
  var _context, _context2;

  var currencySymbol = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : "$";
  return value < 0 ? _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context = "-".concat(currencySymbol)).call(_context, formatDecimal(Math.abs(value))) : _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context2 = "".concat(currencySymbol)).call(_context2, formatDecimal(value));
}
function round(value, digits) {
  var sign = value < 0 ? -1 : 1;
  var absoluteValue = Math.abs(value);
  var rounded = Number(Math.round(absoluteValue + 'e' + digits) + 'e-' + digits);
  return sign * rounded;
}

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/common/actions/paymentRequestUpdate.js":
/*!*********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/common/actions/paymentRequestUpdate.js ***!
  \*********************************************************************************/
/*! exports provided: getPaymentRequestDetail, updatePaymentRequest */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "getPaymentRequestDetail", function() { return getPaymentRequestDetail; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "updatePaymentRequest", function() { return updatePaymentRequest; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");



var getPaymentRequestDetail = function getPaymentRequestDetail(param) {
  return (
    /*#__PURE__*/
    _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
    /*#__PURE__*/
    _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee() {
      var url, response;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
        while (1) {
          switch (_context.prev = _context.next) {
            case 0:
              url = 'Portal/PaymentRequest/GetPaymentRequestDetail';
              _context.next = 3;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

            case 3:
              response = _context.sent;
              return _context.abrupt("return", response && response.status === 200 ? response.data : null);

            case 5:
            case "end":
              return _context.stop();
          }
        }
      }, _callee);
    }))
  );
};
var updatePaymentRequest = function updatePaymentRequest(param) {
  return (
    /*#__PURE__*/
    _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
    /*#__PURE__*/
    _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee2() {
      var url, response;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee2$(_context2) {
        while (1) {
          switch (_context2.prev = _context2.next) {
            case 0:
              url = 'Portal/PaymentRequest/UpdatePaymentRequest';
              _context2.next = 3;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

            case 3:
              response = _context2.sent;
              return _context2.abrupt("return", response && response.status === 200 ? response.data : null);

            case 5:
            case "end":
              return _context2.stop();
          }
        }
      }, _callee2);
    }))
  );
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/common/components/PaymentRequestUpdateForm.jsx":
/*!*****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/common/components/PaymentRequestUpdateForm.jsx ***!
  \*****************************************************************************************/
/*! exports provided: PaymentRequestUpdateForm, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PaymentRequestUpdateForm", function() { return PaymentRequestUpdateForm; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../modules/constants/paymentRequestUpdate */ "./Areas/Portal/www/source/js/pages/common/modules/constants/paymentRequestUpdate.js");
/* harmony import */ var _modules_numberFormat__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../../../modules/numberFormat */ "./Areas/Portal/www/source/js/modules/numberFormat.js");
/* harmony import */ var _actions_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../actions/paymentRequestUpdate */ "./Areas/Portal/www/source/js/pages/common/actions/paymentRequestUpdate.js");

















var pendingStatus = _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["pending"].toLowerCase();
var approvedStatus = _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["statusList"].Approved.toLowerCase();
var rejectedStatus = _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["statusList"].Rejected.toLowerCase();
var PaymentRequestUpdateForm =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default()(PaymentRequestUpdateForm, _PureComponent);

  function PaymentRequestUpdateForm(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3___default()(this, PaymentRequestUpdateForm);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default()(PaymentRequestUpdateForm).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleChange", function (event) {
      _this.setState({
        paymentRequestInfo: _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, _this.state.paymentRequestInfo, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, event.target.name, event.target.value))
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleGetPaymentRequestInfo",
    /*#__PURE__*/
    function () {
      var _ref = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee(transId) {
        var data;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                _context.next = 2;
                return _this.props.getPaymentRequestDetail({
                  transId: transId
                });

              case 2:
                data = _context.sent;
                data && _this.setState({
                  paymentRequestInfo: data,
                  originalStatus: data.Status
                });

              case 4:
              case "end":
                return _context.stop();
            }
          }
        }, _callee);
      }));

      return function (_x) {
        return _ref.apply(this, arguments);
      };
    }());

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleSave",
    /*#__PURE__*/
    _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
    /*#__PURE__*/
    _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee2() {
      var _this$state, paymentRequestInfo, originalStatus, selectedStatus, paymentInfo, result;

      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee2$(_context2) {
        while (1) {
          switch (_context2.prev = _context2.next) {
            case 0:
              _this$state = _this.state, paymentRequestInfo = _this$state.paymentRequestInfo, originalStatus = _this$state.originalStatus;
              selectedStatus = paymentRequestInfo.Status;

              if (!(originalStatus.toLowerCase() === pendingStatus && (selectedStatus.toLowerCase() === approvedStatus || selectedStatus.toLowerCase() === rejectedStatus))) {
                _context2.next = 10;
                break;
              }

              paymentInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, {
                TransId: paymentRequestInfo.TransId,
                Status: selectedStatus,
                AdminDesc: paymentRequestInfo.AdminDesc,
                AffDesc: paymentRequestInfo.AffDesc
              });
              _context2.next = 6;
              return _this.props.updatePaymentRequest(paymentInfo);

            case 6:
              result = _context2.sent;

              if (result) {
                _this.setState({
                  isShowMessage: true,
                  isSuccess: result.IsSuccessful,
                  message: result.IsSuccessful ? "Transaction ID ".concat(paymentRequestInfo.TransId, " has been updated successfully!") : result.Message
                }, function () {
                  return window.setTimeout(_this.hideMessage, 3000);
                });

                if (result.IsSuccessful) {
                  window.setTimeout(function () {
                    return _this.props.handleClosePopup();
                  }, 3000);
                  window.setTimeout(function () {
                    return _this.props.handleFetchData();
                  }, 3200);
                }
              } else {
                _this.setState({
                  isSuccess: false,
                  isShowMessage: true,
                  message: _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["errorList"].UpdateFailed
                }, function () {
                  return window.setTimeout(_this.hideMessage, 3000);
                });
              }

              _context2.next = 11;
              break;

            case 10:
              _this.setState({
                isSuccess: false,
                isShowMessage: true,
                message: _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["errorList"].NotChoose
              }, function () {
                return window.setTimeout(_this.hideMessage, 3000);
              });

            case 11:
            case "end":
              return _context2.stop();
          }
        }
      }, _callee2);
    })));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "hideMessage", function () {
      _this.setState({
        isShowMessage: false
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleClosePopup", function () {
      _this.props.handleClosePopup();

      _this.setState({
        paymentRequestInfo: null
      });
    });

    _this.state = {
      paymentRequestInfo: null,
      originalStatus: '',
      isSuccess: true,
      isShowMessage: false,
      message: ''
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4___default()(PaymentRequestUpdateForm, [{
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var transactionId = this.props.transactionId;

      if (transactionId && transactionId !== prevProps.transactionId) {
        this.handleGetPaymentRequestInfo(transactionId);
      }
    }
  }, {
    key: "_renderAccountInformation",
    value: function _renderAccountInformation(paymentRequestInfo) {
      return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("fieldset", null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("legend", null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("h6", {
        className: "font-weight-bold"
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].AccountInformation)), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].UserName), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("strong", null, paymentRequestInfo.AffName))), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].RequestedDate), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("strong", null, paymentRequestInfo.TransDateText))), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].EmailAddress), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, paymentRequestInfo.Email)), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].FirstName), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, paymentRequestInfo.FirstName)), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].LastName), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, paymentRequestInfo.LastName)));
    }
  }, {
    key: "_renderPaymentCompletionInfo",
    value: function _renderPaymentCompletionInfo(paymentRequestInfo) {
      return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_10___default.a.Fragment, null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].CompletedDate), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("strong", null, paymentRequestInfo.CompletedDateText))), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].UpdatedBy), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, paymentRequestInfo.Creator)));
    }
  }, {
    key: "_renderOptionalStatus",
    value: function _renderOptionalStatus() {
      return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2 ml-1"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        className: "mr-4",
        check: true
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        check: true
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Input"], {
        name: "Status",
        type: "radio",
        className: "mr-2",
        value: _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["statusList"].Approved,
        onChange: this.handleChange
      }), _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["statusList"].Approved)), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        check: true
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        check: true
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Input"], {
        name: "Status",
        type: "radio",
        className: "mr-2",
        value: _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["statusList"].Rejected,
        onChange: this.handleChange
      }), _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["statusList"].Rejected)));
    }
  }, {
    key: "_renderPaymentInformation",
    value: function _renderPaymentInformation(paymentRequestInfo, isPending) {
      return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("fieldset", null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("legend", null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("h6", {
        className: "font-weight-bold"
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].PaymentInformation)), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].PaymentMethod), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("strong", null, paymentRequestInfo.PaymentMethod))), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].Amount), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("strong", null, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_15__["formatDecimalWithCurrencySymbol"])(paymentRequestInfo.Amount)))), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].Status), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("strong", null, paymentRequestInfo.Status))), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].UserName12Bet), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 6
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("strong", null, paymentRequestInfo.PlayerUsername))), isPending ? this._renderOptionalStatus() : this._renderPaymentCompletionInfo(paymentRequestInfo));
    }
  }, {
    key: "_renderNote",
    value: function _renderNote(isPending) {
      var _this$state2 = this.state,
          adminDesc = _this$state2.adminDesc,
          affDesc = _this$state2.affDesc;
      return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Row"], null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Col"], {
        sm: 6,
        className: "pr-1"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("fieldset", null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("legend", null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("h6", {
        className: "font-weight-bold"
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].AdminNote)), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        for: "AdminDesc"
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].VisibleAdmin), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Input"], {
        type: "textarea",
        name: "AdminDesc",
        rows: "6",
        disabled: !isPending,
        value: adminDesc,
        onChange: this.handleChange
      })))), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Col"], {
        sm: 6,
        className: "pl-1"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("fieldset", null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("legend", null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("h6", {
        className: "font-weight-bold"
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].AffiliateNote)), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        for: "AffDesc"
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["resource"].VisibleAffiliate), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Input"], {
        type: "textarea",
        name: "AffDesc",
        rows: "6",
        disabled: !isPending,
        value: affDesc,
        onChange: this.handleChange
      })))));
    }
  }, {
    key: "render",
    value: function render() {
      var transactionId = this.props.transactionId;
      var _this$state3 = this.state,
          paymentRequestInfo = _this$state3.paymentRequestInfo,
          originalStatus = _this$state3.originalStatus,
          isSuccess = _this$state3.isSuccess,
          isShowMessage = _this$state3.isShowMessage,
          message = _this$state3.message;
      var isPending = originalStatus.toLowerCase() === pendingStatus;
      return paymentRequestInfo && react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Modal"], {
        isOpen: transactionId,
        toggle: this.handleClosePopup,
        backdrop: "static"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["ModalHeader"], {
        toggle: this.handleClosePopup
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("span", {
        className: "iconpopup"
      }), "Trxn.ID ", transactionId), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["ModalBody"], null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("div", {
        className: "modal-body-wrapper p-0"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Form"], {
        tag: "form",
        className: "payment-request-update-form"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Alert"], {
        color: isSuccess ? 'success' : 'danger',
        isOpen: isShowMessage
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("div", {
        dangerouslySetInnerHTML: {
          __html: message
        }
      })), this._renderAccountInformation(paymentRequestInfo), this._renderPaymentInformation(paymentRequestInfo, isPending), this._renderNote(paymentRequestInfo, isPending), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Button"], {
        type: "button",
        color: "success",
        className: "mr-3",
        disabled: !isPending,
        onClick: this.handleSave
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["globalResource"].Update), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Button"], {
        type: "button",
        color: "info",
        onClick: this.handleClosePopup
      }, _modules_constants_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_14__["globalResource"].Cancel)))));
    }
  }]);

  return PaymentRequestUpdateForm;
}(react__WEBPACK_IMPORTED_MODULE_10__["PureComponent"]);

function mapStateToProps() {}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_12__["bindActionCreators"])({
    getPaymentRequestDetail: _actions_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_16__["getPaymentRequestDetail"],
    updatePaymentRequest: _actions_paymentRequestUpdate__WEBPACK_IMPORTED_MODULE_16__["updatePaymentRequest"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_11__["connect"])(mapStateToProps, mapDispatchToProps)(PaymentRequestUpdateForm));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/common/modules/constants/paymentRequestUpdate.js":
/*!*******************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/common/modules/constants/paymentRequestUpdate.js ***!
  \*******************************************************************************************/
/*! exports provided: globalResource, errorList, pending, statusList, resource */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "errorList", function() { return errorList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "pending", function() { return pending; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "statusList", function() { return statusList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "resource", function() { return resource; });
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "globalResource", function() { return _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__["globalResource"]; });



var _statusList;



var errorList = {
  NotChoose: 'You must choose Approved or Rejected for this request!',
  UpdateFailed: 'Update transaction failed!'
};
var pending = 'Pending';
var statusList = (_statusList = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_statusList, pending, 'Processing'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_statusList, "Approved", 'Approved'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_statusList, "Rejected", 'Rejected'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_statusList, "Cancelled", 'Cancelled'), _statusList);
var resource = {
  AccountInformation: 'Account Information',
  UserName: 'User Name',
  RequestedDate: 'Requested Date',
  EmailAddress: 'Email Address',
  FirstName: 'First Name',
  LastName: 'Last Name',
  PaymentInformation: 'Payment Information',
  PaymentMethod: 'Payment Method',
  Amount: 'Amount',
  Status: 'Status',
  UserName12Bet: '12BET User Name',
  CompletedDate: 'Completed Date',
  UpdatedBy: 'Updated By',
  AdminNote: 'Admin Note',
  VisibleAdmin: 'Visible only to admin',
  AffiliateNote: 'Affiliate Note',
  VisibleAffiliate: 'Visible also to affiliate'
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/actions/index.js":
/*!****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/statement-history/actions/index.js ***!
  \****************************************************************************************/
/*! exports provided: setPagingOption, toggleLoaderUserPanel, getAffiliateInfo, redirectToDailyEarning, setParam, setTransactionId, fetchData */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setParam", function() { return setParam; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setTransactionId", function() { return setTransactionId; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "fetchData", function() { return fetchData; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");
/* harmony import */ var _actions_pagingAction__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../../actions/pagingAction */ "./Areas/Portal/www/source/js/actions/pagingAction.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "setPagingOption", function() { return _actions_pagingAction__WEBPACK_IMPORTED_MODULE_3__["setPagingOption"]; });

/* harmony import */ var _customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../customer/user-panel/actions */ "./Areas/Portal/www/source/js/pages/customer/user-panel/actions/index.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "toggleLoaderUserPanel", function() { return _customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__["toggleLoaderUserPanel"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "redirectToDailyEarning", function() { return _customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__["redirectToDailyEarning"]; });

/* harmony import */ var _customer_mainpage_actions_loadingDataAction__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../customer/mainpage/actions/loadingDataAction */ "./Areas/Portal/www/source/js/pages/customer/mainpage/actions/loadingDataAction.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "getAffiliateInfo", function() { return _customer_mainpage_actions_loadingDataAction__WEBPACK_IMPORTED_MODULE_5__["getAffiliateInfo"]; });

/* harmony import */ var _actions_types__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../actions/types */ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/actions/types.js");









var setParam = function setParam(data) {
  return {
    type: _actions_types__WEBPACK_IMPORTED_MODULE_6__["SET_PARAM"],
    data: data
  };
};
var setTransactionId = function setTransactionId(data) {
  return {
    type: _actions_types__WEBPACK_IMPORTED_MODULE_6__["TRANSACTION_ID"],
    data: data
  };
};
var fetchData = function fetchData(param) {
  return (
    /*#__PURE__*/
    _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
    /*#__PURE__*/
    _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee() {
      var url, response;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
        while (1) {
          switch (_context.prev = _context.next) {
            case 0:
              url = 'Portal/UserPanel/StatementHistory/GetStatementHistoryList';
              _context.next = 3;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

            case 3:
              response = _context.sent;
              return _context.abrupt("return", response && response.status === 200 ? response.data : null);

            case 5:
            case "end":
              return _context.stop();
          }
        }
      }, _callee);
    }))
  );
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/components/AppContainer.jsx":
/*!***************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/statement-history/components/AppContainer.jsx ***!
  \***************************************************************************************************/
/*! exports provided: AppContainer, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppContainer", function() { return AppContainer; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _layouts_AppLayout__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");
/* harmony import */ var _Filter__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./Filter */ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/components/Filter.jsx");
/* harmony import */ var _Content__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/components/Content.jsx");
/* harmony import */ var _common_components_PaymentRequestUpdateForm__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../../../common/components/PaymentRequestUpdateForm */ "./Areas/Portal/www/source/js/pages/common/components/PaymentRequestUpdateForm.jsx");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/actions/index.js");
















var AppContainer =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6___default()(AppContainer, _PureComponent);

  function AppContainer(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1___default()(this, AppContainer);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_3___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_4___default()(AppContainer).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_7___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_5___default()(_this), "handleClosePopup", function () {
      _this.props.setTransactionId(null);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_7___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_5___default()(_this), "handleFetchData", function () {
      var _this$props = _this.props,
          affName = _this$props.affName,
          param = _this$props.param;

      _this.props.setParam(param);

      _this.props.getAffiliateInfo(affName);
    });

    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2___default()(AppContainer, [{
    key: "render",
    value: function render() {
      var transactionId = this.props.transactionId;
      return react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_11__["default"], null, react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(_Filter__WEBPACK_IMPORTED_MODULE_12__["default"], null), react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_13__["default"], null), react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(_common_components_PaymentRequestUpdateForm__WEBPACK_IMPORTED_MODULE_14__["default"], {
        transactionId: transactionId,
        handleClosePopup: this.handleClosePopup,
        handleFetchData: this.handleFetchData
      }));
    }
  }]);

  return AppContainer;
}(react__WEBPACK_IMPORTED_MODULE_8__["PureComponent"]);

function mapStateToProps(_ref) {
  var statementHistory = _ref.statementHistory,
      affCommonInfo = _ref.affCommonInfo;
  var transactionId = statementHistory.transactionId,
      param = statementHistory.param;
  var affName = affCommonInfo.affName;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    transactionId: transactionId,
    param: param,
    affName: affName
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_10__["bindActionCreators"])({
    setTransactionId: _actions__WEBPACK_IMPORTED_MODULE_15__["setTransactionId"],
    setParam: _actions__WEBPACK_IMPORTED_MODULE_15__["setParam"],
    getAffiliateInfo: _actions__WEBPACK_IMPORTED_MODULE_15__["getAffiliateInfo"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_9__["connect"])(mapStateToProps, mapDispatchToProps)(AppContainer));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/components/Content.jsx":
/*!**********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/statement-history/components/Content.jsx ***!
  \**********************************************************************************************/
/*! exports provided: Content, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Content", function() { return Content; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/objectSpread */ "./node_modules/@babel/runtime-corejs3/helpers/objectSpread.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_11___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_11__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @9softstudio/react-fixed-table-header */ "./node_modules/@9softstudio/react-fixed-table-header/dist/index.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14___default = /*#__PURE__*/__webpack_require__.n(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_numberFormat__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../../../../modules/numberFormat */ "./Areas/Portal/www/source/js/modules/numberFormat.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/actions/index.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/modules/constants.js");



















var Content =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default()(Content, _PureComponent);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Content);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(Content).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleSetTransactionId", function (transId) {
      _this.setState({
        shouldResetScrollPosition: false
      });

      _this.props.setTransactionId(transId);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handlePaging", function (pageIndex, pageSize) {
      var newPaging = {
        PageIndex: pageIndex,
        PageSize: pageSize
      };

      var newParam = _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3___default()({}, _this.props.param, {
        pageIndex: pageIndex,
        pageSize: pageSize
      });

      _this.props.setPagingOption(newPaging);

      _this.fetchData(newParam);
    });

    _this.state = {
      gridData: null,
      shouldResetScrollPosition: true
    };
    _this.transTypeClickHandler = _this._getClickHandlerByTransType();
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Content, [{
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var _this$props = this.props,
          pagingOption = _this$props.pagingOption,
          param = _this$props.param,
          affId = _this$props.affId;

      if (param !== prevProps.param || affId !== prevProps.affId) {
        var newParam = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, param, {
          pageIndex: 1,
          pageSize: pagingOption.PageSize
        });

        this.fetchData(newParam);
      }
    }
  }, {
    key: "fetchData",
    value: function () {
      var _fetchData = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee(param) {
        var postParam, data;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                this.props.toggleLoaderUserPanel(true);
                postParam = _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3___default()({}, param, {
                  affId: this.props.affId
                });
                _context.next = 4;
                return this.props.fetchData(postParam);

              case 4:
                data = _context.sent;
                this.setState({
                  gridData: data ? data.MainData : null,
                  shouldResetScrollPosition: true
                });
                this.props.setPagingOption({
                  TotalItem: data ? data.TotalItem : 0,
                  PageIndex: param.pageIndex,
                  PageSize: param.pageSize
                });
                this.props.toggleLoaderUserPanel(false);

              case 8:
              case "end":
                return _context.stop();
            }
          }
        }, _callee, this);
      }));

      function fetchData(_x) {
        return _fetchData.apply(this, arguments);
      }

      return fetchData;
    }()
  }, {
    key: "_renderHeader",
    value: function _renderHeader() {
      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], {
        key: "thead"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 35,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Index
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Index), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Date
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Date), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 110,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Type
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Type), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Amount
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Amount), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 80,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Status
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Status), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 110,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Creator
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Creator), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 465,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Description
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Description));
    }
  }, {
    key: "_renderDataSection",
    value: function _renderDataSection(dataList) {
      var dataRows = [];

      if (dataList && dataList.length) {
        var pagingOption = this.props.pagingOption;
        var startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

        for (var i = 0; i < dataList.length; i++) {
          var dataItem = dataList[i];
          dataRows.push(this._buildDataRow(dataItem, i + startIndex));
        }
      } else {
        dataRows.push(react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], null, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
          className: "text-center",
          colSpan: "7"
        }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["globalResource"].NoInfo)));
      }

      return dataRows;
    }
  }, {
    key: "_getClickHandlerByTransType",
    value: function _getClickHandlerByTransType() {
      var _this2 = this,
          _ref;

      return _ref = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_ref, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["PAYMENT_REQUEST"], function (dataItem) {
        return function () {
          return _this2.handleSetTransactionId(dataItem.TransId);
        };
      }), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_ref, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["MONTHLY_WINLOST"], function (dataItem) {
        return function () {
          var affId = _this2.props.affId;
          var dailyEarningParam = {
            affId: affId,
            monthDate: dataItem.DateText,
            productId: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["selectedAllProduct"]
          };
          return _this2.props.redirectToDailyEarning(dailyEarningParam);
        };
      }), _ref;
    }
  }, {
    key: "_buildDataRow",
    value: function _buildDataRow(dataItem, index) {
      var _this3 = this;

      var amount = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_16__["formatDecimalWithCurrencySymbol"])(dataItem.Amount);
      var transType = dataItem.TransType;
      var clickHandler = this.transTypeClickHandler[transType];
      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], {
        key: "dataRow-".concat(index)
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: index,
        className: "text-center"
      }, index), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.DateText,
        className: "text-center"
      }, dataItem.DateText), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: transType,
        className: "text-center"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["transTypeList"][transType] || _modules_constants__WEBPACK_IMPORTED_MODULE_18__["transaction"]), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: amount,
        className: "text-right"
      }, clickHandler ? react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Button"], {
        className: "p-0",
        color: "link",
        onClick: clickHandler(dataItem)
      }, amount) : amount), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.Status,
        className: "text-center"
      }, dataItem.Status === 'pending' ? react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Button"], {
        className: "p-0",
        color: "link",
        onClick: function onClick() {
          return _this3.handleSetTransactionId(dataItem.TransId);
        }
      }, "Processing") : dataItem.Status), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.Creator,
        className: "text-center"
      }, dataItem.Creator), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.Description
      }, dataItem.Description));
    }
  }, {
    key: "render",
    value: function render() {
      var _this$state = this.state,
          gridData = _this$state.gridData,
          shouldResetScrollPosition = _this$state.shouldResetScrollPosition;
      return gridData && react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "data-content"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "table-responsive table-striped my-2"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Table"], {
        minWidth: 1000,
        maxWidth: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["maxWidth"],
        bodyHeight: 300,
        header: this._renderHeader(),
        body: this._renderDataSection(gridData),
        containerPadding: 10,
        isPaging: true,
        pageOption: this.props.pagingOption,
        onPaging: this.handlePaging,
        shouldResetScrollPosition: shouldResetScrollPosition
      })));
    }
  }]);

  return Content;
}(react__WEBPACK_IMPORTED_MODULE_11__["PureComponent"]);

function mapStateToProps(_ref2) {
  var today = _ref2.today,
      affCommonInfo = _ref2.affCommonInfo,
      statementHistory = _ref2.statementHistory;
  var pagingOption = statementHistory.pagingOption,
      param = statementHistory.param;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, {
    today: today,
    affId: affId,
    pagingOption: pagingOption,
    param: param
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    setPagingOption: _actions__WEBPACK_IMPORTED_MODULE_17__["setPagingOption"],
    toggleLoaderUserPanel: _actions__WEBPACK_IMPORTED_MODULE_17__["toggleLoaderUserPanel"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_17__["fetchData"],
    setTransactionId: _actions__WEBPACK_IMPORTED_MODULE_17__["setTransactionId"],
    redirectToDailyEarning: _actions__WEBPACK_IMPORTED_MODULE_17__["redirectToDailyEarning"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/components/Filter.jsx":
/*!*********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/statement-history/components/Filter.jsx ***!
  \*********************************************************************************************/
/*! exports provided: Filter, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Filter", function() { return Filter; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @9softstudio/react-datepicker */ "./node_modules/@9softstudio/react-datepicker/dist/index.js");
/* harmony import */ var _9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_13___default = /*#__PURE__*/__webpack_require__.n(_9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_13__);
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/modules/constants.js");
/* harmony import */ var _modules_utils__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../../../../modules/utils */ "./Areas/Portal/www/source/js/modules/utils.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/actions/index.js");

















var minDate = new Date(2009, 11, 1);
var Filter =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7___default()(Filter, _PureComponent);

  function Filter(props) {
    var _context3, _context4, _context5, _context6;

    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default()(this, Filter);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default()(Filter).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleSearch", function () {
      var _context, _context2;

      var _this$state = _this.state,
          isGetAll = _this$state.isGetAll,
          fromDate = _this$state.fromDate,
          toDate = _this$state.toDate;

      var newParam = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, {
        isGetAll: isGetAll,
        fromDate: isGetAll ? _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context2 = "".concat(minDate.getMonth() + 1, "/")).call(_context2, minDate.getDate(), "/")).call(_context, minDate.getFullYear()) : fromDate,
        toDate: isGetAll ? _this.nowString : toDate
      });

      _this.props.setParam(newParam);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleChecked", function (event) {
      _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()({}, event.target.name, event.target.checked));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleChangeFromDate", function (momentDate) {
      _this.setState(Object(_modules_utils__WEBPACK_IMPORTED_MODULE_15__["getDateArrange"])(momentDate, _this.state.toDate));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleChangeToDate", function (momentDate) {
      _this.setState({
        toDate: momentDate.format("MM/DD/YYYY")
      });
    });

    var today = _this.props.today;
    var now = new Date(today);
    _this.nowString = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context3 = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context4 = "".concat(now.getMonth() + 1, "/")).call(_context4, now.getDate(), "/")).call(_context3, now.getFullYear());
    var sixMonthAgo = new Date(today);
    sixMonthAgo.setMonth(sixMonthAgo.getMonth() - 6);
    _this.state = {
      isGetAll: false,
      fromDate: _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context5 = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context6 = "".concat(sixMonthAgo.getMonth() + 1, "/")).call(_context6, sixMonthAgo.getDate(), "/")).call(_context5, sixMonthAgo.getFullYear()),
      toDate: _this.nowString
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default()(Filter, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      this.handleSearch();
    }
  }, {
    key: "render",
    value: function render() {
      var _this$state2 = this.state,
          isGetAll = _this$state2.isGetAll,
          fromDate = _this$state2.fromDate,
          toDate = _this$state2.toDate;
      return react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", {
        className: "filter-container"
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Form"], {
        inline: true,
        tag: "div"
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["FormGroup"], {
        className: "mb-2 mr-sm-2 mb-sm-0"
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Label"], {
        className: "mr-sm-2"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_14__["globalResource"].From), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_13__["DatePicker"], {
        minDate: minDate,
        maxDate: this.nowString,
        date: fromDate,
        isEnable: !isGetAll,
        iconElement: react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("span", {
          className: "oi oi-calendar icon-calendar ml-1"
        }),
        onChange: this.handleChangeFromDate
      })), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["FormGroup"], {
        className: "mb-2 mr-sm-2 mb-sm-0"
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Label"], {
        className: "mr-sm-2"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_14__["globalResource"].To), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_13__["DatePicker"], {
        minDate: fromDate,
        maxDate: this.nowString,
        date: toDate,
        isEnable: !isGetAll,
        iconElement: react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("span", {
          className: "oi oi-calendar icon-calendar ml-1"
        }),
        onChange: this.handleChangeToDate
      })), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["FormGroup"], {
        className: "mb-2 mr-sm-2 mb-sm-0"
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["FormGroup"], {
        check: true
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Label"], {
        check: true
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_14__["globalResource"].GetAll, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Input"], {
        name: "isGetAll",
        className: "ml-1 mr-2",
        type: "checkbox",
        checked: isGetAll,
        onChange: this.handleChecked
      })))), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Button"], {
        outline: true,
        color: "primary",
        type: "button",
        onClick: this.handleSearch
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("span", {
        className: "oi oi-magnifying-glass"
      }), " ", _modules_constants__WEBPACK_IMPORTED_MODULE_14__["globalResource"].Search)));
    }
  }]);

  return Filter;
}(react__WEBPACK_IMPORTED_MODULE_9__["PureComponent"]);

function mapStateToProps(_ref) {
  var today = _ref.today;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, {
    today: today
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_11__["bindActionCreators"])({
    setParam: _actions__WEBPACK_IMPORTED_MODULE_16__["setParam"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_10__["connect"])(mapStateToProps, mapDispatchToProps)(Filter));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/statement-history/modules/constants.js":
/*!********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/statement-history/modules/constants.js ***!
  \********************************************************************************************/
/*! exports provided: globalResource, selectedAllProduct, maxWidth, headerText, MONTHLY_WINLOST, PAYMENT_REQUEST, ADJUSTMENT, transTypeList, transaction */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "headerText", function() { return headerText; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "MONTHLY_WINLOST", function() { return MONTHLY_WINLOST; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PAYMENT_REQUEST", function() { return PAYMENT_REQUEST; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ADJUSTMENT", function() { return ADJUSTMENT; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "transTypeList", function() { return transTypeList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "transaction", function() { return transaction; });
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "globalResource", function() { return _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__["globalResource"]; });

/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "selectedAllProduct", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_2__["selectedAllProduct"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "maxWidth", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_2__["maxWidth"]; });



var _transTypeList;




var headerText = {
  Index: '#',
  Date: 'Date',
  Type: 'Type',
  Amount: 'Amount',
  Status: 'Status',
  Creator: 'Creator',
  Description: 'Description'
};
var MONTHLY_WINLOST = 201;
var PAYMENT_REQUEST = 202;
var ADJUSTMENT = 203;
var transTypeList = (_transTypeList = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_transTypeList, MONTHLY_WINLOST, 'Monthly Winlost'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_transTypeList, PAYMENT_REQUEST, 'Payment Request'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_transTypeList, ADJUSTMENT, 'Adjustment'), _transTypeList);
var transaction = 'Transaction';

/***/ }),

/***/ "./node_modules/moment/moment.js":
/*!*******************************************************************************************************!*\
  !*** delegated ../../../node_modules/moment/moment.js from dll-reference moment_b4d98fc51f6a4237233c ***!
  \*******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference moment_b4d98fc51f6a4237233c */ "dll-reference moment_b4d98fc51f6a4237233c"))(178);

/***/ })

}]);
//# sourceMappingURL=statementHistory.chunk.js.map