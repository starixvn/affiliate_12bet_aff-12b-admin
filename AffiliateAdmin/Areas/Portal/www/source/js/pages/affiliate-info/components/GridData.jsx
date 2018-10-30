import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText, resource } from '../modules/constants';
import { setPagingOption, setParam } from '../actions';
import { toggleLoader } from '../../../actions/loaderAction';
import { post } from '../../../modules/ajaxRequest';
import { format, formatDecimal } from '../../../modules/numberFormat';

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
            this.fetchData(param);
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
                <Cell header={true} colWidth={100} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={300} title={headerText.Username}>{headerText.Username}</Cell>
                <Cell header={true} colWidth={200} title={headerText.Status}>{headerText.Status}</Cell>
                <Cell header={true} colWidth={200} title={headerText.FirstName}>{headerText.FirstName}</Cell>
                <Cell header={true} colWidth={300} title={headerText.LastName}>{headerText.LastName}</Cell>
                <Cell header={true} colWidth={300} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={400} title={headerText.Email}>{headerText.Email}</Cell>
                <Cell header={true} colWidth={300} title={headerText.RegistrationDate}>{headerText.RegistrationDate}</Cell>
                <Cell header={true} colWidth={300} title={headerText.ApprovalDate}>{headerText.ApprovalDate}</Cell>
                <Cell header={true} colWidth={300} title={headerText.CasinoDate}>{headerText.CasinoDate}</Cell>
                <Cell header={true} colWidth={300} title={headerText.CommissionGroup}>{headerText.CommissionGroup}</Cell>
                <Cell header={true} colWidth={300} title={headerText.BalanceBF}>{headerText.BalanceBF}</Cell>
                <Cell header={true} colWidth={200} title={headerText.Member}>{headerText.Member}</Cell>
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
        const balanceBf = formatDecimal(dataItem.BalanceBf);
        const member = format(dataItem.Member);

        const { languageList } = this.props;
        const status = dataItem.Status;
        const isShow = status && status.toLowerCase() == "approved";

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-right" title={index}>{index}</Cell>
                <Cell title={dataItem.UserName}>{dataItem.UserName}{dataItem.IsFavorite ? (<span className="favor-check"></span>) : (<span className="favor-uncheck"></span>)}</Cell>
                <Cell className="text-center" title={dataItem.Status}>{dataItem.Status}</Cell>
                <Cell title={dataItem.FirstName}>{dataItem.FirstName}</Cell>
                <Cell title={dataItem.LastName}>{dataItem.LastName}</Cell>
                <Cell title={dataItem.Language}>{languageList[dataItem.Language]}</Cell>
                <Cell title={dataItem.Email}>{dataItem.Email}</Cell>
                <Cell className="text-center" title={dataItem.RegistrationDateText}>{dataItem.RegistrationDateText ? dataItem.RegistrationDateText : resource.NA}</Cell>
                <Cell className="text-center" title={dataItem.ApprovalDateText}>{dataItem.ApprovalDateText ? dataItem.ApprovalDateText : resource.NA}</Cell>
                <Cell className="text-center" title={dataItem.CasinoDateText}>{dataItem.CasinoDateText ? dataItem.CasinoDateText : resource.NA}</Cell>
                <Cell className="text-center" title={dataItem.CommissionGroup}>{dataItem.CommissionGroup ? dataItem.CommissionGroup : resource.NA}</Cell>
                <Cell className="text-right" title={balanceBf}>{dataItem.BalanceBf ? balanceBf : resource.NA}</Cell>
                <Cell className="text-right" title={member}>{dataItem.Member ? member : resource.NA}</Cell>
                <Cell className="text-center" title="Change Password">
                    {
                        isShow && (<button className="btn btn-link">
                            <span className="oi oi-pencil text-secondary"></span>
                        </button>)
                    }
                </Cell>
            </Row>
        );
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
    return bindActionCreators({ setPagingOption, toggleLoader, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
