import React, { Component } from 'react';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { fetchGroupSettingData, setGridData } from '../actions';
import { INFINITY_AM, INFINITY_QM, INFINITY_SYMBOL } from '../modules/constant';
import { format, formatDecimal } from '../../../modules/numberFormat';

const htmlInfinity = (<span dangerouslySetInnerHTML={{ __html: INFINITY_SYMBOL }} />);

export class GridData extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this._getData();
    }

    async _getData() {
        const response = await this.props.fetchGroupSettingData();

        this.props.setGridData(response.data.CommissionSettingList);
    }

    _renderGroupData() {
        const { gridData } = this.props;
        console.log(gridData);
        const rows = [];

        for (let index = 0; index < gridData.length; index++) {
            const item = gridData[index];

            const { TierSettingList, PlatformFeeList, RemainingFeeList, ...group } = item;
            const tierSettingRowSpan = TierSettingList.length;
            const platformFeeRowSpan = PlatformFeeList.length;
            const rowSpan = tierSettingRowSpan + platformFeeRowSpan;

            for (let iTier = 0; iTier < TierSettingList.length; iTier++) {
                const tierSetting = TierSettingList[iTier];
                const isFirstItem = iTier === 0;

                rows.push(
                    (<tr key={`row-${index}-${iTier}`}>
                        {isFirstItem && (<td className="text-center" rowSpan={rowSpan}>{group.RowIdx}</td>)}
                        {isFirstItem && (<td className="text-center" rowSpan={rowSpan}>{group.GroupName}</td>)}
                        {isFirstItem && (<td className="text-center" rowSpan={rowSpan}>{group.NoAffs}</td>)}
                        {isFirstItem && (<td className="text-center" rowSpan={rowSpan}>{group.Description}</td>)}
                        {isFirstItem && (<td className="text-center" rowSpan={rowSpan}><p>{group.Creator}</p><p>{group.CreatedDateText}</p></td>)}

                        {isFirstItem && (<td className="text-center text-bold" rowSpan={tierSettingRowSpan}>Tier Setting</td>)}
                        <td className="text-center">{tierSetting.TierId}</td>
                        <td className="text-right">{format(tierSetting.MinAmount)}</td>
                        <td className="text-right">{tierSetting.MaxAmount === INFINITY_AM ? htmlInfinity : format(tierSetting.MaxAmount)}</td>
                        <td className="text-right">{format(tierSetting.MinQm)}</td>
                        <td className="text-right">{tierSetting.MaxQm === INFINITY_QM ? htmlInfinity : format(tierSetting.MaxQm)}</td>
                        <td className="text-right">{formatDecimal(tierSetting.Commission)}</td>

                        {
                            isFirstItem && (RemainingFeeList.length === 2 ?
                            RemainingFeeList.map((fee, i) => (
                                <td key={`fee-${index}-${i}`} rowSpan={rowSpan} className="text-right">{fee.DeductedPercentage}</td>
                            )) :
                            (
                                <React.Fragment>
                                    <td rowSpan={rowSpan}></td>
                                    <td rowSpan={rowSpan}></td>
                                </React.Fragment>
                            ))
                        }
                        {isFirstItem && (<td className="text-right" rowSpan={rowSpan}>{formatDecimal(group.QmTurnover)}</td>)}

                        {isFirstItem && (
                            <td className="text-center" rowSpan={rowSpan}>
                                <a href="javacript:;"><span className="oi oi-pencil"></span></a>
                                <a href="javacript:;"><span className="oi oi-x"></span></a>
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
                        {isFirstItem && (<td className="text-center text-bold" rowSpan={platformFeeRowSpan}>Platform Fee</td>)}
                        <td colSpan="5">{platformFee.ProductIds}</td>
                        <td className="text-right">{formatDecimal(platformFee.DeductedPercentage)}</td>
                    </tr>)
                );
            }

        }

        return rows;
    }

    render() {
        return (
            <div className="table-responsive group-setting">
                <table className="table table-bordered table-hover table-sm" style={{ tableLayout: "fixed" }}>
                    <thead>
                        <tr>
                            <th rowSpan="2" width="3%"></th>
                            <th rowSpan="2">Name</th>
                            <th rowSpan="2">No.Aff</th>
                            <th rowSpan="2">Description</th>
                            <th rowSpan="2">Creation</th>
                            <th colSpan="10">Policy</th>
                            <th rowSpan="2">Action</th>
                        </tr>
                        <tr>
                            <th></th>
                            <th width="2%">Tier</th>
                            <th title="Minimum all members' winlost amount">Min AM($)</th>
                            <th title="Maximum all members' winlost amount">Max AM($)</th>
                            <th title="Minimum number of qualified members">Min QM</th>
                            <th title="Maximum number of qualified members">Max QM</th>
                            <th>Comm Rate</th>
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

function mapStateToProps({ gridData }) {
    return Object.assign({}, { gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchGroupSettingData, setGridData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
