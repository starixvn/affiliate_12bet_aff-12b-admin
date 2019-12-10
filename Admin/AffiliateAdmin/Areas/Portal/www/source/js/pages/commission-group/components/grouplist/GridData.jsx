import React, { Component } from 'react';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { resolveClientUrl } from '../../../../modules/utils';
import { INFINITY_AM, INFINITY_QM, INFINITY_SYMBOL, globalResource } from '../../modules/constant';
import { format, formatDecimal } from '../../../../modules/numberFormat';
import { deleteCommGroup, getGroupSettingList } from '../../actions';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { NotificationPopup } from '../../../common/components/NotificationPopup';

export class GridData extends Component {
    constructor(props) {
        super(props);

        const { productList } = this.props;
        this.products = {};
        for (let i = 0; i < productList.length; i++) {
            this.products[productList[i].ProductId] = productList[i].ProductName;
        }

        this.state = {
            shouldResetScrollPosition: true,
            selectedGroupId: 0,
            messageNotification: '',
            isConfirmPopup: true
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

    handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ messageNotification: '' });

        const { selectedGroupId } = this.state;
        if (confirmResult && selectedGroupId) {
            this._deleteCommGroup(selectedGroupId);
        }
    }

    _handleDeleteCommGroup = (selectedGroupId, groupName) => {
        this.setState({ messageNotification: `Are you sure you want to delete the commission group ${groupName} ?`, isConfirmPopup: true, selectedGroupId, shouldResetScrollPosition: false });
    }

    _deleteCommGroup = async (groupId) => {
        const response = await this.props.deleteCommGroup({ groupId });
        if (response && response.status === 200) {
            const result = response.data;

            if (result.IsSuccessful) {
                const { pagingOption } = this.props;
                const gettingParam = Object.assign({}, { PageIndex: pagingOption.PageIndex, PageSize: pagingOption.PageSize, GroupName: this.props.searchedGroupName });
                this.props.getGroupSettingList(gettingParam);

                this.setState({ messageNotification: "Delete Commission Group Successfully", isConfirmPopup: false, shouldResetScrollPosition: true });
            }
            else {
                this.setState({ messageNotification: result.Message, isConfirmPopup: false, shouldResetScrollPosition: true });
            }
        }
    }

    _renderHeader() {
        return [
            <Row key="theard">
                <Cell header={true} colWidth={40} rowSpan="2"></Cell>
                <Cell header={true} colWidth={150} rowSpan="2">Name</Cell>
                <Cell header={true} colWidth={50} rowSpan="2">No.Aff</Cell>
                <Cell header={true} colWidth={150} rowSpan="2">Description</Cell>
                <Cell header={true} colWidth={80} rowSpan="2">Creation</Cell>
                <Cell header={true} colSpan="11">Policy</Cell>
                <Cell header={true} colWidth={70} rowSpan="2">Action</Cell>
            </Row>,
            <Row key="sub-theard">
                <Cell header={true} colWidth={80}></Cell>
                <Cell header={true} colWidth={80}>Tier</Cell>
                <Cell header={true} colWidth={80} title="Minimum all members' winlost amount">Min AM($)</Cell>
                <Cell header={true} colWidth={80} title="Maximum all members' winlost amount">Max AM($)</Cell>
                <Cell header={true} colWidth={80} title="Minimum number of qualified members">Min QM</Cell>
                <Cell header={true} colWidth={80} title="Maximum number of qualified members">Max QM</Cell>
                <Cell header={true} colWidth={80}>Comm Rate</Cell>
                <Cell header={true} colWidth={80}>Exchg. Rev%</Cell>
                <Cell header={true} colWidth={80}>Operational Fee</Cell>
                <Cell header={true} colWidth={80}>Banking Fee</Cell>
                <Cell header={true} colWidth={80}>QM Turnover</Cell>
            </Row>
        ];
    }

    _renderGroupData() {
        const { gridData } = this.props;
        const rows = [];

        if (gridData && gridData.length) {
            for (let index = 0; index < gridData.length; index++) {
                const item = gridData[index];

                const { TierSettingList, PlatformFeeList, OperationalFee, BankingFee, ExchangeWinLossShare, ExchangeCommissionShare, ...group } = item;
                const tierSettingRowSpan = TierSettingList.length;
                const platformFeeRowSpan = PlatformFeeList.length;
                const rowSpan = tierSettingRowSpan + platformFeeRowSpan;

                for (let iTier = 0; iTier < TierSettingList.length; iTier++) {
                    const tierSetting = TierSettingList[iTier];
                    const isFirstItem = iTier === 0;
                    const isPrivateGroup = group.GroupId > 1000;

                    rows.push(
                        (<Row key={`row-${index}-${iTier}`} className={isPrivateGroup ? "private" : ""}>
                            {isFirstItem && (<Cell className="text-center" rowSpan={rowSpan}>{group.RowIdx}</Cell>)}
                            {isFirstItem && (
                                <Cell className="text-center" rowSpan={rowSpan} title={isPrivateGroup && "Private Group"}>
                                    {isPrivateGroup && (<span className="oi oi-person"></span>)}{htmlDecode(group.GroupName)}
                                </Cell>
                            )}
                            {isFirstItem && (
                                <Cell className="text-center" rowSpan={rowSpan}>
                                    <a href={resolveClientUrl("Portal/Affiliate?groupId=") + group.GroupId} target="_top">{group.NoAffs}</a>
                                </Cell>
                            )}
                            {isFirstItem && (<Cell className="text-center" rowSpan={rowSpan}>{htmlDecode(group.Description)}</Cell>)}
                            {isFirstItem && (<Cell className="text-center" rowSpan={rowSpan}><p>{group.Creator}</p><p>{group.CreatedDateText}</p></Cell>)}

                            {isFirstItem && (<Cell className="text-center font-weight-bold cell-tier" rowSpan={tierSettingRowSpan}>Tier Setting</Cell>)}
                            <Cell className="text-center cell-tier">{tierSetting.TierId}</Cell>
                            <Cell className="text-right cell-tier">{format(tierSetting.MinAmount)}</Cell>
                            <Cell className="text-right cell-tier">{tierSetting.MaxAmount === INFINITY_AM ? INFINITY_SYMBOL : format(tierSetting.MaxAmount)}</Cell>
                            <Cell className="text-right cell-tier">{format(tierSetting.MinQm)}</Cell>
                            <Cell className="text-right cell-tier">{tierSetting.MaxQm === INFINITY_QM ? INFINITY_SYMBOL : format(tierSetting.MaxQm)}</Cell>
                            <Cell className="text-right cell-tier">{formatDecimal(tierSetting.Commission)}</Cell>

                            {isFirstItem && (<Cell className="text-right" rowSpan={rowSpan}>
                                <p>WL: {ExchangeWinLossShare ? formatDecimal(ExchangeWinLossShare.DeductedPercentage) : 0}</p>
                                <p>Comm: {ExchangeCommissionShare ? formatDecimal(ExchangeCommissionShare.DeductedPercentage) : 0}</p>
                            </Cell>)}
                            {isFirstItem && (<Cell className="text-right" rowSpan={rowSpan}>
                                {OperationalFee && formatDecimal(OperationalFee.DeductedPercentage)}</Cell>)}
                            {isFirstItem && (<Cell className="text-right" rowSpan={rowSpan}>
                                {BankingFee && formatDecimal(BankingFee.DeductedPercentage)}</Cell>)}
                            {isFirstItem && (<Cell className="text-right" rowSpan={rowSpan}>{formatDecimal(group.QmTurnover)}</Cell>)}

                            {isFirstItem && (
                                <Cell className="text-center" rowSpan={rowSpan}>
                                    <button className="btn btn-link" onClick={(e) => this.props.onEdit(item.GroupId)}>
                                        <span className="oi oi-pencil text-secondary"></span>
                                    </button>

                                    {
                                        group.NoAffs === 0 && (
                                            <button className="btn btn-link" onClick={() => this._handleDeleteCommGroup(item.GroupId, item.GroupName)}>
                                                <span className="oi oi-x text-danger"></span>
                                            </button>)
                                    }
                                </Cell>)
                            }
                        </Row>)
                    );
                }

                for (let iPlatformFee = 0; iPlatformFee < PlatformFeeList.length; iPlatformFee++) {
                    const platformFee = PlatformFeeList[iPlatformFee];
                    const isFirstItem = iPlatformFee === 0;

                    rows.push(
                        (<Row key={`row-platform-${index}-${iPlatformFee}`}>
                            {isFirstItem && (<Cell className="text-center font-weight-bold cell-platform" rowSpan={platformFeeRowSpan}>Platform Fee</Cell>)}
                            <Cell className="cell-platform" colSpan="5">{this._convertToProductNameText(platformFee.ProductIds)}</Cell>
                            <Cell className="text-right cell-platform">{formatDecimal(platformFee.DeductedPercentage)}</Cell>
                        </Row>)
                    );
                }

            }
        }
        else {
            rows.push(<Row>
                <Cell className="text-center" colSpan="16">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return rows;
    }

    render() {
        const { shouldResetScrollPosition, messageNotification, isConfirmPopup } = this.state;

        return (
            <React.Fragment>
                <div className="table-responsive table-group-setting">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderGroupData()}
                        containerPadding={20}
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                </div>
                {
                    !!messageNotification && <NotificationPopup
                        message={messageNotification}
                        isConfirmPopup={isConfirmPopup}
                        onClose={this.handleCloseNotificationPopup}
                    />
                }
            </React.Fragment>
        );
    }
}

function mapStateToProps({ gridData, productList, searchedGroupName, pagingOption }) {
    return Object.assign({}, { gridData, productList, searchedGroupName, pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ deleteCommGroup, getGroupSettingList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
