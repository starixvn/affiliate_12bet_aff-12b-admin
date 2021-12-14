import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedId, getExclusiveAffDetail, fetchData, insertExclusiveAff, toggleLoader } from '../actions/index';
import { defaultState, globalResource, maxLength, maxFileSize } from '../libs/constants';
import { defaultInsertId } from '../../common/libs/constants';
import { formatDateTime } from '../../../../modules/utils';
import { htmlDecode } from '../../../../modules/htmlHelper';

export class InsertForm extends PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            ...defaultState
        }
    }

    _handleChange = (event) => {
        const value = event.target.value;
        const name = event.target.name;

        this.setState({ [name]: value });
    }

    _validateInputs = () => {
        const { affNames, exclusivePage } = this.state;
        return this._validateAffNames(affNames) && this._validateExclusivePage(exclusivePage);
    }

    _validateAffNames = (affNames) => {
        const rg1 = /^[^\\/$:\*\?"<>\|]+(.html)$/; // forbidden characters \ / : * ? $ " < > |
        if (!affNames) {
            this._handleShowAlert(false, "Please enter file name!");
            return false;
        }

        if (affNames && !(rg1.test(affNames))) {
            this._handleShowAlert(false, "File name must not have characters \\ / : * ? $ \" < > | and extension is html!");
            return false;
        }

        return true;
    }

    _validateExclusivePage = (exclusivePage) => {
        if (!exclusivePage) {
            this._handleShowAlert(false, "Please select Exclusive Page")
            return false
        }
        return true;
    }

    _insertExclusiveAff = async (param) => {
        const result = await this.props.insertExclusiveAff(param);
        this._handleAfterUpload(result);
    }

    _save = async (param) => {
        await this._insertExclusiveAff(param);
    }

    _getParam = () => {
        const formatISO = "yyyy-MM-ddTHH:mm:ss.SSS"
        const { selectedFile, fileName, fileSize, fromDate, toDate, language } = this.state;
        const { selectedId } = this.props;
        return Object.assign({}, { ExclusiveAffId: selectedId, file: selectedFile, fileName, fileSize, fromDate: formatDateTime(fromDate, formatISO), toDate: formatDateTime(toDate, formatISO), language });
    }

    _handleAfterUpload = (result) => {
        this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? "The exclusive page has been assigned to the selected affiliate(s)" : result.Message);
    }

    _handleSubmit = () => {
        this.props.toggleLoader(true);

        if (this._validateInputs()) {
            const param = this._getParam();
            this._save(param);
        }

        this.props.toggleLoader(false);
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
        const { affNames, exclusivePage, isShowMessage, isSuccess, message } = this.state;
        const { exclusivePageList } = this.props;
        const isShowPopup = this.props.selectedId !== null;

        return (
            <React.Fragment>
                <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop='static'>
                    <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>Assign Exclusive Page To Affiliate</ModalHeader>
                    <ModalBody>
                        <div className="modal-body-wrapper">
                            <div className="news-letter-form-container">
                                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                                </Alert>
                                <Form tag="form" className="mb-4" autoComplete="off">
                                    <FormGroup row>
                                        <Label className="required" sm={3}>Affiliate(s)</Label>
                                        <Col sm={9}>
                                            <Input type="textarea" rows="8" name="affNames" className="input-required mt-2" value={affNames} onChange={this._handleChange} />
                                            <div><Label>Max 500 affiliates, separated by [Comma], [Tab] or [Enter]</Label></div>
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-2">
                                        <Label className="required" sm={3}>Exclusive Page</Label>
                                        <Col sm={6}>
                                            <Input type="select" name="exclusivePage" className="input-required mt-2" value={exclusivePage} onChange={this._handleChange}>
                                                <option value="">Please select</option>
                                                {exclusivePageList && exclusivePageList.map(lang =>
                                                    <option key={lang.ExclusiveId} value={lang.ExclusiveId}>{lang.ExclusiveName}</option>
                                                )}
                                            </Input>
                                        </Col>
                                    </FormGroup>
                                </Form>

                                <Button type="button" color="success" className="mr-3" onClick={this._handleSubmit} >{globalResource.Submit}</Button>
                                <Button type="button" color="info" onClick={this._handleClosePopup}>{globalResource.Cancel}</Button>
                            </div>
                        </div>
                    </ModalBody>
                </Modal>
            </React.Fragment>
        );
    }
}

function mapStateToProps({ param, selectedId, exclusivePageList }) {
    return Object.assign({}, { param, selectedId, exclusivePageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedId, getExclusiveAffDetail, fetchData, insertExclusiveAff, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(InsertForm);