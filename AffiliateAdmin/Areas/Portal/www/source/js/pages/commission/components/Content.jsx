import React, { Component } from "react";
import { connect } from "react-redux";

import { Modal } from 'react-bootstrap';
import GridData from './GridData';
import PopupContent from './PopupContent';
import Pager from '../../../components/Pager';

class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            showPopup: false,
            pageOption: {
                PageIndex: 1,
                PageSize: 20,
                TotalItem: 100,
                PageList: [10, 20, 50, 100, 500]
            }
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

        const pageOption = Object.assign({}, this.state.pageOption, {PageIndex: pageIndex, PageSize: pageSize});
        this.setState({pageOption});
    }

    render() {
        return (
            <div className="data-content">
                <div>
                    <a href="javascript:;" onClick={this.handleAddNewGroup} className="btn-add-comm">
                        Add New Group <span className="glyphicon glyphicon-triangle-left" aria-hidden="true"></span>
                    </a>
                </div>
                <div className="clearfix"></div>
                <Pager pageOption={this.state.pageOption} onPaging={this.onPaging} />
                <div className="content-container table-responsive">
                    <GridData />
                </div>

                <Modal show={this.state.showPopup} onHide={this.handleHide} bsSize="large" backdrop='static'>
                    <Modal.Header closeButton>
                        <Modal.Title><span className="iconpopup"></span>Add a Commission Group</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <div className="modal-body-wrapper">
                            <PopupContent />
                        </div>
                    </Modal.Body>
                </Modal>
            </div>
        );
    }
}

export default connect()(Content);