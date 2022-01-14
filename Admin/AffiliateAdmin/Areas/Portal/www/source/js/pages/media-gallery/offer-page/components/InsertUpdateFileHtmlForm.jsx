import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedId, getOfferPageDetail, fetchData, insertOfferPage, updateOfferPage, toggleLoader } from '../actions/index';
import { defaultState, globalResource, maxLength, maxFileSize, displayDateFormat } from '../libs/constants';
import { defaultInsertId } from '../../common/libs/constants';
import { formatDateTime } from '../../../../modules/utils';
import { htmlDecode } from '../../../../modules/htmlHelper';
import DatePicker from 'react-datepicker';

export class InsertUpdateFileHtmlForm extends PureComponent {
    constructor(props) {
        super(props)

        const language = props.languageList.find(l => l.LanguageName === defaultState.language) ? defaultState.language : props.languageList[0].LanguageName;
        const initState = Object.assign({}, { ...defaultState }, { language })
        this.state = {
            ...initState
        }
    }

    componentDidUpdate(prevProps) {
        const { selectedId } = this.props;
        if (prevProps.selectedId !== selectedId) {
            this.setState({ isInsert: selectedId === defaultInsertId });

            selectedId && this._handleGetItemDetail(selectedId);
        }
    }

    _handleGetItemDetail = async (selectedId) => {
        const item = await this.props.getOfferPageDetail(selectedId);
        const fileName = htmlDecode(item.FileName);

        item && this.setState({
            fileName,
            fileSize: item.FileSize,
            fromDate: new Date(item.EffectiveFromDate),
            toDate: new Date(item.EffectiveToDate),
            language: item.Language
        });
    }

    _handleChange = (event) => {
        const value = event.target.value;
        const name = event.target.name;

        this.setState({ [name]: value });
    }

    _handleChangeDatePicker = (name, value) => {
        this.setState({ [name]: value });
    }

    _handFileSelect = (event) => {
        var selectedFile = event.target.files[0];
        var fileName = selectedFile.name;
        this.setState({ selectedFile, fileName });
    }

    _validateInputs = () => {
        const { selectedFile, fileName, fromDate, toDate } = this.state;
        return this._validateFile(selectedFile) && this._validateFileName(fileName) && this._validateDateRange(fromDate, toDate);
    }

    _validateFile = (file) => {
        const { isInsert } = this.state;

        if (file && file.size > maxFileSize) {
            this._handleShowAlert(false, "Max file size is 3MB, please check file again!");
            return false;
        }
        if (file && file.type != "text/html") {
            this._handleShowAlert(false, "Please select html file!");
            return false;
        }
        if (isInsert && !file) {
            this._handleShowAlert(false, "Please select file to upload!");
            return false;
        }
        return true;
    }

