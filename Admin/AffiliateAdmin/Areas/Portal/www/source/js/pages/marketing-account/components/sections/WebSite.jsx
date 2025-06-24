import React, { useRef, useEffect } from 'react';
import { Form, FormGroup, Label, Col, Button, Input } from 'reactstrap';

const getUrls = (moreSiteUrls) => {
    return moreSiteUrls ? moreSiteUrls.split(";").map(url => url.trim()) : [];
}

export const WebSite = (props) => {
    const { websiteInfo, setWebsiteInfo } = props;

    const moreSiteUrlsRefs = useRef([]);

    useEffect(() => {
        moreSiteUrlsRefs.current = getUrls(websiteInfo.moreSiteUrls)?.map(
           (_, i) => moreSiteUrlsRefs.current[i] || React.createRef()
        );
    }, [websiteInfo.moreSiteUrls]);


    const _handleChange = (event) => {
        const target = event.target;

        const newWebsiteInfo = {
            ...websiteInfo,
            [target.name]: target.value
        };
        setWebsiteInfo(newWebsiteInfo);
    };

    const _handleChangeMoreUrl = (event) => {
        const { name, value } = event.target;

        const updatedUrls = [...websiteInfo.moreSiteUrls];
        updatedUrls[name] = value;
        setWebsiteInfo({
            ...websiteInfo,
            moreSiteUrls: updatedUrls
        });
    };

    const _handleAddUrl = () => {
        if (!websiteInfo.siteUrl.trim()) {
            return;
        }
        console.log('push')
        moreSiteUrlsRefs.current.push(React.createRef());
    };

    const _handleDeleteUrl = (siteIndex) => {
        const updatedUrls = [...websiteInfo.moreSiteUrls];
        updatedUrls.splice(siteIndex, 1);

        setWebsiteInfo({
            ...websiteInfo,
            moreSiteUrls: updatedUrls
        });
    }

    const _handleChangeHasWebSite = (hasWebSite) => {
        setWebsiteInfo({
            ...websiteInfo,
            hasWebSite: hasWebSite
        });
    }

    const _renderMoreSiteUrl = (hasWebSite) => {
        return (
            <React.Fragment>
                {
                    moreSiteUrlsRefs && moreSiteUrlsRefs.current.map((currentRef, index) => {
                        return (
                            <FormGroup row key={`siteUrl${index} ${123}`}>
                                <Label for={`${index}`} sm={3} xl={2} >Website URL {index + 2}</Label>
                                <Col sm={5} xl={6} className="input-item">
                                    <input className="form-control" disabled={!hasWebSite} name={index} value={currentRef.value} onChange={_handleChangeMoreUrl} ref={currentRef} />
                                </Col>
                                <Button outline color="danger" disabled={!hasWebSite} className="my-1" onClick={() => _handleDeleteUrl(index)}><span aria-hidden>Del</span></Button>
                            </FormGroup>
                        )
                    })
                }
            </React.Fragment>
        );
    }

    return (
        <fieldset>
            <legend><h6 className="font-weight-bold">Website Information</h6></legend>
            <Form tag="div">
                <FormGroup>
                    <FormGroup check>
                        <Label check>
                            <Input 
                                type="radio" 
                                checked={websiteInfo.hasWebSite} 
                                onChange={() => _handleChangeHasWebSite(true)} 
                                onBlur={_handleChange} />I have my own website
                        </Label>
                    </FormGroup>
                    <FormGroup row className="pt-1">
                        <Label htmlFor="siteUrl" sm={2}>Website URL</Label>
                        <Col sm={6} className="input-item">
                            <Input 
                                type="text" 
                                name="siteUrl" 
                                disabled={!websiteInfo.hasWebSite} 
                                value={websiteInfo.siteUrl} 
                                onChange={_handleChange} 
                                onBlur={_handleChange} />
                        </Col>
                        <Button 
                            outline 
                            color="secondary" 
                            disabled={!websiteInfo.hasWebSite} 
                            className="my-1" 
                            onClick={_handleAddUrl}>
                            Add Url
                        </Button>
                    </FormGroup>
                </FormGroup>
                {
                    _renderMoreSiteUrl(websiteInfo.hasWebSite)
                }
                <FormGroup className="pt-3">
                    <FormGroup check>
                        <Label check>
                            <Input type="radio" checked={!websiteInfo.hasWebSite} onChange={() => _handleChangeHasWebSite(false)} />I don't have a website
                        </Label>
                    </FormGroup>
                    <FormGroup row className="pt-1">
                        <Label htmlFor="noSiteDescription" sm={3}>{`Please describe how you would like to promote 12Bet if you don't have the URL`}</Label>
                        <Col sm={9}>
                            <Input type="textarea" disabled={websiteInfo.hasWebSite} style={{ height: 70 }} id="noSiteDescription" name="noSiteDescription" value={websiteInfo.noSiteDescription} onChange={_handleChange} />
                        </Col>
                    </FormGroup>
                </FormGroup>

            </Form>
        </fieldset>
    );
}

export default React.memo(WebSite);