import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerTitle, globalResource, maxWidth } from '../libs/constants';
import { setSelectedPageDetailFilter, toggleLoaderUserPanel, fetchData } from '../actions';
import { htmlDecode } from '../../../../modules/htmlHelper';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            this.fetchData(this.props.param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoaderUserPanel(true);

        const data = await this.props.fetchData(param, this.props.shortCodeLanguageList);

        this.setState({ gridData: data });

        this.props.toggleLoaderUserPanel(false);
    }

    handleSelectedPageDetailFilter = (selectedPage, filterOptionValue, isGroupByCountry) => {
        const filterOptionName = isGroupByCountry ? "selectedCountry" : "languageCode";
        const param = Object.assign({}, { selectedPage, [filterOptionName]: filterOptionValue });

        this.props.setSelectedPageDetailFilter(param);
    }

    _renderHeader() {
        const { isGroupByCountry } = this.props;

        return (
            <Row key="thead">
                {isGroupByCountry && <Cell header={true} colWidth={290} title={headerTitle.Country}>{headerTitle.Country}</Cell>}
                <Cell header={true} colWidth={290} title={headerTitle.Page}>{headerTitle.Page}</Cell>
                {!isGroupByCountry && <Cell header={true} colWidth={290} title={headerTitle.Language}>{headerTitle.Language}</Cell>}
                <Cell header={true} colWidth={110} title={headerTitle.TotalClick}>{headerTitle.TotalClick}</Cell>
                <Cell header={true} colWidth={110} title={headerTitle.UniqueClick}>{headerTitle.UniqueClick}</Cell>
                <Cell header={true} colWidth={110} title={headerTitle.Signups}>{headerTitle.Signups}</Cell>
                <Cell header={true} colWidth={110} title={headerTitle.Fdm}>{headerTitle.Fdm}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            const { isGroupByCountry } = this.props;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i, isGroupByCountry));
            }
        }
        else {
            dataRows.push(
                <Row>
                    <Cell className="text-center" colSpan="6">{globalResource.NoInfo}</Cell>
                </Row>
            );
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index, isGroupByCountry) {
        const language = dataItem.Language;
        const country = dataItem.Country;
        const page = htmlDecode(dataItem.Page);
        const isShowLink = page && !(page.toLowerCase() == "old url" || page.toLowerCase() == "none");

        return (
            <Row key={`dataRow-${index}`}>
                {
                    isGroupByCountry ? (
                        <Fragment>
                            <Cell title={country}>
                                {isShowLink ? (<Button color="link" className="p-0" onClick={() => this.handleSelectedPageDetailFilter(dataItem.Page, dataItem.CountryCode, isGroupByCountry)}>
                                    {country}
                                </Button>) : country}
                            </Cell>
                            <Cell title={page} className="text-right">{page}</Cell>
                        </Fragment>
                    ) : (
                        <Fragment>
                                <Cell title={page}>
                                    {isShowLink ? (<Button color="link" className="p-0" onClick={() => this.handleSelectedPageDetailFilter(dataItem.Page, dataItem.LanguageCode, isGroupByCountry)}>
                                        {page}
                                    </Button>) : page}
                                </Cell>
                            <Cell title={language} className="text-right">{language}</Cell>
                        </Fragment>
                    )
                }
                <Cell title={dataItem.TotalClick} className="text-right">{dataItem.TotalClick}</Cell>
                <Cell title={dataItem.UniqueClick} className="text-right">{dataItem.UniqueClick}</Cell>
                <Cell title={dataItem.Signups} className="text-right">{dataItem.Signups}</Cell>
                <Cell title={dataItem.Fdm} className="text-right">{dataItem.Fdm}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData } = this.state;

        return gridData && (
            <div className="table-responsive table-striped my-2">
                <Table
                    maxWidth={maxWidth}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData)}
                    containerPadding={10}
                />
            </div>
        );
    }
}


function mapStateToProps({ landingPageStatistics }) {
    const { param, shortCodeLanguageList, isGroupByCountry } = landingPageStatistics;

    return Object.assign({}, { param, shortCodeLanguageList, isGroupByCountry });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoaderUserPanel, setSelectedPageDetailFilter, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);