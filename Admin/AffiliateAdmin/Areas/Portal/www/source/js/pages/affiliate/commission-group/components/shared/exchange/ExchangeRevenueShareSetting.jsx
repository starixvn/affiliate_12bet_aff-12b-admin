import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../../actions';
import SharePercentSettingItem from './SharePercentSettingItem';

export class ExchangeRevenueShareSetting extends Component {
    constructor(props) {
        super(props);
    }

    handleSelectExchangeWinLossShare = (newExchangeWinLossShare) => {
        const { ExchangeWinLossShare } = this.props.selectedGroup;
        this.props.setSelectedGroup({
            ExchangeWinLossShare: Object.assign({}, ExchangeWinLossShare, newExchangeWinLossShare)
        });
    }

    handleSelectExchangeCommissionShare = (newExchangeCommissionShare) => {
        const { ExchangeCommissionShare } = this.props.selectedGroup;
        this.props.setSelectedGroup({
            ExchangeCommissionShare: Object.assign({}, ExchangeCommissionShare, newExchangeCommissionShare)
        });
    }

    render() {
        const { ExchangeWinLossShare, ExchangeCommissionShare, GroupId } = this.props.selectedGroup;
        const isCreating = GroupId === 0;

        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">Exchange Revenue Share</h6></legend>

                <SharePercentSettingItem label="Win Loss Share%"
                    onChangeCommission={this.handleSelectExchangeWinLossShare}
                    data={ExchangeWinLossShare}
                    isCreating={isCreating} />

                <SharePercentSettingItem label="Commission Share%"
                    onChangeCommission={this.handleSelectExchangeCommissionShare}
                    data={ExchangeCommissionShare}
                    isCreating={isCreating} />
            </fieldset>
        )
    }
}

function mapStateToProp({ selectedGroup }) {
    return { selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(ExchangeRevenueShareSetting);