import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Input, Form, FormGroup, Label } from 'reactstrap';

import { setSelectedGroup } from '../../../actions';

import PlatformFeeItem from "./PlatformFeeItem";
import { deepClone, updateObjectInArray } from "../../../../../modules/dataHandler";
import { FIRST_ITEM_INDEX, ONLY_ONE_ITEM, groupTypeList } from '../../../modules/constant';
import { convertProductList, createDefaultProductGroupsDeductionFee, getDeductionSetting } from '../../../modules/service';

export class PlatformFeeSetting extends PureComponent {
    constructor(props) {
        super(props);

        const { deductionList, productList } = this.props;
        const deductionSetting = getDeductionSetting(deductionList);
        const defaultPlatformSetting = createDefaultProductGroupsDeductionFee(productList, deductionSetting.platformFeeSetting);
        const defaultKillRateSetting = createDefaultProductGroupsDeductionFee([], deductionSetting.killRateFeeSetting);

        this.defaultPlatformItem = this.createDefaultProductGroupsDeductionFeeItem(defaultPlatformSetting);
        this.defaultKillRateItem = this.createDefaultProductGroupsDeductionFeeItem(defaultKillRateSetting);
    }

    createDefaultProductGroupsDeductionFeeItem = (settings) => {
        return {
            DeductionId: settings.deductionId,
            ProductIds: '',
            DeductedPercentage: settings.deductedPercentage
        }
    }

    handleAdd = () => {
        const { PlatformFeeList, KillRateFeeList } = this.props.selectedGroup;
        const groupProductId = Date.now();
        const platformFeeList = [...PlatformFeeList, { ...this.defaultPlatformItem, GroupProductId: groupProductId }];
        const killRateFeeList = [...KillRateFeeList, { ...this.defaultKillRateItem, GroupProductId: groupProductId }];

        this.props.setSelectedGroup({ PlatformFeeList: platformFeeList, KillRateFeeList: killRateFeeList });
    }

    handleRemove = (index) => {
        const { PlatformFeeList, KillRateFeeList } = this.props.selectedGroup;

        const platformFeeList = deepClone(PlatformFeeList);
        const killRateFeeList = deepClone(KillRateFeeList);

        const rootProductIds = platformFeeList[0].ProductIds.split(',').filter(p => !!p);
        const checkedProductIds = platformFeeList[index].ProductIds.split(',').filter(p => !!p);

        for (let productId of checkedProductIds) {
            rootProductIds.push(productId);
        }

        platformFeeList[0].ProductIds = rootProductIds.join(',');

        platformFeeList.splice(index, 1);
        killRateFeeList.splice(index, 1);

        this.props.setSelectedGroup({ PlatformFeeList: platformFeeList, KillRateFeeList: killRateFeeList });
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

                for (let i = 0; i < settingCount; i++) {
                    if (i !== index) {
                        platformFeeList[i].ProductIds = platformFeeList[i].ProductIds.split(',')
                            .filter(p => p && p !== selectedItem.ProductId.toString())
                            .join(',');
                    }
                }
            }
            else if (index != FIRST_ITEM_INDEX) {
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

    handleChangeCommission = (index, value, propName) => {
        const feeListOld = this.props.selectedGroup[propName];
        const newCommissionSetting = Object.assign({}, feeListOld[index], value);

        const feeListNew = updateObjectInArray(feeListOld, index, newCommissionSetting);

        this.props.setSelectedGroup({ [propName]: feeListNew });
    }

    handleCheck = (isActivePlayerSum) => {
        this.props.setSelectedGroup({ IsActivePlayerSum: isActivePlayerSum });
    }

    handleProductGroupsDeductionData(selectProducts) {
        const { PlatformFeeList, KillRateFeeList } = this.props.selectedGroup;

        const productGroupsDeductionData = PlatformFeeList.map((platformFee, index) => {
            return {
                productList: convertProductList(selectProducts, platformFee.ProductIds),
                royaltyFee: platformFee.DeductedPercentage,
                killRate: KillRateFeeList[index].DeductedPercentage,
                groupProductId: platformFee.GroupProductId
            }
        });

        return productGroupsDeductionData;
    }

    render() {
        const { productList, selectedGroup, selectedProductIds } = this.props;
        const { IsActivePlayerSum } = selectedGroup;
        const selectProducts = productList.filter(p => !selectedProductIds.find(i => i === p.ProductId));

        const productGroupsDeductionData = this.handleProductGroupsDeductionData(selectProducts);


        const isOnlyOnePlatformFeeRow = productGroupsDeductionData.length === ONLY_ONE_ITEM;
        const isTurnover = selectedGroup.GroupTypeId === groupTypeList.Turnover;

        return (
            <div className="platformfee-group">
                <div className="d-flex">
                    <p className="font-weight-bold mr-5">Product Groups</p>
                    <Form inline>
                        <FormGroup check>
                            <Label check>
                                <Input type="checkbox" name="IsActivePlayerSum" checked={IsActivePlayerSum}
                                    onChange={() => this.handleCheck(!IsActivePlayerSum)} />Count Active Player across all products(exclude Free Bet)
                            </Label>
                        </FormGroup>
                    </Form>
                </div>
                {
                    productGroupsDeductionData.map((item, i) => {
                        const isFirst = i === FIRST_ITEM_INDEX;
                        const handleClick = isFirst ? this.handleAdd : () => this.handleRemove(i);

                        return (
                            <PlatformFeeItem data={item} key={`platformfee-${i}`}
                                isOnlyOnePlatformFeeRow={isOnlyOnePlatformFeeRow}
                                onSelectProduct={(selectedItem) => this.handleSelect(i, selectedItem)}
                                onChangeCommission={(value, propName) => this.handleChangeCommission(i, value, propName)}
                                handleClick={handleClick}
                                isFirst={isFirst}
                                isTurnover={isTurnover}
                                index={i + 1} />
                        )
                    })
                }
            </div>
        )
    }
}

function mapStateToProp({ deductionList, productList, selectedGroup, selectedProductIds }) {
    return { deductionList, productList, selectedGroup, selectedProductIds };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(PlatformFeeSetting);