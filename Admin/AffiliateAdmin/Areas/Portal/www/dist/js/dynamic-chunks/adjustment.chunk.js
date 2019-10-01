(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["adjustment"],{

/***/ "./Areas/Portal/www/source/js/pages/user-panel/adjustment/actions/index.js":
/*!*********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/adjustment/actions/index.js ***!
  \*********************************************************************************/
/*! exports provided: toggleLoaderUserPanel, getAffiliateInfo, getBalanceBroughtForward, handleSubmit */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "getBalanceBroughtForward", function() { return getBalanceBroughtForward; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "handleSubmit", function() { return handleSubmit; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");
/* harmony import */ var _customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../customer/user-panel/actions */ "./Areas/Portal/www/source/js/pages/customer/user-panel/actions/index.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "toggleLoaderUserPanel", function() { return _customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__["toggleLoaderUserPanel"]; });

/* harmony import */ var _customer_mainpage_actions_loadingDataAction__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../customer/mainpage/actions/loadingDataAction */ "./Areas/Portal/www/source/js/pages/customer/mainpage/actions/loadingDataAction.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "getAffiliateInfo", function() { return _customer_mainpage_actions_loadingDataAction__WEBPACK_IMPORTED_MODULE_5__["getAffiliateInfo"]; });








var getBalanceBroughtForward = function getBalanceBroughtForward(param) {
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
              url = 'Portal/UserPanel/Adjustment/GetBalanceBroughtForward';
              _context.next = 3;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

            case 3:
              response = _context.sent;
              return _context.abrupt("return", response && response.status === 200 ? response.data : 0);

            case 5:
            case "end":
              return _context.stop();
          }
        }
      }, _callee);
    }))
  );
};
var handleSubmit = function handleSubmit(param) {
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
              url = 'Portal/UserPanel/Adjustment/InsertAdjustment';
              _context2.next = 3;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

            case 3:
              response = _context2.sent;
              return _context2.abrupt("return", response && response.status === 200 ? response.data : _modules_constants__WEBPACK_IMPORTED_MODULE_3__["submitFail"]);

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

/***/ "./Areas/Portal/www/source/js/pages/user-panel/adjustment/components/AppContainer.jsx":
/*!********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/adjustment/components/AppContainer.jsx ***!
  \********************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");
/* harmony import */ var _Content__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/user-panel/adjustment/components/Content.jsx");




var AppContainer = function AppContainer(props) {
  return react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__["default"], null, react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_2__["default"], null));
};

/* harmony default export */ __webpack_exports__["default"] = (react__WEBPACK_IMPORTED_MODULE_0___default.a.memo(AppContainer));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/adjustment/components/Content.jsx":
/*!***************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/adjustment/components/Content.jsx ***!
  \***************************************************************************************/
/*! exports provided: Content, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Content", function() { return Content; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__);
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
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/adjustment/actions/index.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/adjustment/modules/constants.js");
/* harmony import */ var _modules_numberFormat__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../../../../modules/numberFormat */ "./Areas/Portal/www/source/js/modules/numberFormat.js");

















