import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../../actions';

import FdmSettingItem from './FdmSettingItem';
import { removeItem } from '../../../../../../modules/dataHandler';
import { isValidNumber } from '../../../../../../modules/utils';
import { INFINITY_AM, INFINITY_QM, globalResource } from '../../../libs/constant';

export class FdmSetting extends Component {
    constructor(props) {
        super(props);
    }

    handleAdd = () => {
        const { FdmSettingList } = this.props.selectedGroup;
        let defaultMinNewFdm = 0;
        let defaultMinFdmDeposit = 0;

        const itemCount = FdmSettingList.length;
        if (itemCount > 0) {
            const lastItem = FdmSettingList[itemCount - 1];
            defaultMinNewFdm = lastItem.MaxNewFdm + 1;
            defaultMinFdmDeposit = lastItem.MaxFdmDeposit + 1;
        }

        const newFdmSetting = {
            FdmId: itemCount + 1,
            MinNewFdm: defaultMinNewFdm,
            MaxNewFdm: defaultMinNewFdm,
            MinFdmDeposit: defaultMinFdmDeposit,
            MaxFdmDeposit: defaultMinFdmDeposit,
            Commission: 0,
            MaxExtraComm: 0
        };

        this.props.setSelectedGroup({ FdmSettingList: [...FdmSettingList, newFdmSetting] });
    }

    handleRemove = (index) => {
        const { FdmSettingList } = this.props.selectedGroup;
        const fdmSettingList = removeItem(index, FdmSettingList).map((item, index) => {
            return {
                ...item,
                FdmId: index + 1
            }
        });

        this.props.setSelectedGroup({ FdmSettingList: fdmSettingList });
    }

    handleChangeInput = (fdmId, fdmItemValue) => {
        const { MinNewFdm, MaxNewFdm, MinFdmDeposit, MaxFdmDeposit } = fdmItemValue;
        if ((!MinNewFdm || isValidNumber(MinNewFdm)) &&
            (!MaxNewFdm || isValidNumber(MaxNewFdm)) &&
            (!MinFdmDeposit || isValidNumber(MinFdmDeposit)) &&
            (!MaxFdmDeposit || isValidNumber(MaxFdmDeposit))) {
            const { FdmSettingList } = this.props.selectedGroup;

            const fdmSettingList = FdmSettingList.map(item => {
                if (item.FdmId === fdmId) {
                    return {
                        ...item,
                        ...fdmItemValue
                    };
                }

                return item;
            });

            this.props.setSelectedGroup({ FdmSettingList: fdmSettingList });
        }
    }

    render() {
        const { FdmSettingList } = this.props.selectedGroup;
        const canAddSetting = FdmSettingList.filter(item => item.MaxNewFdm === INFINITY_QM || item.MaxFdmDeposit === INFINITY_AM).length > 0;

        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">FDM Setting</h6></legend>
                <table className="table table-bordered tier-setting-table" style={{ tableLayout: "fixed" }}>
                    <thead>
                        <tr>
                            <th>Min New FDM</th>
                            <th>Max New FDM</th>
                            <th>Min FDM's Deposit</th>
                            <th>Max FDM's Deposit</th>
                            <th style={{ width: 80 }}>Commission</th>
                            <th style={{ width: 90 }}>Max Extra Comm</th>
                            <th style={{ width: 50 }}>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            FdmSettingList.length ?
                                FdmSettingList.map((item, i) => {
                                    return (<FdmSettingItem data={item} key={`fdm-${i}`}
                                        isDisabledInfinity={i < FdmSettingList.length - 1}
                                        onRemove={() => this.handleRemove(i)}
                                        onChange={this.handleChangeInput} />);
                                }) :
                                (<tr><td colSpan="7" className="text-center">{globalResource.NoInfo}</td></tr>)
                        }
                    </tbody>
                </table>

                <div>
                    <button type="button" className="btn btn-addTier btn-secondary" onClick={this.handleAdd} disabled={canAddSetting}>
                        <span className="oi oi-plus"></span> Add
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

export default connect(mapStateToProp, mapDispatchToProps)(FdmSetting);