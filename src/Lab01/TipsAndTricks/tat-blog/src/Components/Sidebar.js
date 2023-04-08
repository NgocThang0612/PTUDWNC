import React from "react";
import SearchForm from "./SearchForm";
import CategoriesWidget from "./Widgets/CategoriesWidget";
import FeaturedPostsWidget from "./Widgets/FeaturedPostsWidget";
import RandomPostsWidget from "./Widgets/RandomPostsWidget";
import TagCloudWidget  from "./Widgets/TagCloudWidget";
import BestAuthorWidget from "./Widgets/BestAuthorWidget";
import ArchivesWidget from "./Widgets/ArchivesWidget";

const Sidebar = () => {
    return (
        <div className='pt-4 ps-2'>
            <SearchForm />

            <CategoriesWidget />
            
            <FeaturedPostsWidget />
            
            <RandomPostsWidget />

            <TagCloudWidget />

            <BestAuthorWidget />

            <ArchivesWidget />
        </div>
    )
}
export default Sidebar;