var regexCheckAmount = new RegExp(_modules_constants__WEBPACK_IMPORTED_MODULE_15__["regExp"].checkAmount);
var regexValidAmount = new RegExp(_modules_constants__WEBPACK_IMPORTED_MODULE_15__["regExp"].validAmount);
var regexShouldFormatAmount = new RegExp(_modules_constants__WEBPACK_IMPORTED_MODULE_15__["regExp"].shouldFormatAmount);
var Content =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default()(Content, _PureComponent);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3___default()(this, Content);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default()(Content).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleChange", function (event) {
      _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, event.target.name, event.target.value));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleChangeAmount", function (event) {
      var value = event.target.value;
      var amount = value.replace(/,/g, '');

      if (regexCheckAmount.test(amount)) {
        if (regexShouldFormatAmount.test(amount) && Number(amount) !== 0 && Number(amount) > _modules_constants__WEBPACK_IMPORTED_MODULE_15__["money"].MinValue && Number(amount) < _modules_constants__WEBPACK_IMPORTED_MODULE_15__["money"].MaxValue) {
          _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, event.target.name, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_16__["format"])(Number(amount))));
        } else {
          _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, event.target.name, value));
        }
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "_isValidAmount", function (amount) {
      var absBalanceBf = Math.abs(_this.state.balanceBf);
      return regexValidAmount.test(amount) && Number(amount) - absBalanceBf > _modules_constants__WEBPACK_IMPORTED_MODULE_15__["money"].MinValue && Number(amount) + absBalanceBf < _modules_constants__WEBPACK_IMPORTED_MODULE_15__["money"].MaxValue;
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "_validateAmount", function () {
      var amount = _this.state.amount.replace(/,/g, '');

      var result = {
        isValid: _this._isValidAmount(amount) && Number(amount) != 0,
        message: ''
      };

      if (!result.isValid) {
        result.message = amount === '' ? _modules_constants__WEBPACK_IMPORTED_MODULE_15__["message"].Required : _modules_constants__WEBPACK_IMPORTED_MODULE_15__["message"].Invalid;
      }

      return result;
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleSubmit",
    /*#__PURE__*/
    _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()(
    /*#__PURE__*/
    _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee() {
      var amountValidation, affInfo, _this$state, amount, description, amountNumber, adjustmentInfo, result;

      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
        while (1) {
          switch (_context.prev = _context.next) {
            case 0:
              _this.props.toggleLoaderUserPanel(true);

              amountValidation = _this._validateAmount();

              if (!amountValidation.isValid) {
                _context.next = 14;
                break;
              }

              affInfo = _this.props.affInfo;
              _this$state = _this.state, amount = _this$state.amount, description = _this$state.description;
              amountNumber = Number(amount.replace(/,/g, ''));
              adjustmentInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, {
                AffId: affInfo.AffId,
                Amount: amountNumber,
                Description: description
              });
              _context.next = 9;
              return _this.props.handleSubmit(adjustmentInfo);

            case 9:
              result = _context.sent;

              _this.setState({
                isShowMessage: true,
                isSuccess: result.IsSuccessful,
                message: result.IsSuccessful ? _modules_constants__WEBPACK_IMPORTED_MODULE_15__["message"].Success : result.Message,
                amount: result.IsSuccessful ? '' : amount,
                description: result.IsSuccessful ? '' : description
              }, function () {
                return window.setTimeout(_this.hideMessage, 3000);
              });

              if (result.IsSuccessful) {
                _this.getBalanceBroughtForward();

                window.setTimeout(function () {
                  return _this.props.getAffiliateInfo(affInfo.AffName, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["userPanelMenuKey"].adjustment);
                }, 3000);
              }

              _context.next = 15;
              break;

            case 14:
              _this.setState({
                isSuccess: false,
                isShowMessage: true,
                message: amountValidation.message
              }, function () {
                return window.setTimeout(_this.hideMessage, 3000);
              });

            case 15:
              _this.props.toggleLoaderUserPanel(false);

            case 16:
            case "end":
              return _context.stop();
          }
        }
      }, _callee);
    })));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "hideMessage", function () {
      _this.setState({
        isShowMessage: false
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handlePressEnter", function (event) {
      if (event.charCode === 13) {
        event.preventDefault();

        _this.handleSubmit();
      }
    });

    _this.state = {
      balanceBf: 0,
      amount: '',
      description: '',
      isShowMessage: false,
      isSuccess: true,
      message: ''
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4___default()(Content, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      this.getBalanceBroughtForward();
    }
  }, {
    key: "getBalanceBroughtForward",
    value: function () {
      var _getBalanceBroughtForward = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee2() {
        var data;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee2$(_context2) {
          while (1) {
            switch (_context2.prev = _context2.next) {
              case 0:
                this.props.toggleLoaderUserPanel(true);
                _context2.next = 3;
                return this.props.getBalanceBroughtForward(this.props.affInfo);

              case 3:
                data = _context2.sent;
                this.setState({
                  balanceBf: data
                });
                this.props.toggleLoaderUserPanel(false);

              case 6:
              case "end":
                return _context2.stop();
            }
          }
        }, _callee2, this);
      }));

      function getBalanceBroughtForward() {
        return _getBalanceBroughtForward.apply(this, arguments);
      }

      return getBalanceBroughtForward;
    }()
  }, {
    key: "render",
    value: function render() {
      var affInfo = this.props.affInfo;
      var _this$state2 = this.state,
          balanceBf = _this$state2.balanceBf,
          amount = _this$state2.amount,
          description = _this$state2.description,
          isShowMessage = _this$state2.isShowMessage,
          isSuccess = _this$state2.isSuccess,
          message = _this$state2.message;
      var isValidAmount = this._isValidAmount(amount.replace(/,/g, '')) || amount === '';
      return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Form"], {
        tag: "form",
        className: "adjustment-form"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("fieldset", null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("legend", null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("h5", {
        className: "font-weight-bold"
      }, affInfo.AffName)), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Alert"], {
        color: isSuccess ? 'success' : 'danger',
        isOpen: isShowMessage
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("div", {
        dangerouslySetInnerHTML: {
          __html: message
        }
      })), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 4
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].BalanceBf), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 8
      }, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_16__["formatDecimalWithCurrencySymbol"])(balanceBf))), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: isValidAmount ? "mb-2" : "mb-2 has-error"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        for: "amount",
        sm: 4,
        className: "required"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].Amount), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Col"], {
        sm: 8
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Input"], {
        type: "text",
        name: "amount",
        id: "amount",
        className: "text-right h-75",
        value: amount,
        onChange: this.handleChangeAmount,
        onKeyPress: this.handlePressEnter
      }))), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-3"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        for: "description",
        sm: 4,
        className: "align-self-center"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].Description), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Col"], {
        sm: 8
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Input"], {
        type: "textarea",
        name: "description",
        id: "description",
        rows: "6",
        value: description,
        onChange: this.handleChange
      }))), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        sm: 4
      }), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Col"], {
        sm: 8
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Button"], {
        type: "button",
        color: "success",
        disabled: !isValidAmount,
        onClick: this.handleSubmit
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].Submit)))));
    }
  }]);

  return Content;
}(react__WEBPACK_IMPORTED_MODULE_10__["PureComponent"]);

