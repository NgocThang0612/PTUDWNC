import React, { useEffect, useState } from "react";
import { useLocation } from 'react-router-dom';
import PostItem from '../Components/PostItem';
import Pager from "../Components/Pager";
import { getPosts } from "../Services/BlogReponsitory";

const Index = () => {
    const [postList, setPostList] = useState([]);
    const [metadata, setMetadata] = useState({});

    function useQuery() {
        const { search } = useLocation();
        return React.useMemo(() => new URLSearchParams(search), [search]);
    }

    let query = useQuery(),
        k = query.get('k') ?? '',
        ps = query.get('ps') ?? 10,
        p = query.get('p') ?? 1;

    useEffect(() => {
        document.title = 'Trang chủ';

        getPosts(k, ps, p).then(data => {
            if (data){
                setPostList(data.items);
                setMetadata(data.metadata);
            }
            else
                setPostList([]);
        })
    }, [k, ps, p]);

    useEffect(() => {
        window.scrollTo(0, 0);
    }, [postList]);

    if (postList.length > 0)
    return (
        <div className="p-4">
        {postList.map((item, index) => {
            return (
                <PostItem postItem={item} key={index} />
            );
        })}
        <Pager postquery={{ 'keyword': k }} metadata={metadata} />
    </div>
    );
    else return (
        
        <h1>
            Đây là trang chủ
        </h1>
        
        
    );
}
export default Index;