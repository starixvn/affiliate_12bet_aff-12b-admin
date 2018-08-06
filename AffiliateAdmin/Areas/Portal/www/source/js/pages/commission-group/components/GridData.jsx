import React, { Component } from 'react';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { getGroupSettingList } from '../actions';
import { INFINITY_AM, INFINITY_QM, INFINITY_SYMBOL } from '../modules/constant';
import { format, formatDecimal } from '../../../modules/numberFormat';
import { defaultPagingOption } from '../modules/constant';

const htmlInfinity = (<span dangerouslySetInnerHTML={{ __html: INFINITY_SYMBOL }} />);

export class GridData extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.getGroupSettingList(defaultPagingOption);
    }

    _renderGroupData() {
        const { gridData } = this.props;
        console.log("GridData render", gridData);
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

                        {isFirstItem && (<td className="text-center text-bold cell-tier" rowSpan={tierSettingRowSpan}>Tier Setting</td>)}
                        <td className="text-center cell-tier">{tierSetting.TierId}</td>
                        <td className="text-right cell-tier">{format(tierSetting.MinAmount)}</td>
                        <td className="text-right cell-tier">{tierSetting.MaxAmount === INFINITY_AM ? htmlInfinity : format(tierSetting.MaxAmount)}</td>
                        <td className="text-right cell-tier">{format(tierSetting.MinQm)}</td>
                        <td className="text-right cell-tier">{tierSetting.MaxQm === INFINITY_QM ? htmlInfinity : format(tierSetting.MaxQm)}</td>
                        <td className="text-right cell-tier">{formatDecimal(tierSetting.Commission)}</td>

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
                                <a href="javacript:;" className="mr-2"><span className="oi oi-pencil"></span></a>
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
                        {isFirstItem && (<td className="text-center text-bold cell-platform" rowSpan={platformFeeRowSpan}>Platform Fee</td>)}
                        <td className="cell-platform" colSpan="5">{platformFee.ProductIds}</td>
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

function mapStateToProps({ gridData }) {
    return Object.assign({}, { gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getGroupSettingList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