    _validateFileName = (fileName) => {
        const rg1 = /^[^\\/$:\*\?"<>\|]+(.html)$/; // forbidden characters \ / : * ? $ " < > |
        if (!fileName) {
            this._handleShowAlert(false, "Please enter file name!");
            return false;
        }

        if (fileName.length > maxLength) {
            this._handleShowAlert(false, "File name too long. Max length is 50!");
            return false;
        }
        if (!(rg1.test(fileName))) {
            this._handleShowAlert(false, "File name must not contain \\ / : * ? $ \" < > | characters and extension must be html.");
            return false;
        }

        return true;
    }

    _validateDateRange = (fromDate, toDate) => {
        if (!fromDate || !toDate) {
            this._handleShowAlert(false, "Effective From and To are required")
            return false
        }

        if (toDate <= fromDate) {
            this._handleShowAlert(false, "Effective To Date must be greater than Effective From Date")
            return false
        }
        return true;
    }

    _insertNewItem = async (param) => {
        const result = await this.props.insertOfferPage(param);
        this._handleAfterUpload(result);
    }

    _updateItem = async (param) => {
        const result = await this.props.updateOfferPage(param);
        this._handleAfterUpload(result);
    }

    _save = async (param) => {
        const { isInsert } = this.state;
        if (isInsert) {
            await this._insertNewItem(param);
        }
        else {
            await this._updateItem(param);
        }
    }

    _getParam = () => {
        const formatISO = "yyyy-MM-ddTHH:mm:ss.SSS"
        const { selectedFile, fileName, fileSize, fromDate, toDate, language } = this.state;
        const { selectedId } = this.props;
        return Object.assign({}, { pageId: selectedId, file: selectedFile, fileName, fileSize, effectiveFrom: formatDateTime(fromDate, formatISO), effectiveTo: formatDateTime(toDate, formatISO), language });
    }

    _handleAfterUpload = (result) => {
        const { isInsert } = this.state;
        const message = isInsert ? "The offer page has been added successfully!" : "The offer page has been updated successfully!";

        this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
    }

    _handleSubmit = () => {
        this.props.toggleLoader(true);

        if (this._validateInputs()) {
            const param = this._getParam();
            this._save(param);
        }

        this.props.toggleLoader(false);
    }

    _handleKeyPress = (event) => {
        if (event.charCode === 13) {
            event.preventDefault();
            this._handleSubmit();
        }
    }

    _handleShowAlert = (isSuccess, message) => {
        this.setState({
            message,
            isSuccess,
            isShowMessage: true,
        }, () => window.setTimeout(() => this._hideMessage(isSuccess), 3000));
    }

    _hideMessage = (isSuccess) => {
        this.setState({
            isShowMessage: false
        });

        if (isSuccess) {
            this.props.fetchData(this.props.param);
            this._handleClosePopup();
        }
    }

    _handleClosePopup = () => {
        this.props.setSelectedId(null);

        this.setState({ ...defaultState });
    }

    render() {
        const { fromDate, toDate, fileName, language, isShowMessage, isSuccess, message, isInsert, selectedFile } = this.state;
        const { languageList } = this.props;
        const isShowPopup = this.props.selectedId !== null;
        const title = isInsert ? "Add Offer Page" : "Edit Offer Page";
        const submitBtnText = isInsert ? "Upload" : "Update";
        const isUnselectFile = selectedFile === null;

        return (
            <React.Fragment>
                <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop='static'>
                    <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                    <ModalBody>
                        <div className="modal-body-wrapper">
                            <div className="news-letter-form-container">
                                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                                </Alert>
                                <div><Label>[Valid Types: .html]</Label></div>
                                <div><Label>[File Size Limit: 3MB]</Label></div>
                                <div><Label>[Maximum files: 1]</Label></div>

                                <Form tag="form" className="mb-4" autoComplete="off">
                                    <FormGroup row className="mb-2 align-items-center" >
                                        <Label for="file" className="required" sm={4}>File</Label>
                                        <Col sm={6}>
                                            <Input className={isInsert && isUnselectFile ? "text-danger" : ""} type="file" name="file" id="file" accept=".html" onChange={this._handFileSelect} />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-2">
                                        <Label className="required" sm={4}>File Name</Label>
                                        <Col sm={6}>
                                            <Input type="text" name="fileName" className="input-required mt-2" value={fileName} onChange={this._handleChange}
                                                onKeyPress={this._handleKeyPress} />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-2">
                                        <Label sm={4}>Language</Label>
                                        <Col sm={6}>
                                            <Input type="select" name="language" className="input-required mt-2" value={language} onChange={this._handleChange}>
                                                {languageList.map(lang =>
                                                    <option key={lang.LanguageName} value={lang.LanguageName}>{lang.LanguageName}</option>
                                                )}
                                            </Input>
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-2 align-items-center">
                                        <Label for="fromDate" sm={4} className="required">Effective From (GMT-4)</Label>
                                        <Col sm={6}>
                                            <DatePicker name="fromDate" showTimeInput dateFormat={displayDateFormat} selected={fromDate} className='form-control' onChange={(value) => this._handleChangeDatePicker("fromDate", value)} />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-2 align-items-center">
                                        <Label for="toDate" sm={4} className="required">Effective To (GMT-4)</Label>
                                        <Col sm={6}>
                                            <DatePicker name="toDate" showTimeInput dateFormat={displayDateFormat} selected={toDate} className='form-control' onChange={(value) => this._handleChangeDatePicker("toDate", value)} />
                                        </Col>
                                    </FormGroup>
                                </Form>

                                <Button type="button" color="success" className="mr-3" disabled={isInsert && isUnselectFile} onClick={this._handleSubmit} >{submitBtnText}</Button>
                                <Button type="button" color="info" onClick={this._handleClosePopup}>{globalResource.Cancel}</Button>
                            </div>
                        </div>
                    </ModalBody>
                </Modal>
            </React.Fragment>
        );
    }
}

function mapStateToProps({ param, selectedId, languageList }) {
    return Object.assign({}, { param, selectedId, languageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedId, getOfferPageDetail, fetchData, insertOfferPage, updateOfferPage, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(InsertUpdateFileHtmlForm);