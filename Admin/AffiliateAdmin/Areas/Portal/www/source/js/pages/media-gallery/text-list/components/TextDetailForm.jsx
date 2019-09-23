import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedTextId, getTextDetail, fetchData, insertText, updateText, toggleLoader } from '../actions/index';
import { headerText, globalResource, deviceTypes, pcLaptop } from '../libs/constants';

export class TextDetailForm extends PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            textContent: '',
            category: '',
            language: '',
            currency: '',
            deviceType: deviceTypes[pcLaptop],
            isShowMessage: false,
            isSuccess: true,
            message: ''
        }
    }

    componentDidUpdate(prevProps) {
        const { selectedTextId } = this.props;
        if (selectedTextId && prevProps.selectedTextId !== selectedTextId) {

        }
    }

    _handleChange = (event) => {
        const value = event.target.value;
        const name = event.target.name;

        this.setState({ [name]: value });
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

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
            this.props.setSelectedTextId(null);
        }
    }

    render() {
        const { selectedTextId } = this.props;
        const { textContent, category, language, currency, deviceType, isShowMessage, isSuccess, message } = this.state;
        const isShowPopup = selectedTextId !== null;

        return (
            <Modal isOpen={isShowPopup} toggle={() => this.props.setSelectedTextId(null)} backdrop='static'>
                <ModalHeader toggle={() => this.props.setSelectedTextId(null)}><span className="iconpopup"></span>Add Text</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="text-list-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-2 p-1 border" autocomplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for={key} sm={2} className="d-flex justify-content-end align-items-center">Text</Label>
                                    <Col sm={10}>
                                        <Input type="textarea" name={key} id={key} rows="4"
                                            value={props.textContent} onChange={this._handleChangeMessageContent} disabled={isDisabled} />
                                    </Col>
                                </FormGroup>

                                <FormGroup row className="mb-2">
                                    <Label sm={4} for="category" className="mr-sm-2">{headerText.Category}</Label>
                                    <Input sm={8} type="select" name="category" id="category" value={category} onChange={(e) => setCategory(e.target.value)}>
                                        <option key={'category-all'} value={''}>{globalResource.All}</option>
                                        {
                                            Object.keys(categoryList).map(key => {
                                                return (<option key={`category-${key}`} value={key}>{key}</option>)
                                            })
                                        }
                                    </Input>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={4} for="language" className="mr-sm-2">{headerText.Language}</Label>
                                    <Input sm={8} type="select" name="language" id="language" value={language} onChange={(e) => setLanguage(e.target.value)}>
                                        <option key={'language-all'} value={''}>{globalResource.All}</option>
                                        {
                                            Object.keys(languageList).map(key => {
                                                return (<option key={`language-${key}`} value={languageList[key]}>{languageList[key]}</option>)
                                            })
                                        }
                                    </Input>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={4} for="currency" className="mr-sm-2">{headerText.Currency}</Label>
                                    <Input sm={8} type="select" name="currency" id="currency" value={currency} onChange={(e) => setCurrency(e.target.value)}>
                                        <option key={'currency-all'} value={''}>{globalResource.All}</option>
                                        {
                                            Object.keys(currencyList).map(key => {
                                                return (<option key={`deviceType-${key}`} value={key}>{key}</option>)
                                            })
                                        }
                                    </Input>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={4}>Type</Label>
                                    <Label sm={8}>Text</Label>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={4} for="deviceType" className="mr-sm-2">{headerText.Device}</Label>
                                    <Input sm={8} type="select" name="deviceType" id="deviceType" value={deviceType} onChange={(e) => setDeviceType(e.target.value)}>
                                        {
                                            Object.keys(deviceTypes).map(key => {
                                                return (<option key={`deviceType-${deviceTypes[key]}`} value={deviceTypes[key]}>{key}</option>)
                                            })
                                        }
                                    </Input>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" onClick={this._handleSubmit}>{globalResource.Update}</Button>
                            <Button type="button" color="info" onClick={() => this.props.setSelectedTextId(null)}>{globalResource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedTextId, param, categoryList, languageList, currencyList }) {
    return Object.assign({}, { selectedTextId, param, categoryList, languageList, currencyList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedTextId, getTextDetail, fetchData, insertText, updateText, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(TextDetailForm);