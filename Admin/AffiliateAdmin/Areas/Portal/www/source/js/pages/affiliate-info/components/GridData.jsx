import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText, resource, regExp, defaultSelectedColumnNumber, globalResource } from '../modules/constants';
import { setPagingOption, setShowPopupPassword, setShowPopupCommGroup, setAffId, setCommGroup } from '../actions';
import { toggleLoader } from '../../../actions/loaderAction';
import { post } from '../../../modules/ajaxRequest';
import { format, formatDecimal } from '../../../modules/numberFormat';
import { updateObjectInArray } from '../../../modules/dataHandler';
import { resolveClientUrl } from '../../../modules/utils';
import { convertSelectedColumnList } from '../modules/utils';
import { htmlDecode } from '../../../modules/htmlHelper';
import { NotificationPopup } from '../../common/components/NotificationPopup';

export class GridData extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            shouldResetScrollPosition: true,
            messageNotification: ''
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            const { pagingOption } = this.props;
            const updateParam = Object.assign({}, param, { PageIndex: 1, PageSize: pagingOption.PageSize });

            this.fetchData(updateParam);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const regexCheckAffId = new RegExp(regExp.CheckAffId);

        if (param.AffId === '' || regexCheckAffId.test(param.AffId)) {
            const url = 'Portal/Affiliate/GetAffiliateList';
            const response = await post(url, param);

            if (response && response.status === 200) {
                const { MainData, TotalItem } = response.data;

                this.setState({ gridData: MainData, shouldResetScrollPosition: true });

                this.props.setPagingOption({ TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
            }
        }
        else {
            this.setState({ gridData: {}, shouldResetScrollPosition: true });

            this.props.setPagingOption({ TotalItem: 0, PageIndex: param.PageIndex, PageSize: param.PageSize });
        }
        this.props.toggleLoader(false);
    }

    _renderHeader(selectedColumnList) {
        return (
            <Row key="theard">
                {selectedColumnList['Index'] && <Cell header={true} colWidth={80} title={headerText.Index}>{headerText.Index}</Cell>}
                <Cell header={true} colWidth={300} title={headerText.UserName}>{headerText.UserName}</Cell>
                {selectedColumnList['Status'] && <Cell header={true} colWidth={200} title={headerText.Status}>{headerText.Status}</Cell>}
                {selectedColumnList['FirstName'] && <Cell header={true} colWidth={200} title={headerText.FirstName}>{headerText.FirstName}</Cell>}
                {selectedColumnList['LastName'] && <Cell header={true} colWidth={200} title={headerText.LastName}>{headerText.LastName}</Cell>}
                {selectedColumnList['Language'] && <Cell header={true} colWidth={200} title={headerText.Language}>{headerText.Language}</Cell>}
                {selectedColumnList['Phone'] && <Cell header={true} colWidth={200} title={headerText.Phone}>{headerText.Phone}</Cell>}
                {selectedColumnList['Email'] && <Cell header={true} colWidth={400} title={headerText.Email}>{headerText.Email}</Cell>}
                {selectedColumnList['RegistrationDateText'] && <Cell header={true} colWidth={300} title={headerText.RegistrationDateText}>{headerText.RegistrationDateText}</Cell>}
                {selectedColumnList['ApprovalDateText'] && <Cell header={true} colWidth={300} title={headerText.ApprovalDateText}>{headerText.ApprovalDateText}</Cell>}
                {selectedColumnList['CasinoDateText'] && <Cell header={true} colWidth={200} title={headerText.CasinoDateText}>{headerText.CasinoDateText}</Cell>}
                {selectedColumnList['CommissionGroup'] && <Cell header={true} colWidth={300} title={headerText.CommissionGroup}>{headerText.CommissionGroup}</Cell>}
                {selectedColumnList['BalanceBf'] && <Cell header={true} colWidth={200} title={headerText.BalanceBf}>{headerText.BalanceBf}</Cell>}
                {selectedColumnList['Currency'] && <Cell header={true} colWidth={150} title={headerText.Currency}>{headerText.Currency}</Cell>}
                {selectedColumnList['PlayerUserName'] && <Cell header={true} colWidth={200} title={headerText.PlayerUserName}>{headerText.PlayerUserName}</Cell>}
                {selectedColumnList['PlayerCurrency'] && <Cell header={true} colWidth={150} title={headerText.PlayerCurrency}>{headerText.PlayerCurrency}</Cell>}
                {selectedColumnList['Member'] && <Cell header={true} colWidth={150} title={headerText.Member}>{headerText.Member}</Cell>}
                <Cell header={true} colWidth={130} title={headerText.Password}>{headerText.Password}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList, selectedColumnList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];

                dataRows.push(this._buildDataRow(dataItem, i + startIndex, selectedColumnList));
            }
        }
        else {
            const selectedColumnNumber = Object.keys(selectedColumnList).filter(key => !!selectedColumnList[key]).length
            dataRows.push(<Row>
                <Cell className="text-center" colSpan={defaultSelectedColumnNumber + selectedColumnNumber}>{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index, selectedColumnList) {
        const { languageList } = this.props;
        const status = dataItem.Status && htmlDecode(dataItem.Status).toLowerCase();
        const isApproved = status == "approved";
        const commissionGroupName = htmlDecode(dataItem.CommissionGroup);
        const firstName = htmlDecode(dataItem.FirstName);
        const lastName = htmlDecode(dataItem.LastName);
        const phone = htmlDecode(dataItem.Phone);
        const email = htmlDecode(dataItem.Email);
        const playerUserName = htmlDecode(dataItem.PlayerUserName);

        const balanceBf = dataItem.BalanceBf === null ? resource.NA : formatDecimal(dataItem.BalanceBf);
        const formattedMemberValue = dataItem.Member === null ? resource.NA : format(dataItem.Member);
        const member = dataItem.Member === null ? resource.NA : (
            <a href={resolveClientUrl(`Portal/Member?AffName=${dataItem.UserName}`)} target="_top">
                {formattedMemberValue}
            </a>);

        const language = languageList[dataItem.Language];
        const commissionGroup = dataItem.CommissionGroup ? (
            commissionGroupName === dataItem.UserName ? (
                <Button color="link"
                    onClick={() => this.handleEditCommGroup(dataItem.AffId, dataItem.CommissionGroup)}>
                    {commissionGroupName}
                </Button>) : commissionGroupName
        ) : resource.NA;

        const isFavorite = dataItem.IsFavorite
        const checkFavorite = isFavorite ? "favor-check" : "favor-uncheck";
        const classNameFavorite = (isApproved || status == "closed") ? checkFavorite : "";

        return (
            <Row key={`dataRow-${index}`}>
                {selectedColumnList['Index'] && <Cell className="text-right" title={index}>{index}</Cell>}
                <Cell title={dataItem.UserName}>
                    <a href={resolveClientUrl(`Portal/Customer?user=${dataItem.UserName}`)} target="_top">
                        {dataItem.UserName}
                    </a>
                    <span className={classNameFavorite} onClick={() => this.handleToggleFavorite(dataItem.AffId, isFavorite)}></span>
                </Cell>

                {selectedColumnList['Status'] && <Cell title={dataItem.Status}>{dataItem.Status}</Cell>}
                {selectedColumnList['FirstName'] && <Cell title={firstName}>{firstName}</Cell>}
                {selectedColumnList['LastName'] && <Cell title={lastName}>{lastName}</Cell>}
                {selectedColumnList['Language'] && <Cell title={language}>{language}</Cell>}
                {selectedColumnList['Phone'] && <Cell title={phone}>{phone}</Cell>}
                {selectedColumnList['Email'] && <Cell title={email}>{email}</Cell>}
                {selectedColumnList['RegistrationDateText'] && <Cell title={dataItem.RegistrationDateText}>{dataItem.RegistrationDateText}</Cell>}
                {selectedColumnList['ApprovalDateText'] && <Cell title={dataItem.ApprovalDateText}>{dataItem.ApprovalDateText}</Cell>}
                {selectedColumnList['CasinoDateText'] && <Cell title={dataItem.CasinoDateText}>{dataItem.CasinoDateText}</Cell>}
                {selectedColumnList['CommissionGroup'] && <Cell title={typeof commissionGroup === 'string' ? commissionGroup : commissionGroupName}>{commissionGroup}</Cell>}
                {selectedColumnList['BalanceBf'] && <Cell className="text-right" title={balanceBf}>{balanceBf}</Cell>}
                {selectedColumnList['Currency'] && <Cell title={dataItem.Currency}>{dataItem.Currency}</Cell>}
                {selectedColumnList['PlayerUserName'] && <Cell title={playerUserName}>{playerUserName}</Cell>}
                {selectedColumnList['PlayerCurrency'] && <Cell title={dataItem.PlayerCurrency}>{dataItem.PlayerCurrency}</Cell>}
                {selectedColumnList['Member'] && <Cell className="text-right" title={formattedMemberValue}>{member}</Cell>}
                <Cell className="text-center" title="Change Password">
                    {
                        isApproved && (<Button color="link" onClick={() => this.handleChangePassword(dataItem.AffId)}>
                            <span className="oi oi-pencil text-secondary"></span>
                        </Button>)
                    }
                </Cell>
            </Row>
        );
    }

    handleToggleFavorite = (affId, isFavorite) => {
        const param = {
            AffId: affId,
            IsFavorite: !isFavorite
        }

        this.saveToggleFavorite(param);
    }

    async saveToggleFavorite(param) {
        const url = `Portal/Affiliate/ToggleFavorite`;
        const response = await post(url, param);

        if (response) {
            if (response.data.IsSuccessful) {
                const { gridData } = this.state;
                const indexItem = gridData.findIndex(item => item.AffId === param.AffId);
                const updateItem = Object.assign({}, gridData[indexItem], { IsFavorite: param.IsFavorite });
                const gridDataUpdate = updateObjectInArray(gridData, indexItem, updateItem)

                this.setState({ gridData: gridDataUpdate, shouldResetScrollPosition: false });
            }
            else {
                this.setState({ messageNotification: response.data.Message });
            }
        }
    }

    handleChangePassword = (affId) => {
        this.props.setShowPopupPassword(true);
        this.props.setAffId(affId);
    }

    handleEditCommGroup = (affId, commGroup) => {
        this.props.setShowPopupCommGroup(true);
        this.props.setAffId(affId);
        this.props.setCommGroup(commGroup);
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
        const { gridData, shouldResetScrollPosition, messageNotification } = this.state;

        if (!gridData) {
            return null;
        }

        const { columnList } = this.props;
        const selectedColumnList = convertSelectedColumnList(columnList);

        return (
            <React.Fragment>
                <div className="table-responsive table-striped">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader(selectedColumnList)}
                        body={this._renderDataSection(gridData, selectedColumnList)}
                        containerPadding={20}
                        isPaging={true}
                        pageOption={this.props.pagingOption}
                        onPaging={this.handlePaging}
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                </div>
                {
                    !!messageNotification && <NotificationPopup
                        message={messageNotification}
                        isConfirmPopup={false}
                        onClose={() => this.setState({ messageNotification: '' })}
                    />
                }
            </React.Fragment>
        );
    }
}

function mapStateToProps({ pagingOption, languageList, param, columnList }) {
    return Object.assign({}, { pagingOption, languageList, param, columnList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoader, setShowPopupPassword, setShowPopupCommGroup, setAffId, setCommGroup }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
