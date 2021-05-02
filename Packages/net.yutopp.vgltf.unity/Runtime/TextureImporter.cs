﻿//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using UnityEngine;

namespace VGltf.Unity
{
    public abstract class TextureImporterHook
    {
        public virtual void PostHook(TextureImporter importer)
        {
        }
    }

    public class TextureImporter : ImporterRefHookable<TextureImporterHook>
    {
        public override IContext Context { get; }

        public TextureImporter(IContext context)
        {
            Context = context;
        }

        public IndexedResource<Texture2D> Import(int texIndex)
        {
            var gltf = Context.Container.Gltf;
            var gltfTex = gltf.Textures[texIndex];

            return Context.Cache.CacheObjectIfNotExists(texIndex, texIndex, Context.Cache.Textures, ForceImport);
        }

        public IndexedResource<Texture2D> ForceImport(int texIndex)
        {
            var gltf = Context.Container.Gltf;
            var gltfTex = gltf.Textures[texIndex];

            var tex = new Texture2D(0, 0, TextureFormat.RGBA32, true, true);
            tex.name = gltfTex.Name;

            if (gltfTex.Source != null)
            {
                var imageResource = Context.Images.Import(gltfTex.Source.Value);

                var imageBuffer = new byte[imageResource.Data.Count];
                Array.Copy(imageResource.Data.Array, imageResource.Data.Offset, imageBuffer, 0, imageResource.Data.Count);

                tex.LoadImage(imageBuffer);
            }

            return new IndexedResource<Texture2D>
            {
                Index = texIndex,
                Value = tex,
            };
        }
    }
}
