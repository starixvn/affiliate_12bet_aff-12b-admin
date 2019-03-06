import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerTitle } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { setSelectedPage, toggleLoader, fetchData } from '../actions';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: []
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            this.fetchData(this.props.param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);

        this.setState({ gridData: await this.props.fetchData(param) });

        this.props.toggleLoader(false);
    }

    handleSelectedPage = (page, languageCode) => {
        const selectedPage = Object.assign({}, { SelectedPage: page, LanguageCode: languageCode });

        this.props.setSelectedPage(selectedPage);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={290} title={headerTitle.Page}>{headerTitle.Page}</Cell>
                <Cell header={true} colWidth={290} title={headerTitle.Language}>{headerTitle.Language}</Cell>
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
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem));
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

    _buildDataRow(dataItem) {
        const { languageList } = this.props;
        const language = languageList[dataItem.LanguageCode] || dataItem.LanguageCode;

        return (
            <Row>
                <Cell title={dataItem.SiteUrl}>
                    {(dataItem.Page == "Old URL" || dataItem.Page == "none") ? dataItem.Page :
                        (<Button color="link" className="p-0" onClick={() => this.handleSelectedPage(dataItem.Page, dataItem.LanguageCode)}>
                            {dataItem.Page}
                        </Button>)}
                </Cell>
                <Cell title={language} className="text-right">{language}</Cell>
                <Cell title={dataItem.TotalClick} className="text-right">{dataItem.TotalClick}</Cell>
                <Cell title={dataItem.UniqueClick} className="text-right">{dataItem.UniqueClick}</Cell>
                <Cell title={dataItem.Signups} className="text-right">{dataItem.Signups}</Cell>
                <Cell title={dataItem.Fdm} className="text-right">{dataItem.Fdm}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData } = this.state;

        return (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1010}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData)}
                    containerPadding={10}
                />
            </div>
        );
    }
}


function mapStateToProps({ param, languageList }) {
    return Object.assign({}, { param, languageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, setSelectedPage, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);