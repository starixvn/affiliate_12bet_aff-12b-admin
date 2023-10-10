import React, { Component, Fragment } from 'react';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { resolveClientUrl, formatDateTime } from '../../../../../modules/utils';
import { INFINITY_AM, INFINITY_QM, INFINITY_SYMBOL, globalResource } from '../../libs/constant';
import { format, formatDecimal } from '../../../../../modules/numberFormat';
import { deleteCommGroup, getGroupSettingList } from '../../actions';
import { htmlDecode } from '../../../../../modules/htmlHelper';
import { NotificationPopup } from '../../../../common/components/NotificationPopup';
import { claimResource } from '../../../../../modules/claim';

export class GridData extends Component {
    constructor(props) {
        super(props);

        const { productList } = this.props;
        this.products = {};
        for (let product of productList) {
            this.products[product.ProductId] = product.ProductName;
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
        const { isModifiable } = this.props;
        return [
            <Row key="theard">
                <Cell header={true} colWidth={40} rowSpan="2"></Cell>
                <Cell header={true} colWidth={150} rowSpan="2">Name</Cell>
                <Cell header={true} colWidth={50} rowSpan="2">No.Aff</Cell>
                <Cell header={true} colWidth={150} rowSpan="2">Description</Cell>
                <Cell header={true} colWidth={80} rowSpan="2">Creation</Cell>
                <Cell header={true} colSpan="12">Policy</Cell>
                {isModifiable && <Cell header={true} colWidth={70} rowSpan="2">Action</Cell>}
            </Row>,
            <Row key="sub-theard">
                <Cell header={true} colWidth={80}></Cell>
                <Cell header={true} colWidth={80}>Tier/<br /> Min New FDM</Cell>
                <Cell header={true} colWidth={80} title="Minimum all members' winlost amount">Min AM($)/<br /> Max New FDM</Cell>
                <Cell header={true} colWidth={80} title="Maximum all members' winlost amount">Max AM($)/<br /> Min FDM's Deposit</Cell>
                <Cell header={true} colWidth={80} title="Minimum number of qualified members">Min QM/<br /> Max FDM's Deposit</Cell>
                <Cell header={true} colWidth={80} title="Maximum number of qualified members">Max QM/<br /> Comm Rate</Cell>
                <Cell header={true} colWidth={80}>Comm Rate/<br /> Max Comm Rate</Cell>
                <Cell header={true} colWidth={80}>Bonus</Cell>
                <Cell header={true} colWidth={80}>Exchg. Rev%</Cell>
                <Cell header={true} colWidth={80}>Operational Fee</Cell>
                <Cell header={true} colWidth={80}>Banking Fee</Cell>
                <Cell header={true} colWidth={80}>QM Turnover</Cell>
            </Row>
        ];
    }

    _renderGroupData() {
        const { gridData, isModifiable, isShowAffTab } = this.props;
        const rows = [];

        if (gridData && gridData.length) {
            for (let index = 0; index < gridData.length; index++) {
                const item = gridData[index];

                const { TierSettingList, FdmSettingList, PlatformFeeList, OperationalFee, BankingFee, ExchangeWinLossShare, ExchangeCommissionShare, ...group } = item;
                const tierSettingRowSpan = TierSettingList.length;
                const fdmSettingRowSpan = FdmSettingList.length;
                const platformFeeRowSpan = PlatformFeeList.length;
                const rowSpan = tierSettingRowSpan + fdmSettingRowSpan + platformFeeRowSpan;

                for (let iTier = 0; iTier < tierSettingRowSpan; iTier++) {
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
                                    { isShowAffTab
                                        ? <a href={resolveClientUrl("Portal/Affiliate?groupId=") + group.GroupId} target="_top">{group.NoAffs}</a>
                                        : group.NoAffs
                                    }
                                </Cell>
                            )}
                            {isFirstItem && (<Cell className="text-center" rowSpan={rowSpan}>{htmlDecode(group.Description)}</Cell>)}
                            {isFirstItem && (<Cell className="text-center" rowSpan={rowSpan}><p>{group.Creator}</p><p>{formatDateTime(group.CreatedDate, "MM/dd/yyyy HH:mm:ss")}</p></Cell>)}

                            {isFirstItem && (<Cell className="text-center font-weight-bold cell-tier" rowSpan={tierSettingRowSpan}>Tier Setting</Cell>)}
                            <Cell className="text-center cell-tier">{tierSetting.TierId}</Cell>
                            <Cell className="text-right cell-tier">{format(tierSetting.MinAmount)}</Cell>
                            <Cell className="text-right cell-tier">{tierSetting.MaxAmount === INFINITY_AM ? INFINITY_SYMBOL : format(tierSetting.MaxAmount)}</Cell>
                            <Cell className="text-right cell-tier">{format(tierSetting.MinQm)}</Cell>
                            <Cell className="text-right cell-tier">{tierSetting.MaxQm === INFINITY_QM ? INFINITY_SYMBOL : format(tierSetting.MaxQm)}</Cell>
                            <Cell className="text-right cell-tier">{formatDecimal(tierSetting.Commission)}</Cell>

                            {isFirstItem && (<Cell className="text-right" rowSpan={rowSpan}>{group.IsApplyBonus ? 'Include' : 'Exclude'}</Cell>)}
                            {isFirstItem && (<Cell className="text-right" rowSpan={rowSpan}>
                                <p>WL: {ExchangeWinLossShare ? formatDecimal(ExchangeWinLossShare.DeductedPercentage) : 0}</p>
                                <p>Comm: {ExchangeCommissionShare ? formatDecimal(ExchangeCommissionShare.DeductedPercentage) : 0}</p>
                            </Cell>)}
                            {isFirstItem && (<Cell className="text-right" rowSpan={rowSpan}>
                                {OperationalFee && formatDecimal(OperationalFee.DeductedPercentage)}</Cell>)}
                            {isFirstItem && (<Cell className="text-right" rowSpan={rowSpan}>
                                {BankingFee && formatDecimal(BankingFee.DeductedPercentage)}</Cell>)}
                            {isFirstItem && (<Cell className="text-right" rowSpan={rowSpan}>{formatDecimal(group.QmTurnover)}</Cell>)}

                            {isFirstItem && isModifiable && (
                                <Cell className="text-center" rowSpan={rowSpan}>
                                    <button className="btn btn-link" onClick={(e) => this.props.onEdit(group.GroupId)}>
                                        <span className="oi oi-pencil text-secondary"></span>
                                    </button>

                                    {
                                        group.NoAffs === 0 && (
                                            <Fragment>
                                                <span>&nbsp;&nbsp;&nbsp;</span>
                                                <button className="btn btn-link" onClick={() => this._handleDeleteCommGroup(group.GroupId, group.GroupName)}>
                                                    <span className="oi oi-trash text-danger"></span>
                                                </button>
                                            </Fragment>)
                                    }
                                </Cell>)
                            }
                        </Row>)
                    );
                }