function mapStateToProps(_ref2) {
  var affCommonInfo = _ref2.affCommonInfo;
  var affInfo = {
    AffId: affCommonInfo.affId,
    AffName: affCommonInfo.affName
  };
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, {
    affInfo: affInfo
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_12__["bindActionCreators"])({
    toggleLoaderUserPanel: _actions__WEBPACK_IMPORTED_MODULE_14__["toggleLoaderUserPanel"],
    getBalanceBroughtForward: _actions__WEBPACK_IMPORTED_MODULE_14__["getBalanceBroughtForward"],
    handleSubmit: _actions__WEBPACK_IMPORTED_MODULE_14__["handleSubmit"],
    getAffiliateInfo: _actions__WEBPACK_IMPORTED_MODULE_14__["getAffiliateInfo"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_11__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/adjustment/modules/constants.js":
/*!*************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/adjustment/modules/constants.js ***!
  \*************************************************************************************/
/*! exports provided: userPanelMenuKey, money, resource, message, regExp */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "resource", function() { return resource; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "message", function() { return message; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "regExp", function() { return regExp; });
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "money", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_0__["money"]; });

/* harmony import */ var _customer_common_modules_constants__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../customer/common/modules/constants */ "./Areas/Portal/www/source/js/pages/customer/common/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "userPanelMenuKey", function() { return _customer_common_modules_constants__WEBPACK_IMPORTED_MODULE_1__["userPanelMenuKey"]; });




var resource = {
  BalanceBf: 'Balance Brought Forward',
  Amount: 'Amount',
  Description: 'Description',
  Submit: 'Submit'
};
var message = {
  Required: 'The amount is required',
  Invalid: 'The amount is invalid',
  Success: 'Updated successfully!'
};
var regExp = {
  checkAmount: "^\\-?(\\d+\\.?\\d*)?$",
  shouldFormatAmount: "^\\-?\\d+$",
  validAmount: "^\\-?\\d+(\\.\\d{1,})?$"
};

/***/ })

}]);
//# sourceMappingURL=adjustment.chunk.js.map