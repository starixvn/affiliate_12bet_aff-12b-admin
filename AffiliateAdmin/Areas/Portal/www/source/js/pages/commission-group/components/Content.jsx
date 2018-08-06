import React, { Component } from "react";

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Modal, ModalHeader, ModalBody, Button } from 'reactstrap';
import GridData from './GridData';
import PopupContent from './PopupContent';
import Pager from '../../../components/Pager';

import { getGroupSettingList } from '../actions';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            showPopup: false
        }
    }

    handleAddNewGroup = () => {
        this.setState({ showPopup: true });
    }

    handleHide = () => {
        this.setState({ showPopup: false });
    }

    onPaging = (pageIndex, pageSize) => {
        console.log(pageIndex, pageSize);
        this.props.getGroupSettingList({ PageIndex: pageIndex, PageSize: pageSize });
    }

    render() {
        console.log(this.props.pagingOption);
        return (
            <div className="data-content">
                <div className="mx-2 my-2">
                    <Button outline type="button" onClick={this.handleAddNewGroup}>Add New Group</Button>
                </div>
                
                <Pager pageOption={this.props.pagingOption} onPaging={this.onPaging} />
                <div className="my-1">
                    <GridData />
                </div>

                <Modal isOpen={this.state.showPopup} toggle={this.handleHide} size="lg" backdrop='static'>
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

function mapStateToProps({ pagingOption }) {
    return Object.assign({}, { pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getGroupSettingList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);