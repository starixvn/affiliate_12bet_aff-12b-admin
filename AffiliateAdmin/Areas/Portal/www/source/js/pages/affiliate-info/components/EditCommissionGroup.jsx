import React, { Component } from "react";

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Modal, ModalHeader, ModalBody, Button } from 'reactstrap';
import EditGroup from '../../commission-group/components/editgroup';
import { setShowPopupCommGroup } from '../actions';
import { resolveClientUrl } from '../../../modules/utils';

export class EditCommissionGroup extends Component {
    constructor(props){
        super(props);

        this.modalRef = React.createRef();
    }

    handleHide = () => {
        this.props.setShowPopupCommGroup(false);
    }

    handleSave = () => {
        this.modalRef.current.scrollTo({ top: 0, behavior: "smooth" });
    }

    render() {
        const { showPopupCommGroup, commGroup, affId } = this.props;
        return (
            <div className="data-content">
                <Modal isOpen={showPopupCommGroup} toggle={() => this.props.setShowPopupCommGroup(false)} size="lg" backdrop='static'>
                    <ModalHeader toggle={() => this.props.setShowPopupCommGroup(false)}><span className="iconpopup"></span>Edit Commission Group for {commGroup}</ModalHeader>
                    <ModalBody>
                        <div className="modal-body-wrapper embed-responsive embed-responsive-4by3">
                            <iframe className="embed-responsive-item" src={resolveClientUrl(`Portal/CommissionGroup/AssignPrivateGroup?affId=${affId}`)} allowFullScreen></iframe>
                        </div>
                    </ModalBody>
                </Modal>
            </div>
        );
    }
}

function mapStateToProps({ showPopupCommGroup, affId, commGroup }) {
    return Object.assign({}, { showPopupCommGroup, affId, commGroup });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopupCommGroup }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(EditCommissionGroup);