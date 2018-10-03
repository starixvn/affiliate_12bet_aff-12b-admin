import React, { Component } from 'react';

import { Modal, ModalHeader, ModalBody, Button } from 'reactstrap';

import AppLayout from '../../../../layouts/AppLayout';
import GridData from './GridData';

import { post } from '../../../../modules/ajaxRequest';

export default class AppContainer extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: [],
            selectedPages: []
        }
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData = async () => {
        const url = 'Portal/LandingPage/GetLandingPageList';
        const response = await post(url);
        if (response) {
            console.log(response.data);
            const selectedPages = this._convertSelectedPages(response.data);
            this.setState({ data: response.data, selectedPages });
        }
        else {
            console.log(response);
        }
    }

    _convertSelectedPages(dataList) {
        const selectedPages = {};
        for (let i = 0; i < dataList.length; i++) {
            const itemId = dataList[i].Id;
            selectedPages[itemId] = false;
        }

        return selectedPages;
    }

    onCheckItem = (isChecked, pageId) => {
        this.setState((prevState => ({ selectedPages: Object.assign({}, prevState.selectedPages, { [pageId]: isChecked }) })), () => console.log(this.state.selectedPages));
    }

    onEdit = (pageId) => {
        console.log(pageId);
    }

    onToggleAll = (isCheckAll) => {
        const { selectedPages } = this.state;
        const newSelectedPages = Object.keys(selectedPages).reduce((accumulator, currentValue) => {
            accumulator[currentValue] = isCheckAll;

            return accumulator;
        }, {});

        console.log(newSelectedPages);
        this.setState({ selectedPages: newSelectedPages });
    }

    render() {
        const { data, selectedPages } = this.state;

        return (
            <AppLayout>
                <div className="landing-page-container my-3">
                    <div className="action-container my-2">
                        <Button outline type="button" color="danger" className="mr-2">Delete <span className="oi oi-x ml-2"></span></Button>
                        <Button outline type="button">Add <span className="oi oi-plus ml-2"></span></Button>
                    </div>
                    <GridData dataList={data}
                        onCheckItem={this.onCheckItem}
                        onEdit={this.onEdit}
                        selectedPages={selectedPages}
                        onToggleAll={this.onToggleAll} />
                </div>
            </AppLayout>
        )
    }
}