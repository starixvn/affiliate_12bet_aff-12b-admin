import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import Panel from '../Panel';
import { Row, Col, Button } from 'reactstrap';

import { resource } from '../../modules/constants';
import { formatDecimalWithCurrencySymbol } from '../../../../../modules/numberFormat';
import { getAffiliateInfo } from '../../actions';
import CommissionGroupPopup from '../../../../common/components/CommissionGroupPopup';
import { htmlDecode } from '../../../../../modules/htmlHelper';
import { claimResource } from '../../../../../modules/claim';

export class CommissionPanel extends PureComponent {
    constructor(props) {
        super(props);
        const { affiliateInfo } = this.props;

        this.state = {
            isChangingCommissionGroup: false,
            isPrivateGroup: affiliateInfo.AffId === affiliateInfo.GroupId
        }
    }

    componentDidUpdate() {
        const { affiliateInfo } = this.props;
        const isPrivateGroup = affiliateInfo.AffId === affiliateInfo.GroupId;
        this.setState({ isPrivateGroup });
    }

    toggleEditingCommissionPopup = (isChanging) => {
        this.setState({ isChangingCommissionGroup: isChanging })
    }

    _renderCommGroup(affiliateInfo) {
        const { isChangingCommissionGroup, isPrivateGroup } = this.state;
        return affiliateInfo.IsClosed ? (<span className="">{htmlDecode(affiliateInfo.CommGroupName)}</span>) :
            (
                <React.Fragment>
                    <Button color="link" className="left-panel-btn-link align-baseline text-right" onClick={() => this.toggleEditingCommissionPopup(true)}>
                        {htmlDecode(affiliateInfo.CommGroupName)}
                    </Button>
                    <CommissionGroupPopup
                        showPopupCommGroup={isChangingCommissionGroup}
                        isPrivateGroup={isPrivateGroup}
                        affName={affiliateInfo.AffName}
                        affId={affiliateInfo.AffId}
                        setShowPopupCommGroup={this.toggleEditingCommissionPopup}
                        getAffiliateInfo={this.props.getAffiliateInfo}
                    />
                </React.Fragment>
            )
    }

    _renderStaticContent = (affiliateInfo) => {      
        return htmlDecode(affiliateInfo.CommGroupName)
    }

    render() {
        const { affiliateInfo, isStatic } = this.props;
        const commSelection = isStatic ? this._renderStaticContent(affiliateInfo) : this._renderCommGroup(affiliateInfo);
        return (
            <Panel heading={resource.Commission}>
                <Row>
                    <Col sm={7}>{resource.CommGroup}</Col>
                    <Col sm={5} className='text-right text-break pl-0'>
                        {commSelection}
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

function mapStateToProps({ leftPanel, claimsPrincipal }) {
    const { affiliateInfo } = leftPanel;
    const isStatic = claimsPrincipal.hasWriteClaim(claimResource.CommGroup);
    return Object.assign({}, { affiliateInfo, isStatic });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(CommissionPanel);