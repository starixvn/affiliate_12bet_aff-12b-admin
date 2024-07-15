import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert, Col, Form, FormFeedback, FormGroup, Input, Label } from 'reactstrap';
import { setSelectedRequest, setSelectedRequestId, getProcessingRequestDetail, fetchData, toggleLoader, updateProcessingRequest, insertProcessingRequest, setOriginalData } from '../../actions';
import { playerMethod, wireMethod, resource, statusList } from '../../libs/constants';
import { trimText } from '../../../../../modules/htmlHelper';
import { formatDateTime } from '../../../../../modules/utils';
import { validate, validateDeadline } from '../../libs/service';

const paymentMethodList = [
    { key: playerMethod, value: '12BET player account' },
    { key: wireMethod, value: 'Bank Transfer' }
]

export class EditRequest extends React.PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isShowMessage: false,
            isSuccess: false,
            isSubmit: false,
            message: [],
        }
        this.refAlert = React.createRef();
    }


    componentDidMount() {
        if (!this.props.selectedRequestId) {
            const selectedRequest = Object.assign({}, this.props.selectedRequest, { ['Status']: "Draft" })
            this.props.setSelectedRequest(selectedRequest);
        }
    }

    componentWillUnmount() {
        this.props.setSelectedRequest(null);
        this.props.setSelectedRequestId(null);
        this.props.setOriginalData(null);
    }

    hideMessage = () => {
        this.refAlert.current.scrollIntoView(false);
        window.setTimeout( () => this.setState({isShowMessage: false}), 3000);
    }

    handleSave = async () => {
        this.props.toggleLoader(true);
        this.setState({
            isSubmit : true
        });
        const { selectedRequest, selectedRequestId, originalData } = this.props;
        const validationResult = validate(selectedRequest, originalData);
        const isInsertedForm = !selectedRequestId;


        if (!validationResult.isValid) {
            this.setState({
                isShowMessage: !validationResult.isValid,
                isSuccess: validationResult.isValid,
                message: validationResult.messageList
            }, () => this.hideMessage());
        }
        else {
            const saveParam = selectedRequest;

            const result = isInsertedForm
                ? await this.props.insertProcessingRequest(saveParam)
                : await this.props.updateProcessingRequest(saveParam);

            const successMsg = isInsertedForm ? resource.InsertSuccess : resource.UpdateSuccess;

            if (result.IsSuccessful) {
                window.setTimeout(this.props.onClose, 3000);
                const { pagingOption, param } = this.props;
                const gettingParam = Object.assign({}, param, pagingOption);
                this.props.fetchData(gettingParam);
            }

            this.props.onSave && this.props.onSave();
            this.setState({
                isShowMessage: true,
                isSuccess: result.IsSuccessful,
                message: [result.IsSuccessful ? [successMsg] : result.Message]
            }, () => this.hideMessage());
        }
        this.props.toggleLoader(false);
    }

    handleChangePaymentMethod = (event) => {
        const selectedRequest = Object.assign({}, this.props.selectedRequest, { PreferredPayment: event.target.value })
        this.props.setSelectedRequest(selectedRequest);
    }

    handleChange = (event) => {
        const target = event.target;
        const selectedRequest = Object.assign({}, this.props.selectedRequest, { [target.name]: target.value })
        this.props.setSelectedRequest(selectedRequest);
    }

    validateMaxLength = (text, maxLength) => {
        if (!!text) {
            return text.length > maxLength;
        }
        return false;
    }

    validateRequire = (text) => !text;

    validateDeadline = (selectedRequest, currentDate) => {
        const { today } = this.props;
        const result = validateDeadline(selectedRequest, currentDate, today);

        return result.isValid;
    }

    renderTextArea = (fieldName, labelName, value, isReadOnly, isRequired, maxLength = 1000) => {
        const {isSubmit } = this.state;
        var text = trimText(value);
        const classRequired = isRequired ? "required" : "";
        return (
            <FormGroup row>
                <Label sm={3} for={fieldName} className={classRequired}>{labelName}</Label>
                <Col sm={9}>
                    <Input type='textarea' id={fieldName} name={fieldName} value={text} onChange={this.handleChange}
                        invalid={isSubmit && (this.validateMaxLength(text, maxLength) || (isRequired && this.validateRequire(text)))}
                        required={isRequired}
                        disabled={isReadOnly}>

                        </Input>
                    {isRequired && isSubmit && this.validateRequire(text) && <FormFeedback>{resource.FieldRequeired}</FormFeedback>}
                    {this.validateMaxLength(text, maxLength) && <FormFeedback>{resource.ExceedMaxLength + ` ${maxLength}`}</FormFeedback>}
                </Col>
            </FormGroup>
        )
    }

    _renderFormByMethod(selectedRequest) {
        return {
            [playerMethod]: this._renderPaymentForm('Player_UserName', '12BET Username', selectedRequest && selectedRequest.Player_UserName, false, this.handleChange),
            [wireMethod]: this._renderWireForm(selectedRequest)
        }
    }

    _renderPaymentForm(name, label, inputValue, isRequired, changeHandler) {
        const { isReadOnly } = this.props;

        return (
            <FormGroup row>
                <Label for={name} sm={3} className={isRequired ? "required" : ""}>{label}</Label>
                <Col sm={5}>
                    <Input type="text" id={name} name={name} value={trimText(inputValue)} disabled={isReadOnly} onChange={changeHandler} />
                </Col>
            </FormGroup>
        )
    }

    _renderWireForm(selectedRequest) {
        return (
            <React.Fragment>
                {this._renderPaymentForm('BankName', 'Bank Name', trimText(selectedRequest && selectedRequest.BankName), false, this.handleChange)}
                {this._renderPaymentForm('BankAddress', 'Bank Address', trimText(selectedRequest && selectedRequest.BankAddress), false, this.handleChange)}
                {this._renderPaymentForm('BankSwiftCode', 'Bank ABA/Swift/Sort Code', trimText(selectedRequest && selectedRequest.BankSwiftCode), false, this.handleChange)}
                {this._renderPaymentForm('BankAccountName', 'Bank Account Name', trimText(selectedRequest && selectedRequest.BankAccountName), false, this.handleChange)}
                {this._renderPaymentForm('BankAccountNo', 'Bank Account Number', trimText(selectedRequest && selectedRequest.BankAccountNo), false, this.handleChange)}
            </React.Fragment>
        )
    }

    render() {
        const { selectedRequest, isReadOnly, languageList, originalData, selectedRequestId } = this.props;
        const { isShowMessage, isSuccess, message, isSubmit } = this.state;
        const renderFormByMethod = this._renderFormByMethod(selectedRequest);
        const deadline = formatDateTime(trimText(selectedRequest && selectedRequest.Deadline), 'yyyy-MM-dd');
        const isUpdateForm = !!selectedRequestId;
        const originalStatus = (originalData && originalData.Status) ?? "Draft";
        const currentDeadline = originalData && originalData.Deadline;

        return (
            <div className='request-container'>
                <Alert color={isSuccess ? 'success' : 'danger'}  isOpen={isShowMessage}>
                    <div ref={this.refAlert} dangerouslySetInnerHTML={{ __html: message.join('<br>') }}></div>
                </Alert>
                <Form>
                    <FormGroup row>
                        <Label for="UserName" className={isUpdateForm ? 'pt-0' : 'required pt-0'} sm={2}>Affiliate </Label>
                        <Col sm={4}>
                            {isUpdateForm
                                ? (<b>{selectedRequest.UserName}</b>)
                                : (<React.Fragment>
                                    <Input type="text" name="UserName" id="UserName" className='remove-background' value={trimText(selectedRequest && selectedRequest.UserName)}
                                        onChange={this.handleChange}
                                        invalid={isSubmit && (this.validateMaxLength(selectedRequest && selectedRequest.UserName, 30) || this.validateRequire(selectedRequest && selectedRequest.UserName))} />
                                    {isSubmit && this.validateRequire(selectedRequest && selectedRequest.UserName) && <FormFeedback>{resource.FieldRequeired}</FormFeedback>}
                                    {isSubmit && this.validateMaxLength(selectedRequest && selectedRequest.UserName, 30) && <FormFeedback>{resource.ExceedMaxLength + ' 30'}</FormFeedback>}
                                </React.Fragment>
                                )
                            }

                        </Col>
                    </FormGroup>
                    <fieldset>
                        <legend><h6>Player's Details Amendment</h6></legend>
                        <FormGroup row>
                            <Label sm={3}>Language</Label>
                            <Col sm={5}>
                                <Input type="select" name="MarketId" id="MarketId" value={selectedRequest && selectedRequest.MarketId} onChange={this.handleChange} disabled={isReadOnly}>
                                    <option key={'status-all'} value=''>No change</option>
                                    {
                                        languageList.map(item => {
                                            return (<option key={'sortBy-' + item.LanguageCode} value={item.LanguageCode}>{item.LanguageName}</option>)
                                        })
                                    }
                                </Input>
                            </Col>

                        </FormGroup>
                        <FormGroup row>
                            <Label sm={3}>Payment Method</Label>
                            <Col sm={5}>
                                <Input type='select' name='PreferredPayment' id='preferredPayment'
                                    value={selectedRequest && selectedRequest.PreferredPayment} sm={6}
                                    onChange={this.handleChangePaymentMethod} disabled={isReadOnly}>
                                    <option key='no-change' value=''>No change</option>
                                    {paymentMethodList.map(item => {
                                        return (<option key={item.key} value={item.key}>{item.value}</option>)
                                    })}
                                </Input>
                            </Col>
                        </FormGroup>
                        {selectedRequest && selectedRequest.PreferredPayment && renderFormByMethod[selectedRequest.PreferredPayment]}
                    </fieldset>
                    <fieldset>
                        <legend><h6>Affiliate Promotion Method & Details</h6></legend>
                        {this.renderTextArea('ChangeFirstName', 'Change First Name', selectedRequest && selectedRequest.ChangeFirstName, isReadOnly, false, 100)}
                        {this.renderTextArea('ChangeLastName', 'Change Last Name', selectedRequest && selectedRequest.ChangeLastName, isReadOnly, false, 100)}
                        {this.renderTextArea('PromotionMethod', 'Promotion Method', selectedRequest && selectedRequest.PromotionMethod, isReadOnly)}
                        {this.renderTextArea('WebRank', 'Website Rank', selectedRequest && selectedRequest.WebRank, isReadOnly)}
                        {this.renderTextArea('WebRegDate', 'Website Register Date', selectedRequest && selectedRequest.WebRegDate, isReadOnly)}
                        {this.renderTextArea('WebSMLanguage', 'Website/SM Language', selectedRequest && selectedRequest.WebSMLanguage, isReadOnly)}
                        {this.renderTextArea('ServerCountry', 'Country (Server)', selectedRequest && selectedRequest.ServerCountry, isReadOnly)}
                        {this.renderTextArea('WebSMQuality', 'Quality of Website/SM', selectedRequest && selectedRequest.WebSMQuality, isReadOnly)}
                        {this.renderTextArea('WebSMType', 'Type of Website/SM', selectedRequest && selectedRequest.WebSMType, isReadOnly)}
                        {this.renderTextArea('WebAdvAff', 'Adv/Aff on the Website/SM', selectedRequest && selectedRequest.WebAdvAff, isReadOnly)}
                        {this.renderTextArea('TargetMarket', 'Target Market', selectedRequest && selectedRequest.TargetMarket, isReadOnly)}
                    </fieldset>
                    <fieldset>
                        <legend><h6>Final Verdict by Agent</h6></legend>
                        {this.renderTextArea('AgentName', 'Agent Name', selectedRequest && selectedRequest.AgentName, isReadOnly, true)}
                        {this.renderTextArea('ContactModeTime', 'Contactable Mode & Time', selectedRequest && selectedRequest.ContactModeTime, isReadOnly, false)}
                        <FormGroup row>
                            <Label sm={3} for='Status'>Status</Label>
                            <Col sm={9}>
                                <Input sm={9} type='select' id="Status" name="Status" onChange={this.handleChange} value={selectedRequest && selectedRequest.Status} disabled={isReadOnly}>
                                    {Object.keys(statusList).map(key => {
                                        return statusList[key].shouldShow(originalStatus)
                                            ? <option key={key} value={key}>{key}</option>
                                            : '';
                                    })}
                                </Input>
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label sm={3} className='required pt-0' for="Deadline">Deadline</Label>
                            <Col sm={3}>
                                <Input type='date' id='Deadline' name='Deadline' value={deadline} className='remove-background'
                                    invalid={isSubmit && (this.validateRequire(selectedRequest && selectedRequest.Deadline) || !this.validateDeadline(selectedRequest, currentDeadline))}
                                    onChange={this.handleChange} disabled={isReadOnly}></Input>
                                {isSubmit && this.validateRequire(selectedRequest && selectedRequest.Deadline) && <FormFeedback>{resource.FieldRequeired}</FormFeedback>}
                                {isSubmit && !this.validateRequire(selectedRequest && selectedRequest.Deadline) && !this.validateDeadline(selectedRequest, currentDeadline) && <FormFeedback>{resource.InvalidDeadline}</FormFeedback>}
                            </Col>
                        </FormGroup>
                        {this.renderTextArea('Remark', 'Remark', selectedRequest && selectedRequest.Remark, isReadOnly)}
                    </fieldset>
                    <button type="button" className="my-2 btn btn-success btn-save" onClick={this.handleSave} disabled={isReadOnly || isSuccess}>SAVE</button>
                </Form>
            </div>
        );
    }
}

function mapStateToProp({ today, selectedRequest, selectedRequestId, languageList, originalData, pagingOption, param }) {
    return { today, selectedRequest, selectedRequestId, languageList, originalData, pagingOption, param };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedRequest, setSelectedRequestId, getProcessingRequestDetail, fetchData, toggleLoader, updateProcessingRequest, insertProcessingRequest, setOriginalData }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(EditRequest);