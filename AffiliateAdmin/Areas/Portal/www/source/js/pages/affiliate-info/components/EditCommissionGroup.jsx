import React, { Component } from "react";

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Modal, ModalHeader, ModalBody } from 'reactstrap';
import { setShowPopupCommGroup, setAffId, setCommGroup } from '../actions';
import { resolveClientUrl } from '../../../modules/utils';

export class EditCommissionGroup extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        window.addEventListener('message', this.handleClosePopupEvent);
    }

    componentWillUnmount() {
        window.removeEventListener('message', this.handleClosePopupEvent);
    }

    handleClosePopupEvent = (event) => {
        if (event.data === 'closePopup') {
            this.props.setShowPopupCommGroup(false);
            this.props.setAffId(0);
            this.props.setCommGroup('');
        }
    }

    render() {
        const { showPopupCommGroup, commGroup, affId } = this.props;
        const url = showPopupCommGroup ? resolveClientUrl(`Portal/CommissionGroup/AssignPrivateGroup?affId=${affId}`) : '';
        return (
            <div className="data-content">
                <Modal isOpen={showPopupCommGroup} toggle={() => this.props.setShowPopupCommGroup(false)} size="lg" backdrop='static'>
                    <ModalHeader toggle={() => this.props.setShowPopupCommGroup(false)}><span className="iconpopup"></span>Edit Commission Group for {commGroup}</ModalHeader>
                    <ModalBody>
                        <div className="modal-body-wrapper embed-responsive embed-responsive-4by3">
                            <iframe className="embed-responsive-item" src={url}></iframe>
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
    return bindActionCreators({ setShowPopupCommGroup, setAffId, setCommGroup }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(EditCommissionGroup);