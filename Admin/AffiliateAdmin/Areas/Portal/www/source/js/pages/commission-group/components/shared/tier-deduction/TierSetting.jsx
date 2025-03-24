import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { Col, FormGroup } from 'reactstrap';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../../actions';

import TierSettingItem from './TierSettingItem';
import { removeTierItem } from '../../../modules/service';
import { isValidNumber } from '../../../../../modules/utils';
import { FIRST_ITEM_INDEX, MinNetWin, regexAcceptNegativeAmountFormat, tierSettingDefault } from '../../../modules/constant';

export class TierSetting extends PureComponent {
    constructor(props) {
        super(props);
    }

    handleAdd = () => {
        const { selectedGroup, groupProductId } = this.props;
        let defaultMinAmount = tierSettingDefault.MinAmount;
        let defaultCommission = tierSettingDefault.Commission;

        const tierSettingListByGroupProduct = selectedGroup.TierSettingList.filter(t => t.GroupProductId === groupProductId);
        const itemCount = selectedGroup.TierSettingList.filter(t => t.GroupProductId === groupProductId).length;
        if (itemCount > 0) {
            const lastItem = tierSettingListByGroupProduct[itemCount - 1];
            const minAmount = Number(lastItem.MinAmount);
            defaultMinAmount = (minAmount < 1 || Number.isNaN(minAmount)) ? 1 : minAmount + 1;
            defaultCommission = lastItem.Commission + 0.1;
        }

        const newTierSetting = {
            GroupId: selectedGroup.GroupId,
            TierId: itemCount + 1,
            GroupProductId: this.props.groupProductId,
            MinAmount: defaultMinAmount,
            MinQm: tierSettingDefault.MinQm,
            Commission: defaultCommission
        };

        this.props.setSelectedGroup({ TierSettingList: [...selectedGroup.TierSettingList, newTierSetting] });
    }

    handleRemove = (tierId, groupProductId) => {
        const { TierSettingList } = this.props.selectedGroup;
        const tierSettingList = removeTierItem(tierId, groupProductId, TierSettingList);

        this.props.setSelectedGroup({ TierSettingList: tierSettingList });
    }

    handleChangeInput = (tierId, tierItemValue) => {
        const { MinAmount, MinQm } = tierItemValue;

        if ((!MinAmount || regexAcceptNegativeAmountFormat.test(MinAmount)) &&
            (!MinQm || isValidNumber(MinQm))) {
            const { selectedGroup, groupProductId } = this.props;

            const tierSettingList = selectedGroup.TierSettingList.map(item => {
                if (item.TierId === tierId && item.GroupProductId === groupProductId) {
                    return {
                        ...item,
                        ...tierItemValue
                    };
                }

                return item;
            });

            this.props.setSelectedGroup({ TierSettingList: tierSettingList });
        }
    }

    render() {
        const { selectedGroup, groupProductId, isDisableAddButton, handleClick, btnText } = this.props;
        const tierSettings = selectedGroup.TierSettingList.filter(t => t.GroupProductId === groupProductId)

        return (
            <div className='my-2'>
                <FormGroup row>
                    <Col sm={9} className="pl-0">
                        <table className="table table-bordered tier-setting-table" style={{ width: '495px', marginLeft: 42 }}>
                            <thead>
                                <tr>
                                    <th style={{ width: 40 }}>Tier</th>
                                    <th>{MinNetWin}</th>
                                    <th>Min Qualified Player</th>
                                    <th style={{ width: 85 }}>Rev. Share%</th>
                                    <th style={{ width: 50 }}>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    !!tierSettings.length &&
                                    tierSettings.map((item, i) => {
                                        const isFirst = i === FIRST_ITEM_INDEX;
                                        const handleClick = isFirst ? this.handleAdd : () => this.handleRemove(item.TierId, groupProductId);

                                        return (<TierSettingItem data={item} key={`tier-${i}`}
                                            handleClick={handleClick}
                                            btnText={isFirst ? '+' : '-'}
                                            isShowNegativeInfinity={isFirst}
                                            onChange={this.handleChangeInput} />);
                                    })
                                }
                            </tbody>
                        </table>
                    </Col>
                    <Col sm={3} className="pl-0">
                        <button
                            type="button"
                            className="btn btn-action"
                            style={{ width: '100%', display: 'flex', justifyContent: 'space-between', margin: 0 }}
                            disabled={isDisableAddButton}
                            onClick={handleClick}>
                            {btnText}
                        </button>
                    </Col>
                </FormGroup>
            </div>
        )
    }
}

function mapStateToProp({ selectedGroup }, { groupProductId }) {
    return { selectedGroup, groupProductId };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(TierSetting);