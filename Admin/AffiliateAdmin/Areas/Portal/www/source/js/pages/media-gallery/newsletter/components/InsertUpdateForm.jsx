import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedNewsLetterPageId, getNewsLetterPageDetail, fetchData, insertNewsLetterPage, updateNewsLetterPage, toggleLoader } from '../actions/index';
import { defaultState, globalResource, maxLength, maxFileSize } from '../libs/constants';
import { defaultInsertId } from '../../common/libs/constants';
import { formatDateTime } from '../../../../modules/utils';
import DatePicker from 'react-datepicker';
import NotificationPopup from '../../../common/components/NotificationPopup';

export class InsertUpdateForm extends PureComponent {
    constructor(props) {
        super(props)

        const language = props.languageList.find(l => l.LanguageCode === defaultState.language) ? defaultState.language : props.languageList[0].LanguageCode;
        const initState = Object.assign({}, { ...defaultState }, { language })
        this.state = {
            ...initState
        }
    }

    componentDidUpdate(prevProps) {
        const { selectedNewsLetterPageId } = this.props;
        if (prevProps.selectedNewsLetterPageId !== selectedNewsLetterPageId) {
            this.setState({ isInsert: selectedNewsLetterPageId === defaultInsertId });

            selectedNewsLetterPageId && this._handleGetNewsletterDetail(selectedNewsLetterPageId);
        }
    }

    _handleGetNewsletterDetail = async (selectedNewsLetterPageId) => {
        const item = await this.props.getNewsLetterPageDetail(selectedNewsLetterPageId);

        item && this.setState({
            fileName: item.FileName,
            fileSize: item.FileSize,
            fromDate: new Date(item.FromDate),
            toDate: new Date(item.ToDate),
            language: item.Language,
            initialFileName: item.FileName
        });
    }

    _handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ messageNotificationPopup: '' });

        if (confirmResult) {
            const newsLetterInfo = this._getParam();

            this._updateNewsLetter(newsLetterInfo);
        }
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
        if (fileName.length > maxLength) {
            this._handleShowAlert(false, "File name too long. Max length is 50!");
            return false;
        }
        if (fileName && !(rg1.test(fileName))) {
            this._handleShowAlert(false, "File name must not have characters \\ / : * ? $ \" < > | and extension is html!");
            return false;
        }
        if (!fileName) {
            this._handleShowAlert(false, "Please enter file name!");
            return false;
        }
        return true;
    }

    _validateDateRange = (fromDate, toDate) => {
        if (toDate <= fromDate) {
            this._handleShowAlert(false, "Effectective To Date must be greater than Effective From Date")
            return false
        }
        return true;
    }

    _insertNewsLetter = async (param) => {
        const result = await this.props.insertNewsLetterPage(param);
        this._handleAfterUpload(result);
    }

    _updateNewsLetter = async (param) => {
        const result = await this.props.updateNewsLetterPage(param);
        this._handleAfterUpload(result);
    }

    _save = async (param) => {
        const { fileName, initialFileName, isInsert } = this.state;
        if (isInsert) {
            await this._insertNewsLetter(param);
        }
        else {
            if (fileName === initialFileName) {
                this.setState({ messageNotificationPopup: 'This file name exists already. Do you want to overwrite the current file?' });
            } else {
                await this._updateNewsLetter(param);
            }
        }
    }

    _getParam = () => {
        const formatISO = "yyyy-MM-ddTHH:mm:ss.SSS"
        const { selectedFile, fileName, fileSize, fromDate, toDate, language } = this.state;
        const { selectedNewsLetterPageId } = this.props;
        return Object.assign({}, { newsLetterId: selectedNewsLetterPageId, file: selectedFile, fileName, fileSize, fromDate: formatDateTime(fromDate, formatISO), toDate: formatDateTime(toDate, formatISO), language });
    }

    _handleAfterUpload = (result) => {
        const { isInsert } = this.state;
        const message = isInsert ? "Your newsletter has been added successfully!" : "Your newsletter has been updated successfully!";

        if (result) {
            this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
        } else {
            this._handleShowAlert(false, isInsert ? "Add Failed!" : "Update Failed!");
        }
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
        this.props.setSelectedNewsLetterPageId(null);

        this.setState({ ...defaultState });
    }

    render() {
        const { fromDate, toDate, fileName, language, isShowMessage, isSuccess, message, isInsert, messageNotificationPopup } = this.state;
        const { languageList } = this.props;
        const isShowPopup = this.props.selectedNewsLetterPageId !== null;
        const title = isInsert ? "Add Newsletter" : "Edit Newsletter";
        const submitBtnText = isInsert ? "Upload" : "Update"
        const displayDateFormat = "M/dd/yyyy HH:mm:ss"

        return (
            <React.Fragment>
                {
                    !!messageNotificationPopup && <NotificationPopup
                        message={messageNotificationPopup}
                        isConfirmPopup={true}
                        onClose={this._handleCloseNotificationPopup}
                    />
                }
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


                                <Form tag="form" className="mb-5" autoComplete="off">
                                    <FormGroup row className="mb-2 align-items-center" >
                                        <Label for="file" className="required" sm={4}>File</Label>
                                        <Col sm={6}>
                                            <Input type="file" name="file" id="file" accept=".html" onChange={this._handFileSelect} />
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
                                        <Label className="required" sm={4}>Language</Label>
                                        <Col sm={6}>
                                            <Input type="select" name="language" className="input-required mt-2" value={language} onChange={this._handleChange}>
                                                {languageList.map(lang =>
                                                    <option key={lang.LanguageCode} value={lang.LanguageCode}>{lang.LanguageName}</option>
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
                                        <Label for="toDate" sm={4} className="required">To (GMT-4)</Label>
                                        <Col sm={6}>
                                            <DatePicker name="toDate" showTimeInput dateFormat={displayDateFormat} selected={toDate} className='form-control' onChange={(value) => this._handleChangeDatePicker("toDate", value)} />
                                        </Col>
                                    </FormGroup>
                                </Form>

                                <Button type="button" color="success" className="mr-3" onClick={this._handleSubmit} >{submitBtnText}</Button>
                                <Button type="button" color="info" onClick={this._handleClosePopup}>{globalResource.Cancel}</Button>
                            </div>
                        </div>
                    </ModalBody>
                </Modal>
            </React.Fragment>
        );
    }
}

function mapStateToProps({ param, selectedNewsLetterPageId, languageList }) {
    return Object.assign({}, { param, selectedNewsLetterPageId, languageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedNewsLetterPageId, getNewsLetterPageDetail, fetchData, insertNewsLetterPage, updateNewsLetterPage, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(InsertUpdateForm);