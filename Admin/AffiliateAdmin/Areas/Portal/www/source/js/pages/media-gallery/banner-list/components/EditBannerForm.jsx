import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedBannerId, getBannerDetail, fetchData, insertBanner, updateBanner, toggleLoader } from '../actions/index';
import { headerText, globalResource, deviceTypes, defaultState, defaultDevice, Casino, Sportsbook, Baccarat } from '../libs/constants';
import { defaultInsertId } from '../../common/libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { validateFileExtension } from '../libs/utils';

export class EditBannerForm extends PureComponent {
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
            inputFileName: htmlDecode(item.FileName),
            subCategory: item.SubCategory || '',
            category: item.Category,
            language: item.Language,
            currency: item.Currency,
            imageType: item.ImageType,
            affType: item.AffType,
            deviceType: deviceTypes[item.DeviceType] || deviceTypes[defaultDevice]
        });
    }

    _handleChange = (event) => {
        const value = event.target.value;
        const name = event.target.name;

        this.setState({ [name]: value });

        const isCategoryCasino = value === Casino;
        if (name === 'category' && (isCategoryCasino || value === Sportsbook)) {
            this.setState({ subCategory: isCategoryCasino ? Baccarat : 'Basketball' });
        }
    }


    _handleFile = (event) => {
        const { files } = event.target;

        if (files && files.length > 0) {
            const fileName = files[0].name;
            const result = validateFileExtension(fileName);

            this.setState({ fileName, inputFileName: fileName, isDisabled: !result.isOK });
            result.isOK ? this.setState({ imageType: result.imageType }) : this._handleShowAlert(false, result.message);
        } else {
            this.setState({ isDisabled: true, fileName: '', inputFileName: '' });
        }
    }

    _handleAfterSave = (result, isInsert) => {
        if (result) {
            const message = isInsert ? "Add Successfully!" : "Update Successfully!";

            this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
        } else {
            this._handleShowAlert(false, isInsert ? "Add Failed!" : "Update Failed!");
        }
    }

    _save = async () => {
        const { category, language, currency, deviceType, fileName, inputFileName, isInsert } = this.state;
        const { selectedBannerId } = this.props;
        const bannerInfo = Object.assign({}, { bannerId: selectedBannerId, fileName, inputFileName, category, language, currency, deviceType });

        if (isInsert) {
            const result = await this.props.insertBanner(bannerInfo);
            this._handleAfterSave(result, isInsert);

        } else {
            const result = await this.props.updateBanner(bannerInfo);
            this._handleAfterSave(result, isInsert);
        };
    }

    _handleSubmit = () => {
        this.props.toggleLoader(true);

        const { fileName, inputFileName, isInsert } = this.state;

        if (!!inputFileName && (!isInsert || !!fileName)) {
            this._save();
        } else {
            !!fileName ? this._save() : this._handleShowAlert(false, isInsert ? 'Please select banner to upload.' : "You must enter a file name for banner.");
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
        const { selectedBannerId, categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, imageTypeList, affTypeList } = this.props;
        const { inputFileName, category, subCategory, language, currency, imageType, affType, deviceType, isShowMessage, isSuccess, message, isInsert, isDisabled } = this.state;
        const isShowPopup = selectedBannerId !== null;
        const title = isInsert ? "Add Banner" : "Update Banner";
        const buttonLabel = isInsert ? "Upload" : "Update";
        const isCategoryCasino = category === Casino;
        const subCategoryList = (isCategoryCasino || category === Sportsbook) ? (isCategoryCasino ? subCategoryCasinoList : subCategorySportsbookList) : null;

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
                                    <Label sm={3} for="file">File</Label>
                                    <Col sm={9}>
                                        <Input type="file" name="file" id="file" className={`border ${ isInsert && isDisabled ? "text-danger" : "text-success"}`} onChange={this._handleFile} />
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} for="category">{headerText.Category}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="category" id="category" value={category} onChange={this._handleChange}>
                                            {
                                                categoryList.map(item => {
                                                    return (<option key={`category-${item.Name}`} value={item.Name}>{item.Name}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                {
                                    subCategoryList && (
                                        <FormGroup row className="mb-2">
                                            <Label sm={3} for="subCategory">{headerText.SubCategory}</Label>
                                            <Col sm={5}>
                                                <Input type="select" name="subCategory" id="subCategory" value={subCategory} onChange={this._handleChange}>
                                                    {
                                                        subCategoryList.map(item => {
                                                            return (<option key={`subCategory-${item.Name}`} value={item.Name}>{item.Name}</option>)
                                                        })
                                                    }
                                                </Input>
                                            </Col>
                                        </FormGroup>
                                    )
                                }
                                <FormGroup row className="mb-2">
                                    <Label sm={3} for="language">{headerText.Language}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="language" id="language" value={language} onChange={this._handleChange}>
                                            {
                                                languageList.map(item => {
                                                    return (<option key={`language-${item.LanguageName}`} value={item.LanguageName}>{item.LanguageName}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} for="currency">{headerText.Currency}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="currency" id="currency" value={currency} onChange={this._handleChange}>
                                            {
                                                currencyList.map(item => {
                                                    return (<option key={`currency-${item.Name}`} value={item.Name}>{item.Name}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} for="imageType">{headerText.Type}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="imageType" id="imageType" value={imageType} onChange={this._handleChange}>
                                            {
                                                imageTypeList.map(item => {
                                                    return (<option key={`imageType-${item.Name}`} value={item.Name}>{item.Name}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} for="affType">{headerText.AffType}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="affType" id="affType" value={affType} onChange={this._handleChange}>
                                            {
                                                affTypeList.map(item => {
                                                    return (<option key={`affType-${item.AffTypeId}`} value={item.AffTypeId}>{item.AffTypeName}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} for="deviceType">{headerText.Device}</Label>
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
                                <FormGroup row className="mb-2">
                                    <Label sm={3} for="inputFileName">File Name</Label>
                                    <Col sm={7}>
                                        <Input type="text" name="inputFileName" id="inputFileName" value={inputFileName} onChange={this._handleChange} />
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" disabled={(isInsert || !inputFileName) ? isDisabled : false} onClick={this._handleSubmit}>{buttonLabel}</Button>
                            <Button type="button" color="info" onClick={this._handleClosePopup}>{globalResource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedBannerId, param, categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, imageTypeList, sizeList, affTypeList }) {
    return Object.assign({}, { selectedBannerId, param, categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, imageTypeList, sizeList, affTypeList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedBannerId, getBannerDetail, fetchData, insertBanner, updateBanner, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(EditBannerForm);