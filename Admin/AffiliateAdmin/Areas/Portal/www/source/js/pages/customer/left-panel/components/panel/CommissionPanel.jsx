import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import Panel from '../Panel';
import { Row, Col, Button } from 'reactstrap';

import { resource } from '../../modules/constants';
import { formatDecimalWithCurrencySymbol } from '../../../../../modules/numberFormat';
import { getAffiliateInfo } from '../../actions';
import CommissionGroupPopup from '../../../../common/components/CommissionGroupPopup';

export class CommissionPanel extends PureComponent {
    constructor(props) {
        super(props);
        const { affiliateInfo } = this.props;

        this.state = {
            isChangingCommissionGroup: false,
            isPrivateGroup: affiliateInfo.AffName.toLowerCase() === affiliateInfo.CommGroupName.toLowerCase()
        }
    }

    componentDidUpdate() {
        const { affiliateInfo } = this.props;
        const isPrivateGroup = affiliateInfo.AffName.toLowerCase() === affiliateInfo.CommGroupName.toLowerCase();
        this.setState({ isPrivateGroup });
    }

    toggleEditingCommissionPopup = (isChanging) => {
        this.setState({ isChangingCommissionGroup: isChanging })
    }

    render() {
        const { isChangingCommissionGroup, isPrivateGroup } = this.state;
        const { affiliateInfo } = this.props;

        return (
            <Panel heading={resource.Commission}>
                <Row>
                    <Col sm={7}>{resource.CommGroup}</Col>
                    <Col sm={5} className='text-right text-truncate pl-0'>
                        {
                            affiliateInfo.IsClosed ? (<span>{affiliateInfo.CommGroupName}</span>) :
                                (<Button color="link p-0 left-panel-btn-link" onClick={() => this.toggleEditingCommissionPopup(true)}>
                                    {affiliateInfo.CommGroupName}
                                </Button>)
                        }
                        <CommissionGroupPopup
                            showPopupCommGroup={isChangingCommissionGroup}
                            isPrivateGroup={isPrivateGroup}
                            affName={affiliateInfo.AffName}
                            affId={affiliateInfo.AffId}
                            setShowPopupCommGroup={this.toggleEditingCommissionPopup}
                            getAffiliateInfo={this.props.getAffiliateInfo}
                        />
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
    const { affiliateInfo } = leftPanel;

    return Object.assign({}, { affiliateInfo });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(CommissionPanel);