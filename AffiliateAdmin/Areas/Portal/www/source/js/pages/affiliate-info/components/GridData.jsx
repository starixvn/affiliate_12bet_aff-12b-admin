import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText, resource } from '../modules/constants';
import { setPagingOption, setParam, setShowPopup, setAffId } from '../actions';
import { toggleLoader } from '../../../actions/loaderAction';
import { post } from '../../../modules/ajaxRequest';
import { format, formatDecimal } from '../../../modules/numberFormat';
import { updateObjectInArray } from '../../../modules/dataHandler'

export class GridData extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null
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
        const url = `Portal/AffiliateInfo/GetAffiliateList`;
        const response = await post(url, param);

        if (response && response.status === 200) {
            const { MainData, TotalItem } = response.data;

            this.setState({ gridData: MainData });

            this.props.setPagingOption({ TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
        }
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row key="theard">
                <Cell header={true} colWidth={80} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={300} title={headerText.UserName}>{headerText.UserName}</Cell>
                <Cell header={true} colWidth={200} title={headerText.Status}>{headerText.Status}</Cell>
                <Cell header={true} colWidth={220} title={headerText.FirstName}>{headerText.FirstName}</Cell>
                <Cell header={true} colWidth={300} title={headerText.LastName}>{headerText.LastName}</Cell>
                <Cell header={true} colWidth={300} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={450} title={headerText.Email}>{headerText.Email}</Cell>
                <Cell header={true} colWidth={300} title={headerText.RegistrationDateText}>{headerText.RegistrationDateText}</Cell>
                <Cell header={true} colWidth={300} title={headerText.ApprovalDateText}>{headerText.ApprovalDateText}</Cell>
                <Cell header={true} colWidth={300} title={headerText.CasinoDateText}>{headerText.CasinoDateText}</Cell>
                <Cell header={true} colWidth={300} title={headerText.CommissionGroup}>{headerText.CommissionGroup}</Cell>
                <Cell header={true} colWidth={200} title={headerText.BalanceBf}>{headerText.BalanceBf}</Cell>
                <Cell header={true} colWidth={150} title={headerText.Member}>{headerText.Member}</Cell>
                <Cell header={true} colWidth={130} title={headerText.Password}>{headerText.Password}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];
        const { pagingOption } = this.props;
        const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];

            dataRows.push(this._buildDataRow(dataItem, i + startIndex));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const { languageList } = this.props;
        const statusToLower = dataItem.Status && dataItem.Status.toLowerCase();
        const isApproved = statusToLower == "approved";

        const balanceBf = dataItem.BalanceBf ? formatDecimal(dataItem.BalanceBf) : resource.NA;
        const member = dataItem.Member ? format(dataItem.Member) : resource.NA;
        const language = languageList[dataItem.Language];
        const commissionGroup = dataItem.CommissionGroup ? dataItem.CommissionGroup : resource.NA;

        const isFavorite = dataItem.IsFavorite
        const checkFavorite = isFavorite ? "favor-check" : "favor-uncheck";
        const classNameFavorite = (isApproved || statusToLower == "closed") ? checkFavorite : "";

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell title={dataItem.UserName}>{dataItem.UserName} <span className={classNameFavorite} onClick={() => this.handleToggleFavorite(dataItem.AffId, isFavorite)}></span></Cell>

                <Cell className="text-center" title={dataItem.Status}>{dataItem.Status}</Cell>
                <Cell title={dataItem.FirstName}>{dataItem.FirstName}</Cell>
                <Cell title={dataItem.LastName}>{dataItem.LastName}</Cell>
                <Cell title={language}>{language}</Cell>
                <Cell title={dataItem.Email}>{dataItem.Email}</Cell>
                <Cell className="text-center" title={dataItem.RegistrationDateText}>{dataItem.RegistrationDateText}</Cell>
                <Cell className="text-center" title={dataItem.ApprovalDateText}>{dataItem.ApprovalDateText}</Cell>
                <Cell className="text-center" title={dataItem.CasinoDateText}>{dataItem.CasinoDateText}</Cell>
                <Cell className="text-center" title={commissionGroup}>{commissionGroup}</Cell>
                <Cell className="text-right" title={balanceBf}>{balanceBf}</Cell>
                <Cell className="text-right" title={member}>{member}</Cell>
                <Cell className="text-center" title="Change Password">
                    {
                        isApproved && (<button className="btn btn-link" onClick={() => this.handleChangePassword(dataItem.AffId)}>
                            <span className="oi oi-pencil text-secondary"></span>
                        </button>)
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
        const url = `Portal/AffiliateInfo/ToggleFavorite`;
        const response = await post(url, param);

        if (response) {
            if (response.data.IsSuccessful) {
                const { gridData } = this.state;
                const indexItem = gridData.findIndex(item => item.AffId === param.AffId);
                const updateItem = Object.assign({}, gridData[indexItem], { IsFavorite: param.IsFavorite });
                const gridDataUpdate = updateObjectInArray(gridData, indexItem, updateItem)

                this.setState({ gridData: gridDataUpdate });
            }
            else {
                alert(response.data.Message);
            }
        }
    }

    handleChangePassword = (affId) => {
        this.props.setShowPopup(true);
        this.props.setAffId(affId);
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
        const { gridData } = this.state;

        return gridData && (
            <div className="table-responsive table-striped">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData)}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={this.props.pagingOption}
                    onPaging={this.handlePaging}
                />
            </div>
        );
    }
}

function mapStateToProps({ pagingOption, languageList, param }) {
    return Object.assign({}, { pagingOption, languageList, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoader, setShowPopup, setAffId }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
