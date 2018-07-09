import React, { Component } from "react";
import { connect } from "react-redux";

import { Modal } from 'react-bootstrap';
import GridData from './GridData';
import PopupContent from './PopupContent';

class Content extends Component {
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

    render() {
        return (
            <div className="data-content">
                <div>
                    <a href="javascript:;" className="abutton" onClick={this.handleAddNewGroup}><span>Add New Group</span></a>
                </div>
                <div className="clearfix"></div>
                <div className="content-container table-responsive">
                    <GridData />
                </div>

                <Modal show={this.state.showPopup} onHide={this.handleHide} bsSize="large" backdrop='static'>
                    <Modal.Header closeButton>
                        <Modal.Title><span className="iconpopup"></span>Add a Commission Group</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <div className="modal-body-wrapper">
                            <PopupContent/>
                        </div>
                    </Modal.Body>
                </Modal>
            </div>
        );
    }
}

export default connect()(Content);