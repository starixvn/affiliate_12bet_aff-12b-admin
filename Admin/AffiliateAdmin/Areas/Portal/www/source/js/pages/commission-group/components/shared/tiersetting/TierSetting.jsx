import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../../actions';

import TierSettingItem from './TierSettingItem';
import { removeItem } from '../../../../../modules/dataHandler';
import { isValidNumber } from '../../../../../modules/utils';
import { INFINITY_AM, INFINITY_QM, globalResource } from '../../../modules/constant';

export class TierSetting extends Component {
    constructor(props) {
        super(props);
    }

    handleAdd = () => {
        const { TierSettingList } = this.props.selectedGroup;
        let defaultMinAmount = 0;
        let defaultMinQm = 0;

        const itemCount = TierSettingList.length;
        if (itemCount > 0) {
            const lastItem = TierSettingList[itemCount - 1];
            defaultMinAmount = lastItem.MaxAmount + 1;
            defaultMinQm = lastItem.MaxQm + 1;
        }

        const newTierSetting = {
            TierId: itemCount + 1,
            MinAmount: defaultMinAmount,
            MaxAmount: defaultMinAmount,
            MinQm: defaultMinQm,
            MaxQm: defaultMinQm,
            Commission: 0
        };

        this.props.setSelectedGroup({ TierSettingList: [...TierSettingList, newTierSetting] });
    }

    handleRemove = (index) => {
        const { TierSettingList } = this.props.selectedGroup;
        const tierSettingList = removeItem(index, TierSettingList).map((item, index) => {
            return {
                ...item,
                TierId: index + 1
            }
        });

        this.props.setSelectedGroup({ TierSettingList: tierSettingList });
    }

    handleChangeInput = (tierId, tierItemValue) => {
        const { MinAmount, MaxAmount, MinQm, MaxQm } = tierItemValue;
        if ((!MinAmount || isValidNumber(MinAmount)) &&
            (!MaxAmount || isValidNumber(MaxAmount)) &&
            (!MinQm || isValidNumber(MinQm)) &&
            (!MaxQm || isValidNumber(MaxQm))) {
            const { TierSettingList } = this.props.selectedGroup;

            const tierSettingList = TierSettingList.map(item => {
                if (item.TierId === tierId) {
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
        const { TierSettingList } = this.props.selectedGroup;
        const canAddTier = TierSettingList.filter(item => item.MaxAmount === INFINITY_AM || item.MaxQm === INFINITY_QM).length > 0;

        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">Tier Setting</h6></legend>
                <table className="table table-bordered tier-setting-table" style={{ tableLayout: "fixed" }}>
                    <thead>
                        <tr>
                            <th style={{ width: 40 }}>Tier</th>
                            <th>Min AM</th>
                            <th>Max AM</th>
                            <th>Min QM</th>
                            <th>Max QM</th>
                            <th style={{ width: 85 }}>Commission</th>
                            <th style={{ width: 50 }}>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            TierSettingList.length ?
                                TierSettingList.map((item, i) => {
                                    return (<TierSettingItem data={item} key={`tier-${i}`}
                                        isDisabledInfinity={i < TierSettingList.length - 1}
                                        onRemove={() => this.handleRemove(i)}
                                        onChange={this.handleChangeInput} />);
                                }) :
                                (<tr><td colSpan="7" className="text-center">{globalResource.NoInfo}</td></tr>)
                        }
                    </tbody>
                </table>

                <div>
                    <button type="button" className="btn btn-addTier btn-secondary" onClick={this.handleAdd} disabled={canAddTier}>
                        <span className="oi oi-plus"></span> Add Tier
                    </button>
                </div>
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

export default connect(mapStateToProp, mapDispatchToProps)(TierSetting);