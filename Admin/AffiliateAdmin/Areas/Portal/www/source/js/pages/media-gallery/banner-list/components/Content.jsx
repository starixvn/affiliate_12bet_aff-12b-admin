import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { resizeWidthHeight } from '../libs/utils';
import { headerText, globalResource, maxWidthHeight, pcLaptop, mobile } from '../libs/constants';
import { setSelectedBannerId, setPagingOption, fetchData, setCheckedBanners, setShouldResetScrollPosition, setParam, setBannerDetailUrl, getBannerRootUrl } from '../actions';

export const Content = (props) => {

    useEffect(() => {
        const { param, bannerRootUrl } = props;

        param && props.fetchData(param);
        !bannerRootUrl && props.getBannerRootUrl();
    }, [props.param, props.bannerRootUrl]);

    const _onCheckItem = (isChecked, BannerId) => {
        const newCheckedBanners = Object.assign({}, props.checkedBanners, { [BannerId]: isChecked });

        props.setShouldResetScrollPosition(false);
        props.setCheckedBanners(newCheckedBanners);
    }

    const _handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, props.param, newPaging);

        props.setPagingOption(newPaging);
        props.setParam(newParam);
    };

    function _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={20}></Cell>
                <Cell header={true} colWidth={25} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={415} title={headerText.Images}>{headerText.Images}</Cell>
                <Cell header={true} colWidth={190} title={headerText.FileName}>{headerText.FileName}</Cell>
                <Cell header={true} colWidth={55} title={headerText.Category}>{headerText.Category}</Cell>
                <Cell header={true} colWidth={60} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={55} title={headerText.Currency}>{headerText.Currency}</Cell>
                <Cell header={true} colWidth={50} title={headerText.Device}>{headerText.Device}</Cell>
                <Cell header={true} colWidth={50} title={headerText.ImageType}>{headerText.ImageType}</Cell>
                <Cell header={true} colWidth={50} title={headerText.Size}>{headerText.Size}</Cell>
                <Cell header={true} colWidth={30} title={globalResource.Edit}>{globalResource.Edit}</Cell>
            </Row>
        );
    }

    function _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(_buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="11">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    function _buildDataRow(dataItem, index) {
        const bannerId = dataItem.BannerId;
        const fileName = htmlDecode(dataItem.FileName);
        const src = `${props.bannerRootUrl}/${dataItem.FileName}?id=${dataItem.LastUpdateText}`;
        const size = dataItem.Size;
        const strSize = size.split('x');
        const realWidth = strSize[0];
        const realHeight = strSize[1];
        const isValidWithHeight = realWidth < maxWidthHeight && realHeight < maxWidthHeight;
        const resizeResult = !isValidWithHeight && resizeWidthHeight(realWidth, realHeight);
        const deviceType = dataItem.DeviceType === 0 ? pcLaptop : mobile;

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={props.checkedBanners[bannerId]}
                        onChange={(e) => _onCheckItem(e.target.checked, bannerId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center">
                    {
                        isValidWithHeight ? (
                            <img id={`img${bannerId}`} src={src} alt="" height={realHeight} width={realWidth} />
                        ) : (
                                <a href="javascript:;" id={`link${bannerId}`} onClick={() => props.setBannerDetailUrl({ src, height: realHeight, width: realWidth, bannerId })}>
                                    <img id={`img${bannerId}`} src={src} alt="" height={resizeResult.height} width={resizeResult.width} />
                                </a>
                            )
                    }
                </Cell>
                <Cell className="text-center text-break" title={fileName}>{fileName}</Cell>
                <Cell className="text-center text-break" title={dataItem.Category}>{dataItem.Category}</Cell>
                <Cell className="text-center text-break" title={dataItem.Language}>{dataItem.Language}</Cell>
                <Cell className="text-center text-break" title={dataItem.Currency}>{dataItem.Currency}</Cell>
                <Cell className="text-center text-break" title={deviceType}>{deviceType}</Cell>
                <Cell className="text-center text-break" title={dataItem.ImageType}>{dataItem.ImageType}</Cell>
                <Cell className="text-center text-break" title={size}>{size}</Cell>
                <Cell className="text-center" title="Edit Banner">
                    <Button color="link" onClick={() => props.setSelectedBannerId(bannerId)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    return props.gridData && (
        <div className="data-content">
            <div className="banner-list-content table-responsive table-striped my-2">
                <Table
                    minWidth={1250}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection(props.gridData)}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={props.pagingOption}
                    onPaging={_handlePaging}
                    shouldResetScrollPosition={props.shouldResetScrollPosition}
                />
            </div>
        </div>
    );
}

function mapStateToProps({ pagingOption, param, checkedBanners, bannerRootUrl, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { pagingOption, param, checkedBanners, bannerRootUrl, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedBannerId, fetchData, setPagingOption, setCheckedBanners, setShouldResetScrollPosition, setParam, setBannerDetailUrl, getBannerRootUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));