                for (let iFdmSetting = 0; iFdmSetting < fdmSettingRowSpan; iFdmSetting++) {
                    const fdmSetting = FdmSettingList[iFdmSetting];
                    const isFirstItem = iFdmSetting === 0;

                    rows.push(
                        (<Row key={`row-fdm-${index}-${iFdmSetting}`}>
                            {isFirstItem && (<Cell className="text-center font-weight-bold cell-tier" rowSpan={fdmSettingRowSpan}>FDM Setting</Cell>)}
                            <Cell className="text-center cell-tier">{format(fdmSetting.MinNewFdm)}</Cell>
                            <Cell className="text-right cell-tier">{fdmSetting.MaxNewFdm === INFINITY_QM ? INFINITY_SYMBOL : format(fdmSetting.MaxNewFdm)}</Cell>
                            <Cell className="text-right cell-tier">{format(fdmSetting.MinFdmDeposit)}</Cell>
                            <Cell className="text-right cell-tier">{fdmSetting.MaxFdmDeposit === INFINITY_AM ? INFINITY_SYMBOL : format(fdmSetting.MaxFdmDeposit)}</Cell>
                            <Cell className="text-right cell-tier">{formatDecimal(fdmSetting.Commission)}</Cell>
                            <Cell className="text-right cell-tier">{format(fdmSetting.MaxExtraComm)}</Cell>
                        </Row>)
                    );
                }

                for (let iPlatformFee = 0; iPlatformFee < platformFeeRowSpan; iPlatformFee++) {
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
                <Cell className="text-center" colSpan="18">{globalResource.NoInfo}</Cell>
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

function mapStateToProps({ gridData, productList, searchedGroupName, pagingOption, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.CommissionGroup);
    const isShowAffTab = claimsPrincipal.hasReadClaim(claimResource.AffiliateInfo)
    return Object.assign({}, { gridData, productList, searchedGroupName, pagingOption, isModifiable, isShowAffTab });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ deleteCommGroup, getGroupSettingList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
