import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedBannerId, getBannerDetail, fetchData, insertBanner, updateBanner, toggleLoader } from '../actions/index';
import { headerText, globalResource, deviceTypes, defaultState, defaultDevice, imageTypes, affTypes } from '../libs/constants';
import { defaultInsertId } from '../../common/libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';

export class BannerDetailForm extends PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            ...defaultState
        }
    }

    componentDidUpdate(prevProps) {
        const { selectedBannerId } = this.props;
        if (prevProps.selectedBannerId !== selectedBannerId) {
            this.setState({ isInsert: selectedBannerId === defaultInsertId });

            selectedBannerId && this._handleGetBannerDetail(selectedBannerId);
        }
    }

    _handleGetBannerDetail = async (selectedBannerId) => {
        const item = await this.props.getBannerDetail(selectedBannerId);

        this.setState({
            fileName: htmlDecode(item.FileName),
            category: item.Category,
            language: item.Language,
            currency: item.Currency,
            deviceType: deviceTypes[item.DeviceType] || deviceTypes[defaultDevice]
        });
    }

    _handleChange = (event) => {
        const value = event.target.value;
        const name = event.target.name;

        this.setState({ [name]: value });
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        const { fileName, isInsert } = this.state;

        if (!!fileName) {
            const { category, language, currency, deviceType } = this.state;
            const { selectedBannerId } = this.props;
            const message = isInsert ? "Add Successfully!" : "Update Successfully!";
            const bannerInfo = Object.assign({}, { bannerId: selectedBannerId, fileName, category, language, currency, deviceType });
            const result = isInsert ? await this.props.insertBanner(bannerInfo) : await this.props.updateBanner(bannerInfo);

            if (result) {
                this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
            } else {
                this._handleShowAlert(false, isInsert ? "Add Failed!" : "Update Failed!");
            }
        } else {
            this._handleShowAlert(false, isInsert ? 'Please select banner to upload.' : "Please select banner to update.");
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
        this.props.setSelectedBannerId(null);

        this.setState({ ...defaultState });
    }

    render() {
        const { selectedBannerId, categoryList, languageList, currencyList } = this.props;
        const { fileName, category, subCategory, language, currency, imageType, affType, deviceType, isShowMessage, isSuccess, message, isInsert } = this.state;
        const isShowPopup = selectedBannerId !== null;
        const title = isInsert ? "Add Banner" : "Update Banner";
        const buttonLabel = isInsert ? "Upload" : "Update";

        return (
            <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop='static'>
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="banner-list-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-2 p-1 border" autocomplete="off">
                                <Label>[Valid Types: .gif, .jpg, .png, .jpeg, .swf]</Label>
                                <Label className="d-block">[File Size Limit: 3MB]</Label>
                                <br />
                                <FormGroup row className="mb-2">
                                    <Label sm={2} for="category">{headerText.Category}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="category" id="category" value={category} onChange={this._handleChange}>
                                            {
                                                Object.keys(categoryList).map(key => {
                                                    return (<option key={`category-${key}`} value={key}>{key}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={2} for="subCategory">{headerText.SubCategory}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="subCategory" id="subCategory" value={subCategory} onChange={this._handleChange}>
                                            {
                                                Object.keys(categoryList).map(key => {
                                                    return (<option key={`subCategory-${key}`} value={key}>{key}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={2} for="language">{headerText.Language}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="language" id="language" value={language} onChange={this._handleChange}>
                                            {
                                                Object.keys(languageList).map(key => {
                                                    return (<option key={`language-${key}`} value={languageList[key]}>{languageList[key]}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={2} for="currency">{headerText.Currency}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="currency" id="currency" value={currency} onChange={this._handleChange}>
                                            {
                                                Object.keys(currencyList).map(key => {
                                                    return (<option key={`currency-${key}`} value={key}>{key}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={2} for="imageType">{headerText.Currency}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="imageType" id="imageType" value={imageType} onChange={this._handleChange}>
                                            {
                                                Object.keys(imageTypes).map(key => {
                                                    return (<option key={`imageType-${key}`} value={key}>{key}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={2} for="affType">{headerText.Currency}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="affType" id="affType" value={affType} onChange={this._handleChange}>
                                            {
                                                Object.keys(affTypes).map(key => {
                                                    return (<option key={`affType-${key}`} value={affTypes[key]}>{key}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={2} for="deviceType">{headerText.Device}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="deviceType" id="deviceType" value={deviceType} onChange={this._handleChange}>
                                            {
                                                Object.keys(deviceTypes).map(key => {
                                                    return (<option key={`deviceType-${deviceTypes[key]}`} value={deviceTypes[key]}>{key}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" onClick={this._handleSubmit}>{buttonLabel}</Button>
                            <Button type="button" color="info" onClick={this._handleClosePopup}>{globalResource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedBannerId, param, categoryList, languageList, currencyList, sizeList }) {
    return Object.assign({}, { selectedBannerId, param, categoryList, languageList, currencyList, sizeList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedBannerId, getBannerDetail, fetchData, insertBanner, updateBanner, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(BannerDetailForm);