import React, { PureComponent } from 'react';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { format, formatDecimal } from '../../../modules/numberFormat';
import { setPagingOption, setViewLevel, setParam } from '../actions';
import { post } from '../../../modules/ajaxRequest';
import { toggleLoader } from '../../../actions/loaderAction';
import { allProduct, viewLevel, month, sortDirectionType, totalText, headerText, reportKey } from '../libs/constants';
import { claimResource } from '../../../modules/claim';
import { globalResource } from '../../../modules/globalResource';
import { AggregateHeader, HeaderRow } from '../libs/header';

export class GridData extends PureComponent {
    constructor(props) {
        super(props);

        this.cachedMainReport = null;

        this.state = {
            gridData: null,
            total: null
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;
        const isMainView = this._isMainView();

        if (param !== prevProps.param && isMainView) {
            if (this.cachedMainReport && this.cachedMainReport.gridData) {
                const { gridData, total, totalItem } = this.cachedMainReport;

                this.setState({ gridData, total }, () => {
                    this.cachedMainReport = null;
                });
                this.props.setPagingOption({ TotalItem: totalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
            }
            else {
                this.fetchData(param);
            }
        }
        else if (param !== prevProps.param) {
            if (this.cachedMainReport === null) {
                this.cachedMainReport = Object.assign(this.state, { totalItem: prevProps.pagingOption.TotalItem });
            }

            this.fetchData(param);
        }
    }

    _isMainView() {
        return this.props.currentViewLevel === viewLevel.main;
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const isMainView = this._isMainView();
        const url = `Portal/Report/${isMainView ? 'GetMainData' : 'GetDetailData'}`;
        const response = await post(url, param);
        if (response && response.status === 200) {
            const { MainData, Total, TotalItem } = response.data;

            this.setState({ gridData: MainData, total: Total });
            this.props.setPagingOption({ TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });

            if (isMainView) {
                this.cachedMainReport = null;
            }
        }
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        const isMainView = this._isMainView();
        const { headerConfig, isCheckedDatePicker } = this.props;

        if (!(headerConfig instanceof HeaderRow)) {
            return [];
        }

        const firstColName = isCheckedDatePicker ? headerText.UserName : headerText.MonthDateString;
        const h1 = AggregateHeader.convertColsToObject(headerConfig.getHeaderCols(0));
        const h2 = AggregateHeader.convertColsToObject(headerConfig.getHeaderCols(1));

        return [
            <Row key="h1">
                <Cell header={true} colWidth={120} rowSpan="2" title={firstColName}>{firstColName}</Cell>
                {h1[reportKey.PreferredLanguage].isVisable && <Cell header={true} colWidth={80} rowSpan="2" title={headerText.PreferredLanguage}>{headerText.PreferredLanguage}</Cell>}
                {h1[reportKey.TotalClick].isVisable && <Cell header={true} colWidth={60} rowSpan="2" title={headerText.TotalClick}>{headerText.TotalClick}</Cell>}
                {h1[reportKey.UniqueClick].isVisable && <Cell header={true} colWidth={60} rowSpan="2" title={headerText.UniqueClick}>{headerText.UniqueClick}</Cell>}
                {h1[reportKey.Signup].isVisable && <Cell header={true} colWidth={65} rowSpan="2" title={headerText.Signup}>{headerText.Signup}</Cell>}
                {h1[reportKey.Fdm].isVisable && <Cell header={true} colWidth={50} rowSpan="2" title={headerText.Fdm}>{headerText.Fdm}</Cell>}
                {h1[reportKey.ActiveMember].isVisable && <Cell header={true} colWidth={60} rowSpan="2" title={headerText.ActiveMember}>{headerText.ActiveMember}</Cell>}
                {h1[reportKey.QualifiedMember].isVisable && <Cell header={true} colWidth={60} rowSpan="2" title={headerText.QualifiedMember}>{headerText.QualifiedMember}</Cell>}
                {h1[reportKey.MemberStatistics].isVisable && <Cell header={true} colSpan={h1[reportKey.MemberStatistics].totalVisableCol} title={headerText.MemberStatistics}>{headerText.MemberStatistics}</Cell>}
                {h1[reportKey.AffiliateStatistics].isVisable && <Cell header={true} colSpan={h1[reportKey.AffiliateStatistics].totalVisableCol} title={headerText.AffiliateStatistics}>{headerText.AffiliateStatistics}</Cell>}
                {isMainView && !isCheckedDatePicker && (<Cell header={true} colWidth={50} rowSpan="2" title={headerText.MonthOnMonth}>{headerText.MonthOnMonth}</Cell>)}
            </Row>,
            <Row key="h2">
                {h2[reportKey.Deposit].isVisable && <Cell header={true} colWidth={80} title={headerText.Deposit}>{headerText.Deposit}</Cell>}
                {h2[reportKey.Withdrawal].isVisable && <Cell header={true} colWidth={80} title={headerText.Withdrawal}>{headerText.Withdrawal}</Cell>}
                {h2[reportKey.Turnover].isVisable && <Cell header={true} colWidth={80} title={headerText.Turnover}>{headerText.Turnover}</Cell>}
                {h2[reportKey.Winlost].isVisable && <Cell header={true} colWidth={80} title={headerText.Winlost}>{headerText.Winlost}</Cell>}
                {h2[reportKey.Bonus].isVisable && <Cell header={true} colWidth={70} title={headerText.Bonus}>{headerText.Bonus}</Cell>}
                {h2[reportKey.PlatformFee].isVisable && <Cell header={true} colWidth={70} title={headerText.PlatformFee}>{headerText.PlatformFee}</Cell>}
                {h2[reportKey.OperationalFee].isVisable && <Cell header={true} colWidth={70} title={headerText.OperationalFee}>{headerText.OperationalFee}</Cell>}
                {h2[reportKey.BankingFee].isVisable && <Cell header={true} colWidth={70} title={headerText.BankingFee}>{headerText.BankingFee}</Cell>}
                {h2[reportKey.TotalFee].isVisable && <Cell header={true} colWidth={70} title={headerText.TotalFee}>{headerText.TotalFee}</Cell>}
                {h2[reportKey.CommissionRate].isVisable && <Cell header={true} colWidth={60} title={headerText.CommissionRate}>{headerText.CommissionRate}</Cell>}
                {h2[reportKey.PreviousBalanceBf].isVisable && <Cell header={true} colWidth={100} title={headerText.PreviousBalanceBf}>{headerText.PreviousBalanceBf}</Cell>}
                {h2[reportKey.Earning].isVisable && <Cell header={true} colWidth={80} title={headerText.Earning}>{headerText.Earning}</Cell>}
                {h2[reportKey.NewFdmEarning].isVisable && <Cell header={true} colWidth={80} title={headerText.Earning}>{headerText.NewFdmEarning}</Cell>}
                {h2[reportKey.TotalEarning].isVisable && <Cell header={true} colWidth={80} title={headerText.Earning}>{headerText.TotalEarning}</Cell>}
                {h2[reportKey.AffWithdrawal].isVisable && <Cell header={true} colWidth={80} title={headerText.AffWithdrawal}>{headerText.AffWithdrawal}</Cell>}
                {h2[reportKey.AffAdjustment].isVisable && <Cell header={true} colWidth={70} title={headerText.AffAdjustment}>{headerText.AffAdjustment}</Cell>}
                {h2[reportKey.BalanceBf].isVisable && <Cell header={true} colWidth={100} title={headerText.BalanceBf}>{headerText.BalanceBf}</Cell>}
            </Row>
        ];
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            const isSelectedAllProducts = this.props.param.ProductIds === allProduct;
            const productColumnClassName = isSelectedAllProducts ? 'text-right' : 'text-right highlight';

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, productColumnClassName, i));
            }
        } else {
            const header = AggregateHeader.convertColsToObject(this.props.headerConfig.getHeaderCols(0));
            const singleColums = Object.keys(header).filter(i => header[i].visable).length;
            const memberStatisticsColumns = header[reportKey.MemberStatistics].isVisable ? header[reportKey.MemberStatistics].childerent.filter(i => i.visable).length : 0;
            const affStatisticsColumns = header[reportKey.AffiliateStatistics].isVisable ? header[reportKey.AffiliateStatistics].childerent.filter(i => i.visable).length : 0;

            const colSpan = singleColums + memberStatisticsColumns + affStatisticsColumns + (this._isMainView() && !this.props.isCheckedDatePicker )

            dataRows.push(<Row>
                <Cell className="text-center" colSpan={colSpan}>{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, productColumnClassName, index) {
        const isMainView = this._isMainView();
        const isTotalRow = isMainView ? (!dataItem.AffId && !dataItem.UserName) : !dataItem.MonthDateString;
        const firstColValue = isTotalRow ?
            totalText :
            (isMainView ? dataItem.UserName : this._buildMonthCell(dataItem.MonthDateString, dataItem.Description));

        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={firstColValue}>{firstColValue}</Cell>
                {this._buildCommonCell(dataItem, productColumnClassName, isTotalRow, isMainView)}
                {
                    isMainView && !this.props.isCheckedDatePicker && (
                        <Cell className="text-center">
                            {dataItem.AffId > 0 ? (
                                <Button color="link" className="p-0"
                                    onClick={() => this.handleViewMonthOnMonth(dataItem.UserName)}>View</Button>) : ''}
                        </Cell>)
                }
            </Row>
        )
    }

    _buildMonthCell(monthDate, description) {
        return description ? `${monthDate} (${description})` : monthDate;
    }

    _buildCommonCell(dataItem, productColumnClassName, isTotalRow, isMainView) {
        const { headerConfig, languageList } = this.props;
        const header = AggregateHeader.convertColsToObject(headerConfig.getCols());
        const totalClick = format(dataItem.TotalClick);
        const uniqueClick = format(dataItem.UniqueClick);
        const signup = format(dataItem.Signup);
        const fdm = format(dataItem.Fdm);
        const activeMember = format(dataItem.ActiveMember);
        const qualifiedMember = format(dataItem.QualifiedMember);
        const deposit = formatDecimal(dataItem.Deposit);
        const withdrawal = formatDecimal(dataItem.Withdrawal);
        const turnover = formatDecimal(dataItem.Turnover);
        const winlost = formatDecimal(dataItem.Winlost);
        const bonus = formatDecimal(dataItem.Bonus);
        const platformFee = formatDecimal(dataItem.PlatformFee);
        const operationalFee = formatDecimal(dataItem.OperationalFee);
        const bankingFee = formatDecimal(dataItem.BankingFee);
        const totalFee = formatDecimal(dataItem.TotalFee);

        const earning = formatDecimal(dataItem.Earning);
        const newFdmEarning = formatDecimal(dataItem.NewFdmEarning);
        const totalEarning = formatDecimal(dataItem.TotalEarning);
        const affWithdrawal = formatDecimal(dataItem.AffWithdrawal);
        const affAdjustment = formatDecimal(dataItem.AffAdjustment);
        const balanceBf = formatDecimal(dataItem.BalanceBf);
        const prevBalanceBf = formatDecimal(dataItem.PreviousBalanceBf);
        const commRate = isMainView && isTotalRow ? 'N/A' : formatDecimal(dataItem.CommissionRate) + '%';
        const language = isMainView ? languageList[dataItem.PreferredLanguage] : "";

        return (
            <React.Fragment>
                { header[reportKey.PreferredLanguage].isVisable && <Cell title={language}>{language}</Cell>}
                { header[reportKey.TotalClick].isVisable && <Cell className="text-right" title={totalClick}>{totalClick}</Cell>}
                { header[reportKey.UniqueClick].isVisable &&  <Cell className="text-right" title={uniqueClick}>{uniqueClick}</Cell>}
                { header[reportKey.Signup].isVisable && <Cell className="text-right" title={signup}>{signup}</Cell>}
                { header[reportKey.Fdm].isVisable && <Cell className="text-right" title={fdm}>{fdm}</Cell>}
                { header[reportKey.ActiveMember].isVisable && <Cell className="text-right" title={activeMember}>{activeMember}</Cell>}
                { header[reportKey.QualifiedMember].isVisable && <Cell className="text-right" title={qualifiedMember}>{qualifiedMember}</Cell>}
                { header[reportKey.Deposit].isVisable && <Cell className="text-right" title={deposit}>{deposit}</Cell>}
                { header[reportKey.Withdrawal].isVisable && <Cell className="text-right" title={withdrawal}>{withdrawal}</Cell>}
                { header[reportKey.Turnover].isVisable &&  <Cell className={productColumnClassName} title={turnover}>{turnover}</Cell>}
                { header[reportKey.Winlost].isVisable && <Cell className={productColumnClassName} title={winlost}>{winlost}</Cell>}
                { header[reportKey.Bonus].isVisable && <Cell className="text-right" title={bonus}>{bonus}</Cell>}
                { header[reportKey.PlatformFee].isVisable && <Cell className="text-right" title={platformFee}>{platformFee}</Cell>}
                { header[reportKey.OperationalFee].isVisable && <Cell className="text-right" title={operationalFee}>{operationalFee}</Cell>}
                { header[reportKey.BankingFee].isVisable && <Cell className="text-right" title={bankingFee}>{bankingFee}</Cell>}
                { header[reportKey.TotalFee].isVisable && <Cell className="text-right" title={totalFee}>{totalFee}</Cell>}
                { header[reportKey.CommissionRate].isVisable && <Cell className="text-right" title={commRate}>{commRate}</Cell>}
                { header[reportKey.PreviousBalanceBf].isVisable && <Cell className="text-right" title={prevBalanceBf}>{prevBalanceBf}</Cell>}
                { header[reportKey.Earning].isVisable && <Cell className="text-right" title={earning}>{earning}</Cell>}
                { header[reportKey.NewFdmEarning].isVisable && <Cell className="text-right" title={newFdmEarning}>{newFdmEarning}</Cell>}
                { header[reportKey.TotalEarning].isVisable && <Cell className="text-right" title={totalEarning}>{totalEarning}</Cell>}
                { header[reportKey.AffWithdrawal].isVisable && <Cell className="text-right" title={affWithdrawal}>{affWithdrawal}</Cell>}
                { header[reportKey.AffAdjustment].isVisable &&  (<Cell className="text-right" title={affAdjustment}>{affAdjustment}</Cell>)}
                { header[reportKey.BalanceBf].isVisable && <Cell className="text-right" title={balanceBf}>{balanceBf}</Cell>}
            </React.Fragment>
        )
    }

    handleViewMonthOnMonth = (UserName) => {
        const { param, pagingOption } = this.props;

        const postParam = Object.assign({}, param, {
            UserName,
            SortType: month,
            SortDirectionType: sortDirectionType.descending,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        })

        this.props.setViewLevel(viewLevel.monthOnMonth);
        this.props.setParam(postParam);
    }

    handlePaging = (index, size) => {
        const newPaging = {
            PageIndex: index,
            PageSize: size
        };
        this.props.setPagingOption(newPaging);

        const newParam = Object.assign({}, this.props.param, newPaging);
        this.fetchData(newParam);
    }

    render() {
        const { gridData, total } = this.state;

        return gridData && (
            <div className="table-responsive table-striped">
                <Table
                    minWidth={1368}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData)}
                    footer={total && this._renderDataSection([total])}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={this.props.pagingOption}
                    onPaging={this.handlePaging}
                />
            </div>
        );
    }
}

function mapStateToProps({ pagingOption, param, currentViewLevel, claimsPrincipal, isCheckedDatePicker, languageList }) {
    const hasTurnOverPermission = claimsPrincipal.hasReadClaim(claimResource.TurnOver);
    return Object.assign({}, { pagingOption, param, currentViewLevel, hasTurnOverPermission, isCheckedDatePicker, languageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoader, setViewLevel, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);