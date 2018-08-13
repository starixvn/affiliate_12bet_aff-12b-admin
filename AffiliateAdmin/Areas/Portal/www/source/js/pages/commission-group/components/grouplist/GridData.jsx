import React, { Component } from 'react';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { INFINITY_AM, INFINITY_QM, INFINITY_SYMBOL } from '../../modules/constant';
import { format, formatDecimal } from '../../../../modules/numberFormat';
import { defaultPagingOption } from '../../modules/constant';
import { deleteCommGroup, getGroupSettingList } from '../../actions';

function rawHtml(content) {
    return (<p dangerouslySetInnerHTML={{ __html: content }}></p>);
}

export class GridData extends Component {
    constructor(props) {
        super(props);

        const { productList } = this.props;
        this.products = {};
        for (let i = 0; i < productList.length; i++) {
            this.products[productList[i].ProductId] = productList[i].ProductName;
        }
    }

    _convertToProductNameText(productIdList) {
        return productIdList.split(',').reduce((accumulator, currentItem) => {
            const productName = this.products[currentItem.trim()];
            if (productName) {
                accumulator.push(productName);
            }

            return accumulator;
        }, []).join(',');
    }

    handleDelete = async (groupId, groupName) => {
        const isConfirmed = window.confirm('Are you sure you want to delete the commission group ' + groupName + ' ?');
        if (isConfirmed) {
            const response = await this.props.deleteCommGroup({ groupId });
            const result = response.data;

            if (result.IsSuccessful) {
                const gettingParam = Object.assign({}, defaultPagingOption);
                this.props.getGroupSettingList(gettingParam);

                window.alert("Delete Commission Group Successfully");
            }
            else {
                window.alert(result.Message);
            }
        }
    }

    _renderGroupData() {
        const { gridData } = this.props;
        const rows = [];

        for (let index = 0; index < gridData.length; index++) {
            const item = gridData[index];

            const { TierSettingList, PlatformFeeList, OperationalFee, BankingFee, ...group } = item;
            const tierSettingRowSpan = TierSettingList.length;
            const platformFeeRowSpan = PlatformFeeList.length;
            const rowSpan = tierSettingRowSpan + platformFeeRowSpan;

            for (let iTier = 0; iTier < TierSettingList.length; iTier++) {
                const tierSetting = TierSettingList[iTier];
                const isFirstItem = iTier === 0;

                rows.push(
                    (<tr key={`row-${index}-${iTier}`}>
                        {isFirstItem && (<td className="text-center" rowSpan={rowSpan}>{group.RowIdx}</td>)}
                        {isFirstItem && (<td className="text-center" rowSpan={rowSpan}>{rawHtml(group.GroupName)}</td>)}
                        {isFirstItem && (<td className="text-center" rowSpan={rowSpan}>{group.NoAffs}</td>)}
                        {isFirstItem && (<td className="text-center" rowSpan={rowSpan}>{rawHtml(group.Description)}</td>)}
                        {isFirstItem && (<td className="text-center" rowSpan={rowSpan}><p>{group.Creator}</p><p>{group.CreatedDateText}</p></td>)}

                        {isFirstItem && (<td className="text-center font-weight-bold cell-tier" rowSpan={tierSettingRowSpan}>Tier Setting</td>)}
                        <td className="text-center cell-tier">{tierSetting.TierId}</td>
                        <td className="text-right cell-tier">{format(tierSetting.MinAmount)}</td>
                        <td className="text-right cell-tier">{tierSetting.MaxAmount === INFINITY_AM ? INFINITY_SYMBOL : format(tierSetting.MaxAmount)}</td>
                        <td className="text-right cell-tier">{format(tierSetting.MinQm)}</td>
                        <td className="text-right cell-tier">{tierSetting.MaxQm === INFINITY_QM ? INFINITY_SYMBOL : format(tierSetting.MaxQm)}</td>
                        <td className="text-right cell-tier">{formatDecimal(tierSetting.Commission)}</td>

                        {isFirstItem && (<td className="text-right" rowSpan={rowSpan}>
                            {OperationalFee && formatDecimal(OperationalFee.DeductedPercentage)}</td>)}
                        {isFirstItem && (<td className="text-right" rowSpan={rowSpan}>
                            {BankingFee && formatDecimal(BankingFee.DeductedPercentage)}</td>)}
                        {isFirstItem && (<td className="text-right" rowSpan={rowSpan}>{formatDecimal(group.QmTurnover)}</td>)}

                        {isFirstItem && (
                            <td className="text-center" rowSpan={rowSpan}>
                                <a href="javacript:;" className="mr-2" onClick={() => this.props.onEdit(item.GroupId)}>
                                    <span className="oi oi-pencil text-secondary"></span>
                                </a>
                                <a href="javacript:;" onClick={() => this.handleDelete(item.GroupId, item.GroupName)}>
                                    <span className="oi oi-x text-danger"></span>
                                </a>
                            </td>)
                        }
                    </tr>)
                );
            }

            for (let iPlatformFee = 0; iPlatformFee < PlatformFeeList.length; iPlatformFee++) {
                const platformFee = PlatformFeeList[iPlatformFee];
                const isFirstItem = iPlatformFee === 0;

                rows.push(
                    (<tr key={`row-platform-${index}-${iPlatformFee}`}>
                        {isFirstItem && (<td className="text-center font-weight-bold cell-platform" rowSpan={platformFeeRowSpan}>Platform Fee</td>)}
                        <td className="cell-platform" colSpan="5">{this._convertToProductNameText(platformFee.ProductIds)}</td>
                        <td className="text-right cell-platform">{formatDecimal(platformFee.DeductedPercentage)}</td>
                    </tr>)
                );
            }

        }

        return rows;
    }

    render() {
        return (
            <div className="table-responsive table-group-setting">
                <table className="table table-bordered table-hover table-sm" style={{ tableLayout: "fixed" }}>
                    <thead>
                        <tr>
                            <th rowSpan="2" className="th-smallest"></th>
                            <th rowSpan="2" className="th-medium">Name</th>
                            <th rowSpan="2" className="th-small">No.Aff</th>
                            <th rowSpan="2" className="th-medium">Description</th>
                            <th rowSpan="2">Creation</th>
                            <th colSpan="10">Policy</th>
                            <th rowSpan="2" className="th-small">Action</th>
                        </tr>
                        <tr>
                            <th className="cell-tier"></th>
                            <th className="cell-tier th-smallest">Tier</th>
                            <th className="cell-tier" title="Minimum all members' winlost amount">Min AM($)</th>
                            <th className="cell-tier" title="Maximum all members' winlost amount">Max AM($)</th>
                            <th className="cell-tier" title="Minimum number of qualified members">Min QM</th>
                            <th className="cell-tier" title="Maximum number of qualified members">Max QM</th>
                            <th className="cell-tier">Comm Rate</th>
                            <th>Operational Fee</th>
                            <th>Banking Fee</th>
                            <th>Qm Turnover</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this._renderGroupData()}
                    </tbody>
                </table>
            </div>
        );
    }
}

function mapStateToProps({ gridData, productList }) {
    return Object.assign({}, { gridData, productList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ deleteCommGroup, getGroupSettingList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
