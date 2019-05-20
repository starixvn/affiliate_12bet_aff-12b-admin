import React from 'react';
import { connect } from 'react-redux';
import { Breadcrumb, BreadcrumbItem } from 'reactstrap';

import { userPanelMenuKey } from '../../../customer/common/modules/constants';
const LinkMenu = (props) => {
    const { linkMenuList } = props;
    const lastIndex = linkMenuList && linkMenuList.length - 1;

    return (
        <div>
            <Breadcrumb>
            {
                linkMenuList.map((item, index) => {
                    return (
                        index === lastIndex ? (
                            <BreadcrumbItem active>{item.title}</BreadcrumbItem>
                        ) : (
                            <BreadcrumbItem><a href="javascript:;" onClick={item.handleClick}>{item.title}</a></BreadcrumbItem>
                        )
                    )
                })
            }
            </Breadcrumb>
        </div>
    );
}

export default connect()(React.memo(LinkMenu));