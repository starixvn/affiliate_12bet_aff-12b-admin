import React, { Component } from "react";

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Modal, ModalHeader, ModalBody, Button } from 'reactstrap';
import GridData from './GridData';
import PopupContent from './PopupContent';
import Pager from '../../../components/Pager';

import { getGroupSettingList, setSelectedGroup } from '../actions';

export class Content extends Component {
    constructor(props) {
        super(props);
    }

    handleAddNewGroup = () => {
        this.props.setSelectedGroup({});
    }

    handleHide = () => {
        this.props.setSelectedGroup(null);
    }

    onPaging = (pageIndex, pageSize) => {
        this.props.getGroupSettingList({ PageIndex: pageIndex, PageSize: pageSize });
    }

    render() {
        const showPopup = !!this.props.selectedGroup;

        return (
            <div className="data-content">
                <div className="mx-2 my-2">
                    <Button outline type="button" onClick={this.handleAddNewGroup}>Add New Group</Button>
                </div>
                
                <Pager pageOption={this.props.pagingOption} onPaging={this.onPaging} />
                <div className="my-1">
                    <GridData />
                </div>

                <Modal isOpen={showPopup} toggle={this.handleHide} size="lg" backdrop='static'>
                    <ModalHeader toggle={this.handleHide}><span className="iconpopup"></span>Add a Commission Group</ModalHeader>
                    <ModalBody>
                        <div className="modal-body-wrapper">
                            <PopupContent />
                        </div>
                    </ModalBody>
                </Modal>
            </div>
        );
    }
}

function mapStateToProps({ pagingOption, selectedGroup }) {
    return Object.assign({}, { pagingOption, selectedGroup });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getGroupSettingList, setSelectedGroup }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);