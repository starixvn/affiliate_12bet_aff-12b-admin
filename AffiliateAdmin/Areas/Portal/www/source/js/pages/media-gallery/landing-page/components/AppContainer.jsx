import React, { Component } from 'react';

import { Button, Modal, ModalHeader, ModalBody } from 'reactstrap';

import AppLayout from '../../../../layouts/AppLayout';
import GridData from './GridData';
import InsertUpdateForm from './InsertUpdateForm';
import Loader from '../../../../components/Loader';

import { post } from '../../../../modules/ajaxRequest';

export default class AppContainer extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: [],
            selectedPages: {},
            popupForm: null,
            isShowLoader: false,
            popupTitle: ''
        }
    }

    componentDidMount() {
        this.fetchData();
    }

    toggleLoader(isShow) {
        this.setState({ isShowLoader: !!isShow });
    }

    fetchData = async () => {
        this.toggleLoader(true);
        const url = 'Portal/LandingPage/GetLandingPageList';
        const response = await post(url);
        if (response) {
            const selectedPages = this._convertSelectedPages(response.data);
            this.setState({ data: response.data, selectedPages, isShowLoader: false });
        }

        this.toggleLoader(false);
    }

    _convertSelectedPages(dataList) {
        const selectedPages = {};
        for (let i = 0; i < dataList.length; i++) {
            const itemId = dataList[i].PageId;
            selectedPages[itemId] = false;
        }

        return selectedPages;
    }

    handleHidePopup = () => {
        this.setState({ popupForm: null });
    }

    handleDelete = async () => {
        const pageIdList = this._getSelectedPageIdList();

        if (pageIdList.length === 0) {
            alert('Please select items to delete');
            return;
        }

        const isConfirmed = window.confirm('Are you sure to delete the selected items?');
        if (isConfirmed) {
            this.toggleLoader(true);
            const url = 'Portal/LandingPage/DeleteLandingPage';
            const param = {
                PageIds: pageIdList.join(',')
            }

            const response = await post(url, param);
            if (response) {
                if (response.data.IsSuccessful) {
                    alert('Delete Landing Page Successfully');
                    this.fetchData();
                }
                else {
                    alert(response.data.Message);
                }
            }

            this.toggleLoader(false);
        }
    }

    _getSelectedPageIdList() {
        const { selectedPages } = this.state;

        return Object.keys(selectedPages).reduce((accumulator, currentValue) => {
            if (selectedPages[currentValue]) {
                accumulator.push(currentValue);
            }

            return accumulator;
        }, []);
    }

    handleSave = async (dataItem) => {
        this.toggleLoader(true);
        const param = Object.assign({}, dataItem);
        const url = param.PageId ? 'Portal/LandingPage/UpdateLandingPage' : 'Portal/LandingPage/InsertLandingPage';

        const response = await post(url, param);
        if (response) {
            if (response.data.IsSuccessful) {
                const message = (param.PageId ? 'Update' : 'Insert') + ' Landing Page Successfully';
                alert(message);
                this.handleHidePopup();
                this.fetchData();
            }
            else {
                alert(response.data.Message);
            }
        }

        this.toggleLoader(false);
    }

    handleAdd = () => {
        const data = {
            PageName: '',
            PageCode: ''
        }

        this.setState({
            popupForm: (<InsertUpdateForm data={data} onCancel={this.handleHidePopup} onSave={this.handleSave} />),
            popupTitle: 'Add Landing Page'
        });
    }

    onEdit = async (pageId) => {
        this.toggleLoader(true);
        const url = 'Portal/LandingPage/GetLandingPageItem?pageId=' + pageId;
        const response = await post(url);
        if (response) {
            this.setState({
                popupForm: (<InsertUpdateForm onCancel={this.handleHidePopup} data={response.data} onSave={this.handleSave} />),
                popupTitle: 'Edit Landing Page'
            });
        }

        this.toggleLoader(false);
    }

    onCheckItem = (isChecked, pageId) => {
        this.setState((prevState => ({
            selectedPages: Object.assign({}, prevState.selectedPages, { [pageId]: isChecked })
        })));
    }

    onToggleAll = (isCheckAll) => {
        const { selectedPages } = this.state;
        const newSelectedPages = Object.keys(selectedPages).reduce((accumulator, currentValue) => {
            accumulator[currentValue] = isCheckAll;

            return accumulator;
        }, {});

        this.setState({ selectedPages: newSelectedPages });
    }

    render() {
        const { data, selectedPages, popupForm, isShowLoader, popupTitle } = this.state;
        const showPopup = popupForm !== null;

        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <div className="landing-page-container my-3">
                    <div className="action-container my-2">
                        <Button outline type="button" color="danger" className="mr-2" onClick={this.handleDelete}>Delete <span className="oi oi-x ml-2"></span></Button>
                        <Button outline type="button" onClick={this.handleAdd}>Add <span className="oi oi-plus ml-2"></span></Button>
                    </div>
                    <GridData dataList={data}
                        onCheckItem={this.onCheckItem}
                        onEdit={this.onEdit}
                        selectedPages={selectedPages}
                        onToggleAll={this.onToggleAll} />

                    <Modal isOpen={showPopup} toggle={this.handleHidePopup} backdrop='static'>
                        <ModalHeader toggle={this.handleHidePopup}><span className="iconpopup"></span>{popupTitle}</ModalHeader>
                        <ModalBody>
                            <div className="modal-body-wrapper">
                                {popupForm}
                            </div>
                        </ModalBody>
                    </Modal>
                </div>
            </AppLayout>
        )
    }
}