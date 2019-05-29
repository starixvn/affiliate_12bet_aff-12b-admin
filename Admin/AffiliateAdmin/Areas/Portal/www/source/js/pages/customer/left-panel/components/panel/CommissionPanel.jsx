import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import Panel from '../Panel';
import { Modal, ModalHeader, ModalBody, Row, Col, Input, Button } from 'reactstrap';

import { resolveClientUrl } from '../../../../../modules/utils';
import { resource } from '../../modules/constants';
import { formatDecimalWithCurrencySymbol } from '../../../../../modules/numberFormat';
import { getAffiliateInfo } from '../../../mainpage/actions/loadingDataAction';

export class CommissionPanel extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isChangingCommissionGroup: false
        }
    }

    componentDidMount() {
        window.addEventListener('message', this.handleClosePopupEvent);
    }

    componentWillUnmount() {
        window.removeEventListener('message', this.handleClosePopupEvent);
    }

    handleClosePopupEvent = (event) => {
        if (event.data === 'closePopupAssignCommissionGroup') {
            this.toggleEditingCommissionPopup(false);
            this.props.getAffiliateInfo(this.props.affName);
        }
    }

    toggleEditingCommissionPopup = (isChanging) => {
        this.setState({ isChangingCommissionGroup: isChanging })
    }

    render() {
        const { isChangingCommissionGroup } = this.state;
        const { affiliateInfo } = this.props;
        const url = resolveClientUrl(`Portal/CommissionGroup/AssignGroup?affid=${affiliateInfo.AffId}`);
        const title = `Change Commission Group For "${affiliateInfo.AffName}"`;
        return (
            <Panel heading={resource.Commission}>
                <Row>
                    <Col sm={7}>{resource.CommGroup}</Col>
                    <Col sm={5} className='text-right text-truncate pl-0'>
                        {
                            affiliateInfo.IsClosed ? (<span>{affiliateInfo.CommGroupName}</span>) :
                                (<Button color="link p-0" onClick={() => this.toggleEditingCommissionPopup(true)}>
                                    {affiliateInfo.CommGroupName}
                                </Button>)
                        }

                        <Modal isOpen={isChangingCommissionGroup} backdrop='static' style={{ maxWidth: 570 }}>
                            <ModalHeader toggle={() => this.toggleEditingCommissionPopup(false)}>
                                <span className="iconpopup"></span>{title}
                            </ModalHeader>
                            <ModalBody>
                                <div className="modal-body-wrapper" style={{ height: 283, padding: 0 }}>
                                    <iframe src={url} width="100%" height="100%" frameBorder="0"></iframe>
                                </div>
                            </ModalBody>
                        </Modal>
                    </Col>
                </Row>
                <Row>
                    <Col sm={7} className="pr-0">{resource.BalanceForward}</Col>
                    <Col sm={5} className="text-right text-break pl-0">{formatDecimalWithCurrencySymbol(affiliateInfo.BalanceBf)}</Col>
                </Row>
            </Panel>
        );
    }
}

function mapStateToProps({ leftPanel }) {
    const { affiliateInfo, affName } = leftPanel;

    return Object.assign({}, { affiliateInfo, affName });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(CommissionPanel);