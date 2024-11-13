import React, { PureComponent } from 'react';
import { Modal, ModalHeader, ModalBody } from 'reactstrap';
import { resolveClientUrl } from '../../../modules/utils';
import { popupEventNames } from '../modules/constants/commissionGroup';

export default class CommissionGroupPopup extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            isUsingPrivateGroup: false,
            privateGroupTypeId: 1
        }
    }

    componentDidMount() {
        window.addEventListener('message', this.handleClosePopupEvent);
    }

    componentWillUnmount() {
        window.removeEventListener('message', this.handleClosePopupEvent);
    }

    handleClosePopupEvent = (event) => {

        if (typeof event.data === 'string') {
            if (event.data === popupEventNames.updatePrivateGroupSuccessfully) {
                this.handleUpdatePrivateGroupSuccessfully();
            }

            if (event.data === popupEventNames.assignGroupSuccessfully) {
                this.handleAssignGroupSuccessfully();
            }
        } else if (event.data.eventName === popupEventNames.changeAssignToPrivateGroup) {
            this.handleChangeAssignToPrivateGroup(event.data.groupTypeId);
        }
    }

    handleUpdatePrivateGroupSuccessfully = () => {
        this.handleClosePopup();
        if (this.state.isUsingPrivateGroup) {
            this.props.getAffiliateInfo(this.props.affName);
        }
    }

    handleAssignGroupSuccessfully = () => {
        this.props.getAffiliateInfo(this.props.affName);
        this.props.setShowPopupCommGroup(false);
    }

    handleChangeAssignToPrivateGroup = (groupTypeId) => {
        this.setState({
            isUsingPrivateGroup: true,
            privateGroupTypeId: groupTypeId
        });
    }

    handleClosePopup = () => {
        this.props.setShowPopupCommGroup(false);
        window.setTimeout(this.resetState, 500);
    }

    resetState = () => {
        this.setState({
            isUsingPrivateGroup: false,
            privateGroupTypeId: 1
        });
    }

    render() {
        const { isUsingPrivateGroup, privateGroupTypeId } = this.state;
        const { showPopupCommGroup, affName, affId, groupTypeId, isPrivateGroup, isNewAffiliate } = this.props;

        const isPrivate = isPrivateGroup || isUsingPrivateGroup;
        const assignTitle = isNewAffiliate ? 'Assign Revenue Group For New Affiliate' : `Change Revenue Group For ${affName}`;
        const title = isPrivateGroup ? `Edit the Revenue Group for ${affName}` : assignTitle;
        const assignPrivateGroupTypeId = isPrivateGroup ? groupTypeId : privateGroupTypeId;

        const url = isPrivate ? resolveClientUrl(`Portal/CommGroupAssignment/AssignPrivateGroup?affId=${affId}&groupTypeId=${assignPrivateGroupTypeId}`) : resolveClientUrl(`Portal/CommGroupAssignment/AssignGroup?affId=${affId}&groupTypeId=${groupTypeId}`);
        const size = isPrivate ? 'lg' : '';
        const maxWidth = isPrivate ? '' : 650;

        return (
            <div className="data-content">
                <Modal isOpen={showPopupCommGroup} toggle={this.handleClosePopup} backdrop='static' size={size} style={{ maxWidth }}>
                    <ModalHeader toggle={this.handleClosePopup}>
                        <span className="iconpopup"></span>{isUsingPrivateGroup ? `Approve affiliate ${affName} with a private Revenue group` : title}
                    </ModalHeader>
                    <ModalBody>
                        {
                            isPrivate ? (<div className="modal-body-wrapper embed-responsive embed-responsive-4by3" style={{ height: 635 }}>
                                <iframe className="embed-responsive-item" src={url}></iframe>
                            </div>) : (
                                <div className="modal-body-wrapper" style={{ height: 340, padding: 0 }}>
                                    <iframe src={url} width="100%" height="100%" frameBorder="0"></iframe>
                                </div>
                            )
                        }
                    </ModalBody>
                </Modal>
            </div>
        );
    }
}