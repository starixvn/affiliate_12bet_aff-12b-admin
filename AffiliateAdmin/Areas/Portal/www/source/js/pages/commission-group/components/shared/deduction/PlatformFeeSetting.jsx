import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../../actions';

import PlatformFeeItem from "./PlatformFeeItem";
import { deepClone, updateObjectInArray } from "../../../../../modules/dataHandler";
import { convertProductList, createDefaultPlatformfee, getDeductionSetting } from '../../../modules/service';

export class PlatformFeeSetting extends Component {
    constructor(props) {
        super(props);

        const { deductionList, productList } = this.props;
        const deductionSetting = getDeductionSetting(deductionList);
        const defaultPlatformSetting = createDefaultPlatformfee(productList, deductionSetting.platformFeeSetting);

        this.defaultItem = {
            DeductionId: defaultPlatformSetting.deductionId,
            ProductIds: '',
            DeductedPercentage: defaultPlatformSetting.commission,
            NextDeductedPercentage: defaultPlatformSetting.commission
        }
    }

    handleAdd = () => {
        const { PlatformFeeList } = this.props.selectedGroup;
        const platformFeeList = [...PlatformFeeList, { ...this.defaultItem }];

        this.props.setSelectedGroup({ PlatformFeeList: platformFeeList });
    }

    handleRemove = (index) => {
        const { PlatformFeeList } = this.props.selectedGroup;
        const platformFeeList = deepClone(PlatformFeeList);

        const rootProductIds = platformFeeList[0].ProductIds.split(',').filter(p => !!p);
        const checkedProductIds = platformFeeList[index].ProductIds.split(',').filter(p => !!p);

        for (let i = 0; i < checkedProductIds.length; i++) {
            const productId = checkedProductIds[i];

            rootProductIds.push(productId);
        }

        platformFeeList[0].ProductIds = rootProductIds.join(',');

        platformFeeList.splice(index, 1);

        this.props.setSelectedGroup({ PlatformFeeList: platformFeeList });
    }

    handleSelect = (index, selectedItem) => {
        const { PlatformFeeList } = this.props.selectedGroup;
        const isChecked = selectedItem.Checked;
        const platformFeeList = deepClone(PlatformFeeList);
        const settingCount = platformFeeList.length;

        if (settingCount > 1) {
            if (isChecked) {
                const currentProductIds = platformFeeList[index].ProductIds.split(',').filter(p => !!p);
                currentProductIds.push(selectedItem.ProductId.toString());
                platformFeeList[index].ProductIds = currentProductIds.join(',');

                for (let i = 0; i < platformFeeList.length; i++) {
                    if (i !== index) {
                        platformFeeList[i].ProductIds = platformFeeList[i].ProductIds.split(',')
                            .filter(p => p && p !== selectedItem.ProductId.toString())
                            .join(',');
                    }
                }
            }
            else {
                platformFeeList[index].ProductIds = platformFeeList[index].ProductIds.split(',')
                    .filter(p => p && p !== selectedItem.ProductId.toString())
                    .join(',');

                const firstItemProductIds = platformFeeList[0].ProductIds.split(',').filter(p => !!p);
                firstItemProductIds.push(selectedItem.ProductId.toString());

                platformFeeList[0].ProductIds = firstItemProductIds.join(',');
            }
        }

        this.props.setSelectedGroup({ PlatformFeeList: platformFeeList });
    }

    handleChangeCommission = (index, value) => {
        const { PlatformFeeList } = this.props.selectedGroup;
        const newCommissionSetting = Object.assign({}, PlatformFeeList[index], value);

        const platformFeeList = updateObjectInArray(PlatformFeeList, index, newCommissionSetting);

        this.props.setSelectedGroup({ PlatformFeeList: platformFeeList });
    }

    render() {
        const { productList, selectedGroup } = this.props;
        const { PlatformFeeList, GroupId } = selectedGroup;
        const platformFeeData = PlatformFeeList.map(item => {
            return {
                productList: convertProductList(productList, item.ProductIds),
                commission: item.DeductedPercentage,
                nextCommission: item.NextDeductedPercentage
            }
        });

        return (
            <div className="platformfee-group">
                <p className="font-weight-bold">Platform Fee %</p>
                {
                    platformFeeData.map((item, i) => {
                        const isFirst = i === 0;
                        const isInserting = GroupId === 0;
                        const handleClick = isFirst ? this.handleAdd : () => this.handleRemove(i);

                        return (
                            <PlatformFeeItem data={item} key={`platformfee-${i}`}
                                onSelectProduct={(selectedItem) => this.handleSelect(i, selectedItem)}
                                onChangeCommission={(value) => this.handleChangeCommission(i, value)}
                                handleClick={handleClick}
                                btnText={isFirst ? '+' : '-'}
                                isInserting={isInserting}>
                            </PlatformFeeItem>
                        )
                    })
                }
            </div>
        )
    }
}

function mapStateToProp({ deductionList, productList, selectedGroup }) {
    return { deductionList, productList, selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(PlatformFeeSetting);