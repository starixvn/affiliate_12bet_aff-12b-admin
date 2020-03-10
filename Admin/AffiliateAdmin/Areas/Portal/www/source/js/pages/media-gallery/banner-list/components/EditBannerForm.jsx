import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedBannerId, getBannerDetail, fetchData, insertBanner, updateBanner, toggleLoader } from '../actions/index';
import { headerText, globalResource, defaultState, Casino, Sportsbook, regSpecialCharacters } from '../libs/constants';
import { defaultInsertId } from '../../common/libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import NotificationPopup from '../../../common/components/NotificationPopup';
import { validateFileExtension, validateInputFile } from '../libs/utils';
import MultipleSelect from '../../../../components/multiple-select';

const regexSpecialCharacters = new RegExp(regSpecialCharacters);

export class EditBannerForm extends PureComponent {
    constructor(props) {
        super(props)

        const { categoryList, currencyList, affTypeList, deviceTypeList } = this.props;

        this.state = {
            ...defaultState,
            categoryList,
            subCategoryList: null,
            currencyList,
            affTypeList,
            deviceTypeList
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
        const bannerInfo = await this.props.getBannerDetail(selectedBannerId);

        if (bannerInfo) {
            const { categoryList, subCategoryList, currencyList, affTypeList, deviceTypeList } = this.props;
            
            const fileName = htmlDecode(bannerInfo.FileName);
            const indexOfShortFileName = fileName.lastIndexOf('/') + 1;
            const shortFileName = fileName.substring(indexOfShortFileName, fileName.length);

            this.setState({
                inputFileNames: [shortFileName],
                categoryList: this.handleChangeMultipleSelected(categoryList, bannerInfo.Categories, true),
                subCategoryList: this.handleChangeMultipleSelected(subCategoryList.filter(s => bannerInfo.Categories.includes(s.parentCategoryName)), bannerInfo.SubCategories),
                language: bannerInfo.Language,
                currencyList: this.handleChangeMultipleSelected(currencyList, bannerInfo.Currencies),
                affTypeList: this.handleChangeMultipleSelected(affTypeList, bannerInfo.AffTypes),
                deviceTypeList: this.handleChangeMultipleSelected(deviceTypeList, bannerInfo.DeviceTypes),
                iniatalFileName: shortFileName
            });
        }
    }

    _handleChange = (event) => {
        const value = event.target.value;
        const name = event.target.name;

        this.setState({ [name]: value });
    }

    _handleChangeInputFileName = (event, index) => {
        const value = event.target.value;

        if(regexSpecialCharacters.test(value) || value.includes('\\')) {
            this._handleShowAlert(false, `A File Name can't contain any of the following characters: \\/:*?"<>|`);
        } else {
            let inputFileNames = [...this.state.inputFileNames];

            inputFileNames[index] = value;

            this.setState({ inputFileNames });
        }
    }

    _handleFile = (event) => {
        const { files } = event.target;

        if (files && files.length > 0) {
            let inputFileNames = [];
            let allResults = {
                isOK: true,
                message: ''
            };

            for (let i = 0; i < files.length; i++) {
                let fileName = files[i].name;
                inputFileNames.push(fileName);
                let result = validateFileExtension(fileName);

                if (!result.isOK) {
                    allResults = result;
                }
            }

            this.setState({ files, inputFileNames, isDisabled: !allResults.isOK });
            !allResults.isOK && this._handleShowAlert(false, allResults.message);
        } else {
            this.setState({ isDisabled: true, file: null, inputFileName: '' });
        }
    }

    _handleAfterSave = (isInsert, result) => {
        if (result) {
            const message = isInsert ? "Your banner has been added successfully!" : "Your banner has been updated successfully!";

            this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
        } else {
            this._handleShowAlert(false, isInsert ? "The add is failed!" : "The update is failed!");
        }
    }

    _save = async (hasSubCategory) => {
        const { inputFileNames, isInsert, iniatalFileName } = this.state;
        const bannerInfo = this._getParam(hasSubCategory);
        
        if (isInsert) {
            const result = await this.props.insertBanner(bannerInfo);
            this._handleAfterSave(isInsert, result);
        } else {
            if (inputFileNames[0] === iniatalFileName) {
                this.setState({ messageNotificationPopup: 'This file name exists already. Do you want to overwrite the current file?' });
            } else {
                this._updateBanner(bannerInfo);
            }
        };
    }

    _getParam = (hasSubCategory) => {
        const { files, inputFileNames, categoryList, subCategoryList, language, currencyList, affTypeList, deviceTypeList, isInsert } = this.state;

        const categories = categoryList.filter(item => item.checked).map(item => item.key).join(',');
        const subCategories = hasSubCategory ? subCategoryList.filter(item => item.checked).map(item => item.key).join(',') : '';
        const currencies = currencyList.filter(item => item.checked).map(item => item.key).join(',');
        const affTypes = affTypeList.filter(item => item.checked).map(item => item.key).join(',');
        const deviceTypes = deviceTypeList.filter(item => item.checked).map(item => item.key).join(',');
        const { selectedBannerId } = this.props;

        return Object.assign({}, { bannerId: selectedBannerId, files, inputFileNames: isInsert ? inputFileNames.map(item => item ? item.trim() : '').join('|') : inputFileNames[0] && inputFileNames[0].trim(),
            categories, subCategories, language, currencies, affTypes, deviceTypes });
    }

    _updateBanner = async (bannerInfo) => {
        const result = await this.props.updateBanner(bannerInfo);
        this._handleAfterSave(this.state.isInsert, result);
    }

    _validateInputFileName = () => {
        const { inputFileNames, isInsert, files } = this.state;

        let validInputFileNames = {
            isOK: true,
            message: ''
        };

        if (isInsert) {
            for (let i = 0; i < inputFileNames.length; i++) {
                let inputFileName = inputFileNames[i];

                if (inputFileName) {
                    let result = validateInputFile(inputFileName.trim());

                    if (!result.isOK) {
                        result.message = `File Name ${i + 1} incorrect.<br />` + result.message;
                        validInputFileNames = result;
                    }
                } else {
                    if (files && files.length > 0) {
                        let result = validateInputFile(files[i].name);

                        if (!result.isOK) {
                            validInputFileNames = result;
                        }
                    }else {
                        validInputFileNames = {
                            isOK: false,
                            message: 'You must browse a banner.'
                        };
                    }
                }
            }
        } else {
            const inputFileName = inputFileNames[0];
            if (inputFileName) {
                let result = validateInputFile(inputFileNames[0]);

                if (!result.isOK) {
                    validInputFileNames = result;
                }
            } else {
                if(files && files.length > 0) {
                    let result = validateInputFile(files[0].name);

                    if (!result.isOK) {
                        validInputFileNames = result;
                    }
                } else {
                    validInputFileNames = {
                        isOK: false,
                        message: 'You must either enter a file name for banner or browse other banner.'
                    };
                }
            }
        }

        return validInputFileNames;
    }

    _handleSubmit = (hasSubCategory) => {
        this.props.toggleLoader(true);

        const { categoryList, subCategoryList, currencyList, affTypeList, deviceTypeList } = this.state;

        const category = categoryList.filter(item => item.checked).map(item => item.key);
        const subCategory = hasSubCategory && subCategoryList.filter(item => item.checked);
        const currency = currencyList.filter(item => item.checked);
        const affType = affTypeList.filter(item => item.checked);
        const deviceType = deviceTypeList.filter(item => item.checked);

        if (!category.length) {
            this._handleShowAlert(false, "Please select Category.");
            return;
        }
        if (hasSubCategory && (category.indexOf(Casino) !== -1 || category.indexOf(Sportsbook) !== -1) && !subCategory.length) {
            this._handleShowAlert(false, "Please select Sub Category.");
            return;
        }
        if (!currency.length) {
            this._handleShowAlert(false, "Please select Currency.");
            return;
        }
        if (!affType.length) {
            this._handleShowAlert(false, "Please select Affiliate Type.");
            return;
        }
        if (!deviceType.length) {
            this._handleShowAlert(false, "Please select Device.");
            return;
        }

        const validInputFileNames = this._validateInputFileName();
        validInputFileNames.isOK ? this._save(hasSubCategory) : this._handleShowAlert(false, validInputFileNames.message)

        this.props.toggleLoader(false);
    }

    _handleShowAlert = (isSuccess, message) => {
        this.setState({
            message,
            isSuccess,
            isShowMessage: true,
        }, () => window.setTimeout(() => this._hideMessage(isSuccess), 3000));

        this.props.toggleLoader(false);
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
        const { categoryList, currencyList, affTypeList, deviceTypeList } = this.props;

        this.props.setSelectedBannerId(null);
        this.setState({ ...defaultState, categoryList, subCategoryList: null, currencyList, affTypeList, deviceTypeList });
    }

    handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ messageNotificationPopup: '' });

        if (confirmResult) {
            const bannerInfo = this._getParam();

            this._updateBanner(bannerInfo);
        }
    }

    _handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this._handleSubmit();
        }
    }

    handleChangeMultipleSelected = (initialList, selectedKey, isCategory) => {
        const selectedList = typeof(selectedKey) === "string" ? selectedKey.split(',').filter(p => !!p) : selectedKey;

        return initialList && initialList.map(item => {
            const { key, name, categoryName } = item;
            const checked = selectedList.indexOf(key.toString()) !== -1;

            return isCategory ? {
                categoryName,
                key,
                name,
                checked
            } : {
                    key,
                    name,
                    checked
                };
        })
    }

    handleChangeCategory = (item, selectedKey) => {
        const { categoryList } = this.props;
        const { subCategoryList } = this.state;

        const newCategoryList = this.handleChangeMultipleSelected(categoryList, selectedKey, true);
        const categoryNames = newCategoryList.filter(category => category.checked).map(category => category.categoryName);
        let newSubCategoryList = categoryNames.map(c => this.props.subCategoryList.filter(sub => sub.parentCategoryName == c)).reduce((accumulator, field) => {
            var newField = field.reduce((acc, item) => {
                var newItem = subCategoryList && subCategoryList.filter(sub => sub.key === item.key);

                return acc.concat((!!newItem && !!newItem[0]) ? newItem : item);
            }, []);
            return accumulator.concat(newField);
        }, []);

        this.setState({ categoryList: newCategoryList, subCategoryList: newSubCategoryList });
    }

    handleChangeSubCategory = (item, selectedKey) => {
        const { subCategoryList } = this.state;

        this.setState({ subCategoryList: this.handleChangeMultipleSelected(subCategoryList, selectedKey) });
    }

    handleChangeCurrency = (item, selectedKey) => {
        const { currencyList } = this.state;

        this.setState({ currencyList: this.handleChangeMultipleSelected(currencyList, selectedKey) });
    }

    handleChangeAffType = (item, selectedKey) => {
        const { affTypeList } = this.state;

        this.setState({ affTypeList: this.handleChangeMultipleSelected(affTypeList, selectedKey) });
    }

    handleChangeDevice = (item, selectedKey) => {
        const { deviceTypeList } = this.state;

        this.setState({ deviceTypeList: this.handleChangeMultipleSelected(deviceTypeList, selectedKey) });
    }

    render() {
        const { selectedBannerId, languageList } = this.props;
        const { inputFileNames, language, isShowMessage, isSuccess, message, isInsert, isDisabled, messageNotificationPopup, categoryList,
            subCategoryList, currencyList, affTypeList, deviceTypeList } = this.state;
        const isShowPopup = selectedBannerId !== null;
        const title = isInsert ? "Add Banner" : "Update Banner";
        const buttonLabel = isInsert ? "Upload" : "Update";
        const selectedCategory = categoryList.filter(item => item.checked).map(item => item.key);
        const hasSubCategory = selectedCategory.indexOf(Casino) !== -1 || selectedCategory.indexOf(Sportsbook) !== -1;

        return (
            <React.Fragment>
                {
                    !!messageNotificationPopup && <NotificationPopup
                        message={messageNotificationPopup}
                        isConfirmPopup={true}
                        onClose={this.handleCloseNotificationPopup}
                    />
                }
                <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop='static'>
                    <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                    <ModalBody>
                        <div className="modal-body-wrapper">
                            <div className="banner-list-form-container">
                                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                                </Alert>
                                <Form tag="form" className="mb-2 p-1 border" autocomplete="off">
                                    <Label>[Valid Types: .gif, .jpg, .png, .jpeg]</Label>
                                    <Label className="d-block">[File Size Limit: 3MB]</Label>
                                    <br />
                                    <FormGroup row className="mb-2">
                                        <Label sm={3} for="file" className={isInsert ? "required" : ""}>File</Label>
                                        <Col sm={9}>
                                            <Input type="file" name="file" id="file" multiple={isInsert} className={`border ${isInsert && isDisabled ? "text-danger" : "text-success"}`} onChange={this._handleFile} accept="image/jpg, image/gif, image/png, image/jpeg" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-2">
                                        <Label sm={3} for="category" className="required">{headerText.Category}</Label>
                                        <Col sm={5}>
                                            <MultipleSelect
                                                dataSource={categoryList}
                                                keyField="key"
                                                valueField="name"
                                                statusField="checked"
                                                maxDisplayItemCount={1}
                                                noneSelectedLabel="Please Select"
                                                onChange={this.handleChangeCategory} />
                                        </Col>
                                    </FormGroup>
                                    {
                                        hasSubCategory && subCategoryList && (
                                            <FormGroup row className="mb-2">
                                                <Label sm={3} for="subCategory" className="required">{headerText.SubCategory}</Label>
                                                <Col sm={5}>
                                                    <MultipleSelect
                                                        dataSource={subCategoryList}
                                                        keyField="key"
                                                        valueField="name"
                                                        statusField="checked"
                                                        maxDisplayItemCount={1}
                                                        noneSelectedLabel="Please Select"
                                                        onChange={this.handleChangeSubCategory} />
                                                </Col>
                                            </FormGroup>
                                        )
                                    }
                                    <FormGroup row className="mb-2">
                                        <Label sm={3} for="language">{headerText.Language}</Label>
                                        <Col sm={5}>
                                            <Input type="select" name="language" id="language" value={language} onChange={this._handleChange}>
                                                {
                                                    languageList && languageList.map(item => {
                                                        return (<option key={`language-${item.LanguageName}`} value={item.LanguageName}>{item.LanguageName}</option>)
                                                    })
                                                }
                                            </Input>
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-2">
                                        <Label sm={3} for="currency" className="required">{headerText.Currency}</Label>
                                        <Col sm={5}>
                                            <MultipleSelect
                                                dataSource={currencyList}
                                                keyField="key"
                                                valueField="name"
                                                statusField="checked"
                                                maxDisplayItemCount={1}
                                                noneSelectedLabel="Please Select"
                                                onChange={this.handleChangeCurrency} />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-2">
                                        <Label sm={3} for="affType" className="required">{headerText.AffType}</Label>
                                        <Col sm={5}>
                                            <MultipleSelect
                                                dataSource={affTypeList}
                                                keyField="key"
                                                valueField="name"
                                                statusField="checked"
                                                maxDisplayItemCount={1}
                                                noneSelectedLabel="Please Select"
                                                onChange={this.handleChangeAffType} />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-2">
                                        <Label sm={3} for="deviceType" className="required">{headerText.Device}</Label>
                                        <Col sm={5}>
                                            <MultipleSelect
                                                dataSource={deviceTypeList}
                                                keyField="key"
                                                valueField="name"
                                                statusField="checked"
                                                maxDisplayItemCount={1}
                                                noneSelectedLabel="Please Select"
                                                onChange={this.handleChangeDevice} />
                                        </Col>
                                    </FormGroup>
                                    {
                                        isInsert ? inputFileNames && inputFileNames.map((value, index) => {
                                            return (<FormGroup row className="mb-2" key={`FileName${index + 1}`}>
                                                <Label sm={3} for="inputFileName">{`File Name ${index + 1}`}</Label>
                                                <Col sm={7}>
                                                    <Input type="text" name="inputFileName" id={`inputFileName${index}`} value={value} onChange={(e) => this._handleChangeInputFileName(e, index)} onKeyPress={this._handlePressEnter} />
                                                </Col>
                                            </FormGroup>)
                                        }) : (
                                                <FormGroup row className="mb-2">
                                                    <Label sm={3} for="inputFileName">File Name</Label>
                                                    <Col sm={7}>
                                                        <Input type="text" name="inputFileName" id="inputFileName" value={inputFileNames && inputFileNames[0]} onChange={(e) => this._handleChangeInputFileName(e, 0)} onKeyPress={this._handlePressEnter} />
                                                    </Col>
                                                </FormGroup>
                                            )
                                    }
                                </Form>

                                <Button type="button" color="success" className="mr-3" disabled={((isInsert || (inputFileNames && !inputFileNames[0]))) ? isDisabled : false} onClick={() => this._handleSubmit(hasSubCategory)}>{buttonLabel}</Button>
                                <Button type="button" color="info" onClick={this._handleClosePopup}>{globalResource.Cancel}</Button>
                            </div>
                        </div>
                    </ModalBody>
                </Modal>
            </React.Fragment>
        );
    }
}

function mapStateToProps({ selectedBannerId, param, categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, sizeList, affTypeList, deviceTypeList, subCategoryList }) {
    return Object.assign({}, { selectedBannerId, param, categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, sizeList, affTypeList, deviceTypeList, subCategoryList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedBannerId, getBannerDetail, fetchData, insertBanner, updateBanner, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(EditBannerForm);